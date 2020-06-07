using System;
using System.Threading.Tasks;
using BlueModas.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Web.Controllers
{
    [Route("DetalhesDoPedido")]
    public class OrderDetailsController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderDetailsController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Show()
        {
            if (!HttpContext.Session.TryGetValue("@order-number", out var value))
            {
                return RedirectToAction("Index", "Product");
            }

            var orderNumber = new Guid(value);

            var result = await _orderService.FindByNumber(orderNumber);

            if (result.IsFailure)
            {
                TempData["Failure"] = "Não foi possível carregar os detalhes do pedido";

                return View();
            }

            var order = result.Value;

            return View(order);
        }
    }
}
