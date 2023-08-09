using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductEntity>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductEntity>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductEntity>> GetProductById(string id)
        {
            var product = await _productRepository.GetProduct(id);

            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductEntity>))]
        public async Task<ActionResult<IEnumerable<ProductEntity>>> GetProductByCategory(string category) 
        {
            if (category is null)
            {
                return BadRequest("Invalid category");
            }

            var products = await _productRepository.GetProductByCategory(category);

            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductEntity))]
        public async Task<ActionResult<ProductEntity>> CreateProduct([FromBody] ProductEntity productEntity)
        {
            if (productEntity is null)
            {
                return BadRequest("Invalid product");
            }
            await _productRepository.CreateProduct(productEntity);
            return CreatedAtRoute("GetProduct", new { id = productEntity.Id }, productEntity);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductEntity))]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductEntity productEntity)
        {
            if (productEntity is null)
                return BadRequest("Invalid product");

            return Ok(await _productRepository.UpdateProduct(productEntity));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductEntity))]
        public async Task<IActionResult> DelteProductById(string id)
        {
            return Ok(await _productRepository.DeleteProduct(id));
        }
    }
}
