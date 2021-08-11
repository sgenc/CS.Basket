using CicekSepeti.Basket.Core;
using CicekSepeti.Basket.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Basket.Api.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductApiRequestModel productApiRequestModel)
        {
            var productResponse = await _productService.AddProduct(productApiRequestModel);

            return Ok(productResponse);
        }
    }
}
