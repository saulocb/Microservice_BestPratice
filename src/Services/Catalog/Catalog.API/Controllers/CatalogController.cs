using Catalog.Aplication.Features.Product.Commands.CreateProduct;
using Catalog.Aplication.Features.Product.Commands.UpdateProduct;
using Catalog.Aplication.Features.Product.Queries.GetProductsList;
using Catalog.Aplication.Interfaces;
using Catalog.Domain.Entities;
using DnsClient.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductService _iProductService;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ILogger<CatalogController> logger, IProductService iProductService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _iProductService = iProductService ?? throw new ArgumentNullException(nameof(iProductService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductsVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductsVm>>> GetProducts()
        {
            var products = await _iProductService.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductsVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductsVm>> GetProductById(string id)
        {
            var product = await _iProductService.GetProduct(id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }

            return Ok(product);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductsVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductsVm>>> GetProductByCategory(string category)
        {
            var products = await _iProductService.GetProductByCategory(category);
            return Ok(products);
        }

        [Route("[action]/{name}", Name = "GetProductByName")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<ProductsVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductsVm>>> GetProductByName(string name)
        {
            var items = await _iProductService.GetProductByName(name);
            if (items == null)
            {
                _logger.LogError($"Products with name: {name} not found.");
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductsVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductsVm>> CreateProduct([FromBody] CreateProductCommand product)
        {
            await _iProductService.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProductsVm), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand product)
        {
            await _iProductService.UpdateProduct(product);
            return Ok();
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]        
        [ProducesResponseType(typeof(ProductsVm), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            await _iProductService.DeleteProduct(id);
            return Ok();
        }
    }
}
