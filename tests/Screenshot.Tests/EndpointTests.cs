using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Newtonsoft.Json;

using Screenshot.API;

namespace Screenshot.Tests
{
    public abstract class EndpointTests
    {
        protected readonly HttpClient Client;
        protected readonly TestServer Server;

        protected EndpointTests()
        {
            Server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            Client = Server.CreateClient();
        }

        protected static StringContent CreateStringContent<TRequest>(TRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected static async Task<TResponse> DeserializeResponse<TResponse>(HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();
            var deserializedResponse = JsonConvert.DeserializeObject<TResponse>(result);
            return deserializedResponse;
        }
    }
}