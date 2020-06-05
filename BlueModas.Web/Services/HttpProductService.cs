using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlueModas.Web.ViewModels;
using Newtonsoft.Json;

namespace BlueModas.Web.Services
{
    public class HttpProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IList<ProductIndexViewModel>> FindAll()
        {
            var client = _clientFactory.CreateClient("Api");

            var response = await client.GetStringAsync("/Product");

            return JsonConvert.DeserializeObject<IList<ProductIndexViewModel>>(response);
        }
    }
}
