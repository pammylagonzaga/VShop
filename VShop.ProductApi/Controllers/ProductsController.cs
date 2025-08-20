using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Service;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var productsDto = await _productService.GetProducts();
            if (productsDto == null)
            {
                return NotFound("No products found.");
            }
            return Ok(productsDto);
        }

        [HttpGet("{id}", Name = "GetProduct")]

        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var productDto = await _productService.GetProductById(id);
            if (productDto == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(productDto);
        }

        [HttpPost]

        public async Task<ActionResult> Post([FromBody] ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Invalid data.");
            }
            await _productService.AddProduct(productDto);

            return new CreatedAtRouteResult("GetProduct", new { id = productDto.Id }, productDto);
        }

        [HttpPut()]

        public async Task<ActionResult> Put([FromBody] ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Invalid data.");
            }

            await _productService.UpdateProduct(productDto);

            return Ok(productDto);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var produtoDto = await _productService.GetProductById(id);

            if (produtoDto == null)
            {
                return NotFound("Product not found.");
            }
                await _productService.RemoveProduct(id);

            return Ok(produtoDto);
        }
    }
}
