using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Interfaces;
using ProductManagementAPI.Models;
using System.Text;

namespace ProductManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync([FromQuery] int page,
       [FromQuery] int pageSize)
    {
       var products = await _productService.GetAllAsync(page, pageSize);

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> ObtainById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        return Ok(product);    
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(Product product)
    {
        var createProduct = await _productService.CreateAsync(product);

        return CreatedAtAction(nameof(ObtainById), new { id = createProduct.Id }, createProduct);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);

        return NoContent();
    }
}
