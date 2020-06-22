using System.Net.Http;
namespace GeekTime.Mobile.ApiAggregator.Services
{
    public class OrderService : IOrderService
    {
        IHttpClientFactory _clientFactory;
        HttpClient _httpClient;
        public OrderService(HttpClient httpClient)
        {
            //_clientFactory = clientFactory;
            _httpClient = httpClient;

        }


        public void GetOrder()
        { 
            
        }
    }
}
