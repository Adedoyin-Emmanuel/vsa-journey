using MediatR;
using AutoMapper;
using FluentResults;
using FluentValidation;
using vsa_journey.Features.Products.Repository;
using vsa_journey.Infrastructure.Events;

namespace vsa_journey.Features.Products.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<object>>
{
    private readonly IValidator<CreateProductCommand> _validator;
    private readonly IEventPublisher _eventPublisher;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IProductRepository _productRepository;


    public CreateProductCommandHandler(IValidator<CreateProductCommand> validator, IEventPublisher eventPublisher, IMapper mapper, ILogger<CreateProductCommandHandler> logger, IProductRepository productRepository)
    {
        _validator = validator;
        _eventPublisher = eventPublisher;
        _mapper = mapper;
        _logger = logger;
        _productRepository = productRepository;
    }


    public async Task<Result<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        return Result.Ok().WithSuccess("Product created successfully");
    }
}