using System.Collections.Generic;
using AutoMapper;
using BlueModas.Api.Infrastructure;
using BlueModas.Api.Models;
using BlueModas.Api.Repositories;
using BlueModas.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _uow;

        public ProductController
        (
            IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork uow
        )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _uow = uow;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ProductShowViewModel> Store([FromBody] ProductStoreViewModel viewModel)
        {
            var product = _mapper.Map<Product>(viewModel);

            _productRepository.Add(product);

            _uow.Commit();

            return CreatedAtAction(nameof(Show), new { id = product.Id }, _mapper.Map<ProductShowViewModel>(product));
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ProductIndexViewModel>> Index()
        {
            var products = _mapper.Map<IList<ProductIndexViewModel>>(_productRepository.FindAll());

            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductShowViewModel> Show([FromRouteAttribute] int id)
        {
            var maybeProduct = _productRepository.FindById(id);

            if (!maybeProduct.HasValue)
            {
                return NotFound();
            }

            var product = _mapper.Map<ProductShowViewModel>(maybeProduct.Value);

            return Ok(product);
        }
    }
}
