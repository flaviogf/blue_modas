using System;
using BlueModas.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlueModas.Api.Controllers
{
    [ApiController]
    [Route("Order/{number}/NumberOfItems")]
    public class OrderNumberOfItemsController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderNumberOfItemsController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Tags = new[] { "Order" })]
        public ActionResult<int> Show([FromRoute] Guid number)
        {
            var maybeOrder = _orderRepository.FindByNumber(number);

            if (!maybeOrder.HasValue)
            {
                return NotFound();
            }

            var order = maybeOrder.Value;

            return Ok(order.NumberOfItems);
        }
    }
}
