using MediatR;

namespace vsa_journey.Infrastructure.Events;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : INotification;
}