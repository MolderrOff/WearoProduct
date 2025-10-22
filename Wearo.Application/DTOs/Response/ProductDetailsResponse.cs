using Wearo.Domain.ValueObjects;

namespace Wearo.Application.DTOs.Response;

public record ProductDetailsResponse(
    Guid Id,
    string Brand,
    string Title,
    string? Description,
    string? ArticleNumber,
    decimal Price,
    string Category,
    IEnumerable<string> Sizes);
