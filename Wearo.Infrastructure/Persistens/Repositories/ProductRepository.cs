using Microsoft.EntityFrameworkCore;
using Wearo.Domain.Entities;
using Wearo.Domain.Interfaces;

namespace Wearo.Infrastructure.Persistens.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetAllWithFiltersAsync(string? title = null, Guid? categoryId = null)
    {
        var query = _dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.AvailableSizes)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(p => p.Description.Title.Contains(title));

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        return await query.ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
    }

    public void Update(Product product)
    {
        _dbContext.Products.Update(product);
    }

    public void Delete(Product product)
    {
        _dbContext.Products.Remove(product);
    }

    public async Task<Product?> GetByIdAsync(Guid id)
        => await _dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.AvailableSizes)
            .FirstOrDefaultAsync(p => p.Id == id);
}
