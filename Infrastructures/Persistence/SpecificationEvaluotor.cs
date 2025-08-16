namespace Persistence;
internal static class SpecificationEvaluotor
{
    public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specifications)
        where TEntity : BaseEntity<TKey>
    {
        var query = inputQuery;

        if (specifications.Criteria is not null)
            query = query.Where(specifications.Criteria);
        
        if (specifications.OrderBy is not null)
            query = query.OrderBy(specifications.OrderBy);

        if (specifications.OrderByDescending is not null)
            query = query.OrderByDescending(specifications.OrderByDescending);

        if (specifications.Includes is not null && specifications.Includes.Count > 0)
            query = specifications.Includes
                .Aggregate(query, (currQuery, inculeExp)
                    => currQuery.Include(inculeExp));

        if (specifications.IsPaginated)
            query = query.Skip(specifications.Skip).Take(specifications.Take);

        return query;
    }
}
