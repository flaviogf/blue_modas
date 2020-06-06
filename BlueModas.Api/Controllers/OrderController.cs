using System;
using System.Collections.Generic;
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
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IUnitOfWork uow, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Tags = new[] { "Order" })]
        public ActionResult Store([FromBody] OrderStoreViewModel viewModel)
        {
            var maybeOrder = _orderRepository.FindByNumber(viewModel.Number);

            if (maybeOrder.HasValue)
            {
                return BadRequest();
            }

            var order = _mapper.Map<Order>(viewModel);

            _orderRepository.Add(order);

            _uow.Commit();

            return Ok();
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Tags = new[] { "Order" })]
        public ActionResult<IList<OrderIndexViewModel>> Index()
        {
            var orders = _mapper.Map<IList<OrderIndexViewModel>>(_orderRepository.FindAll());

            return Ok(orders);
        }

        [HttpGet]
        [Route("{number}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Tags = new[] { "Order" })]
        public ActionResult<OrderShowViewModel> Show([FromRoute] Guid number)
        {
            var maybeOrder = _orderRepository.FindByNumber(number);

            if (!maybeOrder.HasValue)
            {
                return NotFound();
            }

            var order = _mapper.Map<OrderShowViewModel>(maybeOrder.Value);

            return Ok(order);
        }
    }
}
