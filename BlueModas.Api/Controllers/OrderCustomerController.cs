using System;
using AutoMapper;
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
    [Route("Order/{number}/Customer")]
    public class OrderCustomerController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public OrderCustomerController(IOrderRepository orderRepository, IUnitOfWork uow, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Tags = new[] { "Order" })]
        public ActionResult Store([FromRoute] Guid number, [FromBody] OrderCustomerStoreViewModel viewModel)
        {
            var maybeOrder = _orderRepository.FindByNumber(number);

            if (!maybeOrder.HasValue)
            {
                return NotFound();
            }

            var customer = _mapper.Map<Customer>(viewModel);

            maybeOrder.Value.Customer = customer;

            _uow.Commit();

            return NoContent();
        }
    }
}
