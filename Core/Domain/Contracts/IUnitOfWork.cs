namespace Domain.Contracts;
public interface IUnitOfWork
{
    IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>;
    //public IGenericRepository<ProductBrand, int> BrandRepository { get; }
    //public IGenericRepository<ProductType, int> TypeRepository { get; }

    Task<int> SaveChangesAsync();
}
