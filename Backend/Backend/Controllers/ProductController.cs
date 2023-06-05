using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : Controller
    {
        private readonly FSDataContext _fSDataContext;

        public ProductController(FSDataContext fSDataContext)
        {
            _fSDataContext = fSDataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _fSDataContext.products.ToListAsync();
            return Ok(products);

        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product CreateProductRequest)
        {
            CreateProductRequest.Id = Guid.NewGuid();
            await _fSDataContext.products.AddAsync(CreateProductRequest);
            await _fSDataContext.SaveChangesAsync();
            return Ok(CreateProductRequest);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProuctById(Guid id)
        {
            var product = await _fSDataContext.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product UpdateProductReuqest)
        {
            var product = await _fSDataContext.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = UpdateProductReuqest.Name;
            product.Description = UpdateProductReuqest.Description;
            await _fSDataContext.SaveChangesAsync();
            return Ok(product);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProuduct(Guid id)
        {
            var product = await _fSDataContext.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _fSDataContext.products.Remove(product);
            await _fSDataContext.SaveChangesAsync();
            return Ok(product);

        }

        
    }
}
