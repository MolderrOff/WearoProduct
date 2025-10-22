using FluentResults;
using Wearo.Application.DTOs.Request;
using Wearo.Application.DTOs.Response;
using Wearo.Application.Interfaces;
using Wearo.Domain.Entities;
using Wearo.Domain.Interfaces;

namespace Wearo.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> CreateAsync(CategoryParamsRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.CategoryAttributes))
            return Result.Fail("Category name cannot be empty");

        var category = Category.Create(Guid.CreateVersion7(), request.CategoryAttributes);

        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.CategoryRepository.AddAsync(category);
        await _unitOfWork.CommitTransactionAsync();

        return Result.Ok();
    }

    public async Task<Result<List<CategoryDetailsResponse>>> GetAllAsync()
    {
        var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

        if (!categories.Any())
            return Result.Fail("No categories found");

        var response = categories
            .Select(c => new CategoryDetailsResponse(c.Id, c.CategoryAttributes))
            .ToList();

        return Result.Ok(response);
    }
}

