using System;
using System.Threading.Tasks;
using BlueModas.Web.Services;
using BlueModas.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Web.Controllers
{
    [Route("[controller]/{productId}")]
    public class EditProductController : Controller
    {
        private readonly IOrderService _orderService;

        public EditProductController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromForm] OrderItemUpdateViewModel viewModel)
        {
            if (!HttpContext.Session.TryGetValue("@order-number", out var value))
            {
                return RedirectToAction("Index", "Product");
            }

            if (!ModelState.IsValid)
            {
                TempData["Failure"] = "Quantidade informada inválida";

                return RedirectToAction("Show", "ShoppingCart");
            }

            var orderNumber = new Guid(value);

            var result = await _orderService.UpdateItem(orderNumber, productId, viewModel);

            if (result.IsFailure)
            {
                TempData["Failure"] = "Não foi possível alterar a quantidade";

                return RedirectToAction("Show", "ShoppingCart");
            }

            TempData["Success"] = "Quantidade alterada";

            var countResult = await _orderService.CountNumberOfItems(orderNumber);

            if (countResult.IsFailure)
            {
                TempData["Failure"] = "Não foi possível atualizar o carrinho";

                return RedirectToAction("Show", "ShoppingCart");
            }

            HttpContext.Session.SetInt32("@order-items-count", countResult.Value);

            return RedirectToAction("Show", "ShoppingCart");
        }
    }
}
