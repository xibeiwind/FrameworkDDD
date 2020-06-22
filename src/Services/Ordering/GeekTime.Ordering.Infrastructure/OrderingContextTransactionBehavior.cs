using GeekTime.Infrastructure.Core.Behaviors;
using Microsoft.Extensions.Logging;

namespace GeekTime.Ordering.Infrastructure
{
    public class OrderingContextTransactionBehavior<TRequest, TResponse>
        : TransactionBehavior<OrderingDbContext, TRequest, TResponse>
    {
        public OrderingContextTransactionBehavior(OrderingDbContext dbContext,
            ILogger<OrderingContextTransactionBehavior<TRequest, TResponse>> logger)
            : base(dbContext, logger)
        {
        }
    }
}
