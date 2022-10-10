using System.Net;
using System.Collections.Generic;
// PRESENTATION LAYER
namespace Catalog.Api.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Catalog.Api.Entities;
    using Catalog.Api.Repositories;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/v1/[controller]")] // it would fetch CatalogController and strip off "controller"
    public class CatalogController: ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await this._repository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await this._repository.GetProduct(id);
            if (product == null)
            {
                this._logger.LogError($"Product with id: {id}, not found!");
                return NotFound();
            }

            return Ok(product);
        }
    
        [HttpGet]
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products = await this._repository.GetProductsByCategory(category);
            return Ok(products);
        }

        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            var products = await this._repository.GetProductsByName(name);
            return Ok(products);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await this._repository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            var deleted = await this._repository.DeleteProduct(id);
            return Ok(deleted);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await this._repository.UpdateProduct(product));
        }
    }
}