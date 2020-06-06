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
    }
}
