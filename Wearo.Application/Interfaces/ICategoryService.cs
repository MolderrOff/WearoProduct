using FluentResults;
using Wearo.Application.DTOs.Request;
using Wearo.Application.DTOs.Response;

namespace Wearo.Application.Interfaces;
public interface ICategoryService
{
    Task<Result> CreateAsync(CategoryParamsRequest request);
    Task<Result<List<CategoryDetailsResponse>>> GetAllAsync();
}