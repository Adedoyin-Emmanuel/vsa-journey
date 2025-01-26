using FluentResults;
using MediatR;
using vsa_journey.Features.Products.Repository;
using vsa_journey.Infrastructure.Repositories;

namespace vsa_journey.Features.Products.DeleteProduct.Command;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<object>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly Logger<DeleteProductCommandHandler> _logger;


    public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, Logger<DeleteProductCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _logger = logger;
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
        
        
    }
}