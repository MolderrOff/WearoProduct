using Wearo.Domain.Entities;

namespace Wearo.Domain.Interfaces;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetAllWithFiltersAsync(string? title = null, Guid? categoryId = null);

    public Task<Product?> GetByIdAsync(Guid id);

    public Task AddAsync(Product product);

    public void Update(Product product);

    public void Delete(Product product);
}
