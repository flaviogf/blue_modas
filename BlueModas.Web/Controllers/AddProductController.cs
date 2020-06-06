using System;
using System.Threading.Tasks;
using BlueModas.Web.Services;
using BlueModas.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Web.Controllers
{
    [Route("[controller]/{productId}")]
    public class AddProductController : Controller
    {
        private readonly IOrderService _orderService;

        private readonly IProductService _productService;

        public AddProductController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpPost]
        [Route("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Store([FromRoute] int productId)
        {
            if (HttpContext.Session.TryGetValue("@order-number", out var value))
            {
                var orderItem = new OrderItemStoreViewModel
                {
                    OrderNumber = new Guid(value),
                    ProductId = productId
                };

                var orderItemCreateResult = await _orderService.CreateItem(orderItem);

                if (orderItemCreateResult.IsFailure)
                {
                    TempData["Failure"] = "Não foi possível adicionar o produto no carrinho";

                    return RedirectToAction("Index", "Product");
                }

                TempData["Success"] = "Produto adicionado ao carrinho";

                return RedirectToAction("Index", "Product");
            }
            else
            {
                var order = new OrderStoreViewModel
                {
                    Number = Guid.NewGuid()
                };

                var orderCreateResult = await _orderService.Create(order);

                if (orderCreateResult.IsFailure)
                {
                    TempData["Failure"] = "Não foi possível adicionar o produto no carrinho";

                    return RedirectToAction("Index", "Product");
                }

                var orderItem = new OrderItemStoreViewModel
                {
                    OrderNumber = order.Number,
                    ProductId = productId
                };

                var orderItemCreateResult = await _orderService.CreateItem(orderItem);

                if (orderItemCreateResult.IsFailure)
                {
                    TempData["Failure"] = "Não foi possível adicionar o produto no carrinho";

                    return RedirectToAction("Index", "Product");
                }

                HttpContext.Session.Set("@order-number", order.Number.ToByteArray());

                TempData["Success"] = "Produto adicionado ao carrinho";

                return RedirectToAction("Index", "Product");
            }
        }
    }
}
