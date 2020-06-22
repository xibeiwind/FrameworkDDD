﻿using DotNetCore.CAP;
using GeekTime.Ordering.Domain.OrderAggregate;
using GeekTime.Ordering.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeekTime.Ordering.API.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, long>
    {
        IOrderRepository _orderRepository;
        ICapPublisher _capPublisher;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, ICapPublisher capPublisher)
        {
            _orderRepository = orderRepository;
            _capPublisher = capPublisher;
        }


        public async Task<long> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var address = new Address("wen san lu", "hangzhou", "310000");
            var order = new Order("xiaohong1999", "xiaohong", 25, address);

            _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return order.Id;
        }
    }
}
