using Microsoft.AspNetCore.Mvc;
using Wearo.Application.DTOs.Request;
using Wearo.Application.Interfaces;

namespace Wearo.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/products
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? title = null, [FromQuery] Guid? categoryId = null)
    {
        var result = await _productService.GetAllWithFiltersAsync(title, categoryId);

        return Ok(result.Value);
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _productService.GetByIdAsync(id);

        if (result.IsFailed)
            return NotFound(result.Errors.First().Message);

        return Ok(result.Value);
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ProductParamsRequest request)
    {
        var result = await _productService.CreateAsync(request);

        if (result.IsFailed)
            return BadRequest(result.Errors.First().Message);

        return Ok();
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] ProductParamsRequest request)
    {
        var result = await _productService.UpdateAsync(id, request);

        if (result.IsFailed)
            return NotFound(result.Errors.First().Message);

        return NoContent();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _productService.DeleteAsync(id);

        if (result.IsFailed)
            return NotFound(result.Errors.First().Message);

        return NoContent();
    }
}
