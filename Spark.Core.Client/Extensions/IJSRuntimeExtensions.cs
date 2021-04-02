using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Spark.Core.Client.Extensions
{
    public static class IJSRuntimeExtensions
    {
        public static async ValueTask<bool> Confirm(this IJSRuntime js, string message)
        {

            return await js.InvokeAsync<bool>("confirm", message);
        }

        public static async ValueTask Alert(this IJSRuntime js, string message)
        {
            await js.InvokeVoidAsync("alert", message);
        }

        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
            => js.InvokeAsync<object>("localStorage.setItem", key, content);

        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<string>("localStorage.getItem", key);

        public static ValueTask<object> RemoveFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<object>("localStorage.removeItem", key);
    }
}
