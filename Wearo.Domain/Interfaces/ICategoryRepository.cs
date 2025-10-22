using Wearo.Domain.Entities;

namespace Wearo.Domain.Interfaces;

public interface ICategoryRepository
{
    Task AddAsync(Category category);

    Task<IEnumerable<Category>> GetAllAsync();
}
