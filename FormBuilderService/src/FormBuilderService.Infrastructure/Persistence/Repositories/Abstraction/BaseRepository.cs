using FormBuilderService.Domain.Interfaces.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Utilities.Logging.Services;
using System.Linq.Expressions;

namespace FormBuilderService.Infrastructure.Persistence.Repositories.Abstraction
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> CreateAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);

                return await _context.SaveChangesAsync() > 0;
            }

            catch (Exception ex)
            {
                LogService.LogError(ex.ToString());
            }

            return false;

        }

        public virtual async Task<bool> CreateRangeAsync(ICollection<TEntity> entities)
        {
            try
            {
                await _context.Set<TEntity>().AddRangeAsync(entities);

                return await _context.SaveChangesAsync() > 0;
            }

            catch (Exception ex)
            {
                LogService.LogError(ex.ToString());
            }

            return false;
        }

        public virtual async Task<TEntity> GetAsync<TPrimaryKey>(TPrimaryKey id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            return entity!;
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null!)
        {
            var query = _context.Set<TEntity>().AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        public virtual async Task<bool> Update(TEntity newEntity)
        {
            try
            {
                _context.Set<TEntity>().Update(newEntity);

                return await _context.SaveChangesAsync() > 0;
            }

            catch (Exception ex)
            {
                LogService.LogError(ex.ToString());
            }

            return false;
        }

        public virtual async Task<bool> UpdateRange(ICollection<TEntity> newEntities)
        {
            try
            {
                _context.Set<TEntity>().UpdateRange(newEntities);

                return await _context.SaveChangesAsync() > 0;
            }

            catch (Exception ex)
            {
                LogService.LogError(ex.ToString());
            }

            return false;
        }
    }
}