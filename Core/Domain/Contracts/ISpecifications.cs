namespace Domain.Contracts;
public interface ISpecifications<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
{
    // Property signature for each dynamic part in the query.
    // 1. Where 
    public Expression<Func<TEntity, bool>> Criteria { get; }
    public List<Expression<Func<TEntity, object>>> Includes { get; }
    public Expression<Func<TEntity, object>> OrderBy { get; }
    public Expression<Func<TEntity, object>> OrderByDescending { get; }
    public int Take { get; }
    public int Skip { get; }
    public bool IsPaginated { get; set; }
}
