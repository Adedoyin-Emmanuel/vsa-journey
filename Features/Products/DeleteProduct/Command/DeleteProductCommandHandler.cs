using MediatR;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using vsa_journey.Infrastructure.Events;
using vsa_journey.Infrastructure.Repositories;
using vsa_journey.Features.Products.Repository;
using vsa_journey.Features.Products.DeleteProduct.Event;

namespace vsa_journey.Features.Products.DeleteProduct.Command;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<object>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly IProductRepository _productRepository;
    private readonly Logger<DeleteProductCommandHandler> _logger;
    


    public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, Logger<DeleteProductCommandHandler> logger, IEventPublisher eventPublisher)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _productRepository = productRepository;
    }

    public async Task<Result<object>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await _productRepository.GetByIdAsync(request.Id);

        if (existingProduct is null)
        {
            return Result.Fail("Product with given id was not found");
        }

        var productBaseImageUrl = existingProduct.BaseImageUrl;
        var productImagesUrl = existingProduct.Images;
        
        productImagesUrl.Add(productBaseImageUrl);

        var deleteProductEvent = new DeleteProductEvent
        {
            FilePaths = productImagesUrl
        };

       await _eventPublisher.PublishAsync(deleteProductEvent);

       await _productRepository.DeleteAsync(existingProduct.Id);

       await _unitOfWork.SaveChangesAsync();

       return Result.Ok("Product deleted successfully");
    }
}