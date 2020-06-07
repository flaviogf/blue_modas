using System;
using System.Threading.Tasks;
using BlueModas.Web.Services;
using BlueModas.Web.ViewModels;
using Microsoft.AspNetCore.Http;
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
                var orderNumber = new Guid(value);

                var item = new OrderItemStoreViewModel
                {
                    ProductId = productId
                };

                var itemResult = await _orderService.AddItem(orderNumber, item);

                if (itemResult.IsFailure)
                {
                    TempData["Failure"] = "Não foi possível adicionar o produto no carrinho";

                    return RedirectToAction("Index", "Product");
                }

                TempData["Success"] = "Produto adicionado ao carrinho";

                var countResult = await _orderService.CountNumberOfItems(orderNumber);

                if (countResult.IsFailure)
                {
                    TempData["Failure"] = "Não foi possível atualizar o carrinho";

                    return RedirectToAction("Index", "Product");
                }

                HttpContext.Session.SetInt32("@order-items-count", countResult.Value);

                return RedirectToAction("Index", "Product");
            }

            var order = new OrderStoreViewModel
            {
                Number = Guid.NewGuid()
            };

            var orderCreateResult = await _orderService.Add(order);

            if (orderCreateResult.IsFailure)
            {
                TempData["Failure"] = "Não foi possível adicionar o produto no carrinho";

                return RedirectToAction("Index", "Product");
            }

            HttpContext.Session.Set("@order-number", order.Number.ToByteArray());

            var orderItem = new OrderItemStoreViewModel
            {
                ProductId = productId
            };

            var orderItemCreateResult = await _orderService.AddItem(order.Number, orderItem);

            if (orderItemCreateResult.IsFailure)
            {
                TempData["Failure"] = "Não foi possível adicionar o produto no carrinho";

                return RedirectToAction("Index", "Product");
            }

            TempData["Success"] = "Produto adicionado ao carrinho";

            var orderItemCountResult = await _orderService.CountNumberOfItems(order.Number);

            if (orderItemCountResult.IsFailure)
            {
                TempData["Failure"] = "Não foi possível atualizar o carrinho";

                return RedirectToAction("Index", "Product");
            }

            HttpContext.Session.SetInt32("@order-items-count", orderItemCountResult.Value);

            return RedirectToAction("Index", "Product");
        }
    }
}
