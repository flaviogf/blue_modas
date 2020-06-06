using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlueModas.Web.Infrastructure;
using BlueModas.Web.ViewModels;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlueModas.Web.Services
{
    public class HttpProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly ILogger<HttpProductService> _logger;

        public HttpProductService(IHttpClientFactory clientFactory, ILogger<HttpProductService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<Result<IList<ProductIndexViewModel>>> FindAll()
        {
            try
            {
                var client = _clientFactory.CreateClient("Api");

                var response = await client.GetStringAsync("/Product");

                var products = JsonConvert.DeserializeObject<IList<ProductIndexViewModel>>(response);

                return Result.Ok<IList<ProductIndexViewModel>>(products);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, string.Empty);

                return Result.Fail<IList<ProductIndexViewModel>>(ex.Message);
            }
        }
    }
}
