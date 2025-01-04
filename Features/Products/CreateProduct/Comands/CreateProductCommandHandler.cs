using MediatR;
using AutoMapper;
using FluentResults;
using FluentValidation;
using vsa_journey.Infrastructure.Events;

namespace vsa_journey.Features.Products.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<object>>
{
    private readonly IValidator<CreateProductCommand> _validator;
    private readonly IEventPublisher _eventPublisher;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductCommandHandler> _logger;
    
    
    
    public async Task<Result<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}