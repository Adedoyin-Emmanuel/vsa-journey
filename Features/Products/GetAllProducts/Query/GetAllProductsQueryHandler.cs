using FluentResults;
using FluentValidation;
using MediatR;
using vsa_journey.Application.Common.PaginatedResult;
using vsa_journey.Features.Products.Repository;

namespace vsa_journey.Features.Products.GetAllProducts.Query;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<PaginatedResult<object>>>
{

    private readonly IValidator<GetAllProductsQueryHandler> _validator;
    private readonly IProductRepository _productRepository;
    private readonly ILogger<GetAllProductsQueryHandler> _logger;

    public GetAllProductsQueryHandler(IValidator<GetAllProductsQueryHandler> validator, IProductRepository productRepository, ILogger<GetAllProductsQueryHandler> logger)
    {
        _validator = validator;
        _productRepository = productRepository;
        _logger = logger;
    }
    public async Task<Result<PaginatedResult<object>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        return Result.Ok();
    }
}