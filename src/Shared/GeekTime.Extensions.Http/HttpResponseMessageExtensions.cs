using Newtonsoft.Json;
using System.Threading.Tasks;
namespace System.Net.Http
{
    public static class HttpResponseMessageExtensions
    {
        public async static Task<T> AsJson<T>(this HttpResponseMessage httpResponseMessage)
        {
            var json = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
