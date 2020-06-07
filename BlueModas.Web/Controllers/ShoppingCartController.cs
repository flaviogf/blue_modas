using System;
using System.Threading.Tasks;
using BlueModas.Web.Services;
using BlueModas.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Web.Controllers
{
    [Route("MeuCarrinho")]
    public class ShoppingCartController : Controller
    {
        private readonly IOrderService _orderService;

        public ShoppingCartController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Show()
        {
            if (!HttpContext.Session.TryGetValue("@order-number", out var value))
            {
                return View(new OrderShowViewModel());
            }

            var orderNumber = new Guid(value);

            var result = await _orderService.FindByNumber(orderNumber);

            if (result.IsFailure)
            {
                TempData["Failure"] = "Não foi possível carregar o carrinho";

                return View(new OrderShowViewModel());
            }

            return View(result.Value);
        }
    }
}
