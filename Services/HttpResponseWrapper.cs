using System.Net.Http;
using System.Threading.Tasks;

namespace Spark.Core.Services
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T response, bool success, HttpResponseMessage httpResponseMessage)
        {
            Success = success;
            Response = response;
            ResponseMessage = httpResponseMessage;
        }
        public bool Success { get; set; }
        public T Response { get; set; }
        public HttpResponseMessage ResponseMessage { get; set; }

        public async Task<string> GetBody()
        {
            return await ResponseMessage.Content.ReadAsStringAsync();
        }
    }
}
