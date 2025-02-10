using Microsoft.JSInterop;

namespace Logic
{
    public static class SessionController
    {
        public static async Task<string> GetData(IJSRuntime jSRuntime, string key)
        {
            return await jSRuntime.InvokeAsync<string>("sessionStorage.getItem", key);
        }

        public static async Task SetData(IJSRuntime jSRuntime, string key, string data)
        {
            await jSRuntime.InvokeVoidAsync("sessionStorage.setItem", key, data);
        }
    }
}
