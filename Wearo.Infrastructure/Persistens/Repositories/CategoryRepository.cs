using Microsoft.EntityFrameworkCore;
using Wearo.Domain.Entities;
using Wearo.Domain.Interfaces;

namespace Wearo.Infrastructure.Persistens.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _dbContext.Categories.AsNoTracking().ToListAsync();
    }
}
