namespace Domain.Contracts;
public interface IGenericRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
    Task Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<int> Count(ISpecifications<TEntity, TKey> specifications);
}
