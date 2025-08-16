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

    #region Include
    public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
    protected void _AddInculdes(Expression<Func<TEntity, object>> include)
        => Includes.Add(include);
    #endregion

    #region Order By
    public Expression<Func<TEntity, object>> OrderBy { get; private set; } 
    public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
    protected void _AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) => OrderBy = orderByExpression;
    protected void _AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression) => OrderByDescending = orderByDescExpression;
    #endregion

    #region Pagination
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPaginated { get; set; }
    protected void _ApplyPagination(int pageSize, int pageIndex)
    {
        IsPaginated = true;
        Take = pageSize;
        Skip = (pageIndex - 1) * pageSize;
    } 
    #endregion
}
