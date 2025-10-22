using System.Net.Http.Headers;
using FluentResults;
using Wearo.Application.DTOs.Request;
using Wearo.Application.DTOs.Response;
using Wearo.Application.Interfaces;
using Wearo.Domain.Entities;
using Wearo.Domain.Interfaces;
using Wearo.Domain.ValueObjects;

namespace Wearo.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> CreateAsync(ProductParamsRequest request)
    {
        //валидация
        var result = Validate(request);

        if (result.IsFailed)
            return Result.Fail(result.Errors.First().Message);

        var product = FabricProductFromRequest(request); 

        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.ProductRepository.AddAsync(product);
        await _unitOfWork.CommitTransactionAsync();

        return Result.Ok();
    }

    public async Task<Result<ProductDetailsResponse>> GetByIdAsync(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

        if (product is null) //
            return Result.Fail("Product not found");

        var response = new ProductDetailsResponse(
            product.Id,
            product.Description.Brand,
            product.Description.Title,
            product.Description.Description,
            product.Description.ArticleNumber,
            product.Price,
            product.Category?.CategoryAttributes ?? string.Empty,
            product.AvailableSizes.Select(s => s.Value));

        return Result.Ok(response);
    }

    public async Task<Result<List<ProductShortResponse>>> GetAllWithFiltersAsync(
        string? title = null,
        Guid? categoryId = null)
    {
        var products = await _unitOfWork.ProductRepository.GetAllWithFiltersAsync(title, categoryId);

        var response = products.Select(product => new ProductShortResponse(
            product.Id,
            product.Description.Title,
            product.Price,
            product.Category.CategoryAttributes ?? string.Empty)).ToList();

        return Result.Ok(response);
    }

    public async Task<Result> UpdateAsync(Guid id, ProductParamsRequest request)
    {
        var result = Validate(request);

        if (result.IsFailed)
            return Result.Fail(result.Errors.First().Message);

        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

        if (product is null)
            return Result.Fail("Product not found");

        var updatedDescription = new ProductDescription(
            request.DescriptionParamsRequest.Brand,
            request.DescriptionParamsRequest.Title,
            request.DescriptionParamsRequest.Description,
            request.DescriptionParamsRequest.ArticleNumber);

        var updatedSizes = request.Sizes?
            .Select(sizeDto => new SizeOption(sizeDto.Size))
            .ToList() ?? new List<SizeOption>();

        product.Update(
            price: request.Price,
            categoryId: request.CategoryId,
            description: updatedDescription,
            sizes: updatedSizes);

        await _unitOfWork.BeginTransactionAsync();
        _unitOfWork.ProductRepository.Update(product);
        await _unitOfWork.CommitTransactionAsync();

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

        if (product is null)
            return Result.Fail("Product not found");

        await _unitOfWork.BeginTransactionAsync();
        _unitOfWork.ProductRepository.Delete(product);
        await _unitOfWork.CommitTransactionAsync();

        return Result.Ok();
    }

    private Product FabricProductFromRequest(ProductParamsRequest request)
    {
        var productId = Guid.CreateVersion7();

        var productDescription = new ProductDescription(
            request.DescriptionParamsRequest.Brand,
            request.DescriptionParamsRequest.Title,
            request.DescriptionParamsRequest.Description,
            request.DescriptionParamsRequest.ArticleNumber);

        var productPrice = request.Price;

        var productCategory = request.CategoryId;

        var productSizes = request.Sizes
            .Select(sizeDto => new SizeOption(sizeDto.Size))
            .ToList() ?? new List<SizeOption>();

        var product = Product.Create(productId, productPrice, productCategory, productDescription, productSizes);

        return product;
    }
    
    private Result Validate(ProductParamsRequest request)
    {
        //Костыльная валидация request
        if (request.Price <= 0)
            return Result.Fail("Incorrect price");

        if (string.IsNullOrWhiteSpace(request.DescriptionParamsRequest.Title))
            return Result.Fail("Incorrect title");

        if (string.IsNullOrWhiteSpace(request.DescriptionParamsRequest.Brand))
            return Result.Fail("Incorrect  brand");

        return Result.Ok();
    }
}
