using MediatR;
using AutoMapper;
using FluentResults;
using FluentValidation;
using vsa_journey.Infrastructure.Events;
using vsa_journey.Infrastructure.Repositories;
using vsa_journey.Features.Products.Repository;

namespace vsa_journey.Features.Products.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<object>>
{
    private readonly IValidator<CreateProductCommand> _validator;
    private readonly IEventPublisher _eventPublisher;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;


    public CreateProductCommandHandler(IValidator<CreateProductCommand> validator, IEventPublisher eventPublisher, IMapper mapper, ILogger<CreateProductCommandHandler> logger, IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _productRepository = productRepository;
    }

    

    public async Task<Result<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        
        return Result.Ok().WithSuccess("Product created successfully");
    }
}