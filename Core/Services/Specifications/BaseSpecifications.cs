using Domain.Models;
namespace Services.Specifications;
public class BaseSpecifications<TEntity, TKey>
    : ISpecifications<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
{
    protected BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }
    public Expression<Func<TEntity, bool>> Criteria { get; private set; }
    public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
    protected void _AddInculdes(Expression<Func<TEntity, object>> include) 
        => Includes.Add(include);
}
