using MediatR;
using System.Collections.Generic;
namespace GeekTime.Ordering.API.Application.Queries
{
    public class MyOrderQuery : IRequest<List<string>>
    {
        public string UserName { get; set; }
    }
}
