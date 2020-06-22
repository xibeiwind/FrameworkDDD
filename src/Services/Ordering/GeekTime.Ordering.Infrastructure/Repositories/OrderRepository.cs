using GeekTime.Infrastructure.Core;
using GeekTime.Ordering.Domain.OrderAggregate;

namespace GeekTime.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order, long, OrderingDbContext>, IOrderRepository
    {
        public OrderRepository(OrderingDbContext context) : base(context)
        {
        }
    }
}
