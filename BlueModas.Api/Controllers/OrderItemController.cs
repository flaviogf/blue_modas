using System;
using System.Linq;
using BlueModas.Api.Infrastructure;
using BlueModas.Api.Models;
using BlueModas.Api.Repositories;
using BlueModas.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlueModas.Api.Controllers
{
    [ApiController]
    [Route("Order/{number}/Item")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IProductRepository _productRepository;

        private readonly IUnitOfWork _uow;

        public OrderItemController(IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork uow)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _uow = uow;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Tags = new[] { "Order" })]
        public ActionResult Store([FromRoute] Guid number, [FromBody] OrderItemStoreViewModel viewModel)
        {
            var maybeOrder = _orderRepository.FindByNumber(number);

            if (!maybeOrder.HasValue)
            {
                return NotFound();
            }

            var maybeProduct = _productRepository.FindById(viewModel.ProductId);

            if (!maybeProduct.HasValue)
            {
                return BadRequest();
            }

            var order = maybeOrder.Value;

            var product = maybeProduct.Value;

            var orderItem = new OrderItem
            {
                OrderNumber = order.Number,
                ProductId = product.Id,
                Price = product.Price,
                Quantity = 1
            };

            Maybe<OrderItem> maybeOrderItem = order.Items.FirstOrDefault(it => it.Equals(orderItem));

            if (maybeOrderItem.HasValue)
            {
                maybeOrderItem.Value.Quantity += orderItem.Quantity;

                _uow.Commit();

                return NoContent();
            }

            order.Items.Add(orderItem);

            _uow.Commit();

            return NoContent();
        }

        [HttpPut]
        [Route("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Tags = new[] { "Order" })]
        public ActionResult Update([FromRoute] Guid number, [FromRoute] int productId, [FromBody] OrderItemUpdateViewModel viewModel)
        {
            var maybeOrder = _orderRepository.FindByNumber(number);

            if (!maybeOrder.HasValue)
            {
                return NotFound();
            }

            var order = maybeOrder.Value;

            Maybe<OrderItem> maybeOrderItem = order.Items.FirstOrDefault(it => it.ProductId == productId);

            if (!maybeOrderItem.HasValue)
            {
                return NotFound();
            }

            maybeOrderItem.Value.Quantity = viewModel.Quantity;

            _uow.Commit();

            return NoContent();
        }

        [HttpDelete]
        [Route("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Tags = new[] { "Order" })]
        public ActionResult Destroy([FromRoute] Guid number, [FromRoute] int productId)
        {
            var maybeOrder = _orderRepository.FindByNumber(number);

            if (!maybeOrder.HasValue)
            {
                return NotFound();
            }

            var order = maybeOrder.Value;

            Maybe<OrderItem> maybeOrderItem = order.Items.FirstOrDefault(it => it.ProductId == productId);

            if (!maybeOrderItem.HasValue)
            {
                return NotFound();
            }

            var orderItem = maybeOrderItem.Value;

            order.Items.Remove(orderItem);

            _uow.Commit();

            return NoContent();
        }
    }
}
