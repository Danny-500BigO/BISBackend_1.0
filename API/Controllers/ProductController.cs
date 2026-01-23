
using Microsoft.AspNetCore.Mvc;
using BakeryApi.Application.Services;
using BakeryApi.Domain.Entities;


namespace BakeryApi.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService; //dependecy injection of service

        public ProductController(IProductService productService)
            {
                _productService = productService;
            }

        // POST api/product
        [HttpPost]
        public async Task<ActionResult<Product> > PostProduct([FromBody] Product product)
        {
            if(product == null)
            {
                return BadRequest("Product data is required");

            }

            var createdProduct = await _productService.AddProductAsync(product);

            return CreatedAtAction(nameof(GetProduct), new {id = createdProduct.Product_Id}, createdProduct);
            



        }

        // This is another action to get a product by ID (just for completeness)
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }


}
