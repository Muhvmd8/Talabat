namespace Persistence.Repositories;
public class UnitOfWork(StoredDbContext _dbContext) 
    : IUnitOfWork

{
    private readonly Dictionary<string, object> _repositories = [];
    public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
    {
        var typeName = typeof(TEntity).Name;
        //if (_repositories.ContainsKey(typeName))
        //    return (IGenericRepository<TEntity, TKey>)_repositories[typeName];

        if (_repositories.TryGetValue(typeName, out object? value))
            return (IGenericRepository<TEntity, TKey>)value;
        else
        {
            var repository = new GenericRepository<TEntity, TKey>(_dbContext);
            //_repositories.Add(typeName, repository);
            _repositories[typeName] = repository;
            return repository;
        }

    }
    public async Task<int> SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}
