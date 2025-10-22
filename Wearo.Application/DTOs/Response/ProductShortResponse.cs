namespace Wearo.Application.DTOs.Response;

public record ProductShortResponse(Guid Id, string Title, decimal Price, string Category);