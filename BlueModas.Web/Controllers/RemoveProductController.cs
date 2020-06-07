using System;
using System.Threading.Tasks;
using BlueModas.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Web.Controllers
{
    [Route("[controller]/{productId}")]
    public class RemoveProductController : Controller
    {
        private readonly IOrderService _orderService;

        public RemoveProductController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Destroy([FromRoute] int productId)
        {
            if (!HttpContext.Session.TryGetValue("@order-number", out var value))
            {
                return RedirectToAction("Index", "Product");
            }

            var orderNumber = new Guid(value);

            var result = await _orderService.RemoveItem(orderNumber, productId);

            if (result.IsFailure)
            {
                TempData["Failure"] = "Não foi possível remover o produto";

                return RedirectToAction("Show", "ShoppingCart");
            }

            TempData["Success"] = "Produto removido do carrinho";

            var countResult = await _orderService.CountNumberOfItems(orderNumber);

            if (countResult.IsFailure)
            {
                TempData["Failure"] = "Não foi possível atualizar o carrinho";

                return RedirectToAction("Index", "Product");
            }

            HttpContext.Session.SetInt32("@order-items-count", countResult.Value);

            return RedirectToAction("Show", "ShoppingCart");
        }
    }
}
