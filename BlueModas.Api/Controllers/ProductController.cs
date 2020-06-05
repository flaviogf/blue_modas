using System.Collections.Generic;
using BlueModas.Api.Models;
using BlueModas.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<Product>> Index()
        {
            var products = _productRepository.FindAll();

            return Ok(products);
        }
    }
}
