using FluentResults;
using Wearo.Application.DTOs.Request;
using Wearo.Application.DTOs.Response;

namespace Wearo.Application.Interfaces;
public interface IProductService
{
    Task<Result> CreateAsync(ProductParamsRequest request);
    Task<Result> DeleteAsync(Guid id);
    Task<Result<List<ProductShortResponse>>> GetAllWithFiltersAsync(string? title = null, Guid? categoryId = null);
    Task<Result<ProductDetailsResponse>> GetByIdAsync(Guid id);
    Task<Result> UpdateAsync(Guid id, ProductParamsRequest request);
}