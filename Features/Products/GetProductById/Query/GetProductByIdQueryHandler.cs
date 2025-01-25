using MediatR;
using AutoMapper;
using FluentResults;
using vsa_journey.Features.Products.Repository;

namespace vsa_journey.Features.Products.GetProductById.Query;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>
{

    private readonly IProductRepository _productRepository;
    private readonly ILogger<GetProductByIdResponse> _logger;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, ILogger<GetProductByIdResponse> logger, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _productRepository = productRepository;
    }
    public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
        {
            return Result.Fail("Product with given id was not found");
        }

        var productResponse = _mapper.Map<GetProductByIdResponse>(product);
        
        return Result.Ok(productResponse).WithSuccess("Product fetched successfully");
    }
}