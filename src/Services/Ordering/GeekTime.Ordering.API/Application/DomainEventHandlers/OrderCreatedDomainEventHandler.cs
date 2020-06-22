using DotNetCore.CAP;
using GeekTime.Domain;
using GeekTime.Ordering.API.Application.IntegrationEvents;
using GeekTime.Ordering.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace GeekTime.Ordering.API.Application.DomainEventHandlers
{
    public class OrderCreatedDomainEventHandler : IDomainEventHandler<OrderCreatedDomainEvent>
    {
        ICapPublisher _capPublisher;
        public OrderCreatedDomainEventHandler(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _capPublisher.PublishAsync("OrderCreated", new OrderCreatedIntegrationEvent(notification.Order.Id));
        }
    }
}
