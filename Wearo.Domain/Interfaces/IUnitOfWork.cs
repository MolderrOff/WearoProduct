namespace Wearo.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }

    ICategoryRepository CategoryRepository { get; }

    Task SaveChangesAsync(bool applySoftDeleted = true);

    bool HasActiveTransaction { get; }

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}

