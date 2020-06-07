using System.Collections.Generic;
using System.Threading.Tasks;
using BlueModas.Web.Services;
using BlueModas.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Web.Controllers
{
    [Route("")]
    [Route("Produtos")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var result = await _productService.FindAll();

            if (result.IsFailure)
            {
                TempData["Failure"] = "Não foi possível carregar os produtos";

                return View(new List<ProductIndexViewModel>());
            }

            return View(result.Value);
        }
    }
}
