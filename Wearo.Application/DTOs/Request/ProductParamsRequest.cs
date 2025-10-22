namespace Wearo.Application.DTOs.Request;

public record ProductParamsRequest(decimal Price,
    Guid CategoryId,
    ProductDescriptionParamsRequest DescriptionParamsRequest,
    IEnumerable<SizeOptionParamsRequest> Sizes);
