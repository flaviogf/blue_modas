using System;
using System.Threading.Tasks;
using BlueModas.Web.Services;
using BlueModas.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Web.Controllers
{
    [Route("MeusDados")]
    public class CustomerIdentificationController : Controller
    {
        private readonly IOrderService _orderService;

        public CustomerIdentificationController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Store()
        {
            if (!HttpContext.Session.TryGetValue("@order-number", out var value))
            {
                return RedirectToAction("Index", "Product");
            }

            var orderNumber = new Guid(value);

            var result = await _orderService.FindByNumber(orderNumber);

            if (result.IsFailure)
            {
                TempData["Failure"] = "Não foi possível carregar seus dados";

                return View();
            }

            var order = result.Value;

            var viewModel = new OrderCustomerStoreViewModel
            {
                Name = order.CustomerName,
                Email = order.CustomerEmail,
                Phone = order.CustomerPhone
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Store([FromForm] OrderCustomerStoreViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (!HttpContext.Session.TryGetValue("@order-number", out var value))
            {
                return RedirectToAction("Index", "Product");
            }

            var orderNumber = new Guid(value);

            var result = await _orderService.AddCustomer(orderNumber, viewModel);

            if (result.IsFailure)
            {
                TempData["Failure"] = "Não foi possível confirmar seus dados";

                return View(viewModel);
            }

            return RedirectToAction("Show", "OrderDetails");
        }
    }
}
