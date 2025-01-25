using AutoMapper;
using FluentResults;
using FluentValidation;
using MediatR;
using vsa_journey.Features.Products.Repository;
using vsa_journey.Infrastructure.Repositories;
using vsa_journey.Infrastructure.Services.FileUpload;

namespace vsa_journey.Features.Products.UpdateProduct.Command;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<UpdateProductResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IFileUploadService _fileUploadService;
    private readonly IValidator<UpdateProductCommand> _validator;
    private readonly ILogger<UpdateProductCommandHandler> _logger;


    public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository, IFileUploadService fileUploadService, IValidator<UpdateProductCommand> validator, ILogger<UpdateProductCommandHandler> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _fileUploadService = fileUploadService;
        _validator = validator;
        _logger = logger;
    }
    
    
    public async Task<Result<UpdateProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var existingProduct = await _productRepository.GetByIdAsync(request.Id);

        if (existingProduct == null)
        {
            _logger.LogWarning("Product with ID {ProductId} was not found.", request.Id);
            return Result.Fail("Product with given id was not found");
        }

        if (request.Files?.Any() == true)
        {
            _logger.LogInformation("Uploading files for product update, ProductId: {ProductId}", request.Id);
            var filesUploadResult = await _fileUploadService.UploadFilesAsync(request.Files);

            if (filesUploadResult.IsFailed)
            {
                var fileUploadResultErrors = filesUploadResult.Errors.Select(error => error.Message);
                _logger.LogError("File upload failed for ProductId {ProductId}: {Errors}", request.Id, fileUploadResultErrors);
                return Result.Fail(fileUploadResultErrors);
            }

            var uploadedImagesUrl = filesUploadResult.ValueOrDefault?.UploadedFiles;
            if (uploadedImagesUrl == null || !uploadedImagesUrl.Any())
            {
                _logger.LogError("No files were returned after upload for ProductId {ProductId}", request.Id);
                return Result.Fail("Failed to upload files or no files were returned.");
            }

            var uploadedImagesBaseUrl = uploadedImagesUrl.First().UploadUrl;
            var otherImagesUrl = uploadedImagesUrl.Skip(1).Select(images => images.UploadUrl).ToList();

            var existingBaseImageUrl = existingProduct.BaseImageUrl;
            var existingImagesUrl = existingProduct.Images;

            existingImagesUrl.Add(existingBaseImageUrl);

            _logger.LogInformation("Deleting old product files for ProductId: {ProductId}", request.Id);
            var fileDeletionResult =  _fileUploadService.DeleteFiles(existingImagesUrl);

            if (fileDeletionResult.IsFailed)
            {
                var fileDeletionResultErrors = fileDeletionResult.Errors.Select(error => error.Message);
                _logger.LogError("File deletion failed for ProductId {ProductId}: {Errors}", request.Id, fileDeletionResultErrors);
                return Result.Fail(fileDeletionResultErrors);
            }

            existingProduct.BaseImageUrl = uploadedImagesBaseUrl;
            existingProduct.Images = otherImagesUrl;
        }

        _mapper.Map(request, existingProduct);

        _logger.LogInformation("Updating product in repository, ProductId: {ProductId}", request.Id);
        await _productRepository.UpdateAsync(existingProduct);

        await _unitOfWork.SaveChangesAsync();

        var productToReturn = _mapper.Map<UpdateProductResponse>(existingProduct);

        _logger.LogInformation("Product updated successfully, ProductId: {ProductId}", request.Id);
        return Result.Ok(productToReturn).WithSuccess("Product updated successfully");
    }

}