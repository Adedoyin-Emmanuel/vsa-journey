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
    
    
    public async Task<Result<object>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}