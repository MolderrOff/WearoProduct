using Microsoft.AspNetCore.Mvc;
using Wearo.Application.DTOs.Request;
using Wearo.Application.Interfaces;
using FluentResults;

namespace Wearo.API.Controllers;


[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CategoryParamsRequest request)
    {
        var result = await _categoryService.CreateAsync(request);

        if (result.IsFailed)
            return BadRequest(result.Errors);

        return Ok("Category created successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _categoryService.GetAllAsync();

        if (result.IsFailed)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}
