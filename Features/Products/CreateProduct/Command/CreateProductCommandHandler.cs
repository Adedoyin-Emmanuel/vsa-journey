using MediatR;
using AutoMapper;
using FluentResults;
using FluentValidation;
using vsa_journey.Infrastructure.Events;
using vsa_journey.Domain.Entities.Product;
using vsa_journey.Infrastructure.Repositories;
using vsa_journey.Features.Products.Repository;
using vsa_journey.Infrastructure.Services.FileUpload;

namespace vsa_journey.Features.Products.CreateProduct.Command;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly IProductRepository _productRepository;
    private readonly IFileUploadService _fileUploadService;
    private readonly IValidator<CreateProductCommand> _validator;
    private readonly ILogger<CreateProductCommandHandler> _logger;


    public CreateProductCommandHandler(IValidator<CreateProductCommand> validator, IEventPublisher eventPublisher, IMapper mapper, ILogger<CreateProductCommandHandler> logger, IProductRepository productRepository, IUnitOfWork unitOfWork, IFileUploadService fileUploadService)
    {
        _mapper = mapper;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _productRepository = productRepository;
        _fileUploadService = fileUploadService;
    }

    

    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var filesUploadResult = await _fileUploadService.UploadFilesAsync(request.Files);

        if (filesUploadResult.IsFailed)
        {
            var fileUploadResultErrors = filesUploadResult.Errors.Select(error => error.Message);
            return Result.Fail(fileUploadResultErrors);
        }

        var uploadedImagesUrl = filesUploadResult.ValueOrDefault.UploadedFiles;
        var baseImageUrl = uploadedImagesUrl.First().UploadUrl;
        var otherImagesUrl = uploadedImagesUrl.Skip(1).Select(images => images.UploadUrl)
            .ToArray();

        var product = _mapper.Map<CreateProductCommand, Product>(request);
        
        product.BaseImageUrl = baseImageUrl;
        product.Images = otherImagesUrl;

        var createdProduct = await _productRepository.AddAsync(product);

        await _unitOfWork.SaveChangesAsync();
        
        var createProductResponse = _mapper.Map<Product, CreateProductResponse>(createdProduct);
        
        return Result.Ok(createProductResponse).WithSuccess("Product created successfully");
    }
}