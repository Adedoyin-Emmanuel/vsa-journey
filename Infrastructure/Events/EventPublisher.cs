using MediatR;

namespace vsa_journey.Infrastructure.Events;

public class EventPublisher : IEventPublisher
{
    private readonly IMediator _mediator;

    public EventPublisher(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : INotification
    {
       await _mediator.Publish(@event);
    }
}