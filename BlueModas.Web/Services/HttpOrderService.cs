using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlueModas.Web.Infrastructure;
using BlueModas.Web.ViewModels;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlueModas.Web.Services
{
    public class HttpOrderService : IOrderService
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly ILogger<HttpOrderService> _logger;

        public HttpOrderService(IHttpClientFactory clientFactory, ILogger<HttpOrderService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<Result> Create(OrderStoreViewModel order)
        {
            try
            {
                var client = _clientFactory.CreateClient("Api");

                var json = JsonConvert.SerializeObject(order);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("Order", content);

                response.EnsureSuccessStatusCode();

                return Result.Ok();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, string.Empty);

                return Result.Fail(ex.Message);
            }
        }
    }
}