using System;
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

        public async Task<Result> Add(OrderStoreViewModel order)
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

        public async Task<Result> AddItem(Guid orderNumber, OrderItemStoreViewModel item)
        {
            try
            {
                var client = _clientFactory.CreateClient("Api");

                var json = JsonConvert.SerializeObject(item);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"Order/{orderNumber}/Item", content);

                response.EnsureSuccessStatusCode();

                return Result.Ok();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, string.Empty);

                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> RemoveItem(Guid orderNumber, int productId)
        {
            try
            {
                var client = _clientFactory.CreateClient("Api");

                var response = await client.DeleteAsync($"Order/{orderNumber}/Item/{productId}");

                response.EnsureSuccessStatusCode();

                return Result.Ok();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, string.Empty);

                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> UpdateItem(Guid orderNumber, int productId, OrderItemUpdateViewModel item)
        {
            try
            {
                var client = _clientFactory.CreateClient("Api");

                var json = JsonConvert.SerializeObject(item);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"Order/{orderNumber}/Item/{productId}", content);

                response.EnsureSuccessStatusCode();

                return Result.Ok();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, string.Empty);

                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> AddCustomer(Guid orderNumber, OrderCustomerStoreViewModel customer)
        {
            try
            {
                var client = _clientFactory.CreateClient("Api");

                var json = JsonConvert.SerializeObject(customer);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"Order/{orderNumber}/Customer", content);

                response.EnsureSuccessStatusCode();

                return Result.Ok();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, string.Empty);

                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<OrderShowViewModel>> FindByNumber(Guid number)
        {
            try
            {
                var client = _clientFactory.CreateClient("Api");

                var response = await client.GetStringAsync($"/Order/{number}");

                var products = JsonConvert.DeserializeObject<OrderShowViewModel>(response);

                return Result.Ok<OrderShowViewModel>(products);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, string.Empty);

                return Result.Fail<OrderShowViewModel>(ex.Message);
            }
        }

        public async Task<Result<int>> CountNumberOfItems(Guid orderNumber)
        {
            try
            {
                var client = _clientFactory.CreateClient("Api");

                var response = await client.GetStringAsync($"/Order/{orderNumber}/NumberOfItems");

                var count = JsonConvert.DeserializeObject<int>(response);

                return Result.Ok<int>(count);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, string.Empty);

                return Result.Fail<int>(ex.Message);
            }
        }
    }
}
