using MediatR;
using FluentResults;
using FluentValidation;
using vsa_journey.Domain.Entities.Product;
using vsa_journey.Features.Products.Repository;
using vsa_journey.Application.Common.PaginatedResult;

namespace vsa_journey.Features.Products.GetAllProducts.Query;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<PaginatedResult<Product>>>
{

    private readonly IValidator<GetAllProductsQuery> _validator;
    private readonly IProductRepository _productRepository;
    private readonly ILogger<GetAllProductsQueryHandler> _logger;

    public GetAllProductsQueryHandler(IValidator<GetAllProductsQuery> validator, IProductRepository productRepository, ILogger<GetAllProductsQueryHandler> logger)
    {
        _validator = validator;
        _productRepository = productRepository;
        _logger = logger;
    }
    public async Task<Result<PaginatedResult<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var allProducts = await _productRepository.GetAllAsync(request.Skip, request.Take);

        return Result.Ok(allProducts).WithSuccess("Products fetched successfully");
    }
}