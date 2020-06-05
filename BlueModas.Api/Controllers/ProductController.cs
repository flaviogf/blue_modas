using System.Collections.Generic;
using AutoMapper;
using BlueModas.Api.Repositories;
using BlueModas.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<ProductIndexViewModel>> Index()
        {
            var products = _mapper.Map<IList<ProductIndexViewModel>>(_productRepository.FindAll());

            return Ok(products);
        }
    }
}
