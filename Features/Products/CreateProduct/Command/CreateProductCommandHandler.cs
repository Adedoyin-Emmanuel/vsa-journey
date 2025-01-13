using AutoMapper;
using FluentEmail.Core;
using FluentResults;
using FluentValidation;
using MediatR;
using vsa_journey.Features.Products.Repository;
using vsa_journey.Infrastructure.Events;
using vsa_journey.Infrastructure.Repositories;
using vsa_journey.Infrastructure.Services.FileUpload;

namespace vsa_journey.Features.Products.CreateProduct.Command;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<object>>
{
    private readonly IValidator<CreateProductCommand> _validator;
    private readonly IEventPublisher _eventPublisher;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileUploadService _fileUploadService;


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

    

    public async Task<Result<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
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


        
        
        
        
        
        
        return Result.Ok().WithSuccess("Product created successfully");
    }
}