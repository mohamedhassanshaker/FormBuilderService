using System.Linq.Expressions;

namespace FormBuilderService.Domain.Interfaces.Repositories.Abstraction
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> CreateRangeAsync(ICollection<TEntity> entities);
        Task<TEntity> GetAsync<TPrimaryKey>(TPrimaryKey id);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null!);
        Task<bool> Update(TEntity newEntity);
        Task<bool> UpdateRange(ICollection<TEntity> newEntities);
    }
}