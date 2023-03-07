using BookingNet.Domain.Aggregates;
using BookingNet.Domain.Entities;
using BookingNet.Domain.Interfaces;
using BookingNet.Infraestructure.DataContext;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace BookingNet.Infraestructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, IAggregateRoot
    {
        private readonly BookingNetDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(BookingNetDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Delete(int id)
        {
            TEntity entity = _dbSet.Find(id);
            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(int id)
        {
            TEntity entity = await _dbSet.FindAsync(id);
            Delete(entity);
        }

        public IEnumerable<TEntity>? GetAll()
        {
            IQueryable<TEntity> query = _dbSet;

            return query.ToList();
        }

        public async Task<IEnumerable<TEntity>>? GetAllAsync()
        {
            IQueryable<TEntity> query = _dbSet;
            return await query.ToListAsync();
        }

        public TEntity? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity>? GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IEnumerable<TEntity>? GetByQuery(Expression<Func<TEntity, bool>>? filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", bool asTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (asTracking)
            {
                query = query.AsTracking();
            }
            else
            {
                query = query.AsNoTracking();
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async Task<IEnumerable<TEntity>>? GetByQueryAsync(Expression<Func<TEntity, bool>>? filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", bool asTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (asTracking)
            {
                query = query.AsTracking();
            }
            else
            {
                query = query.AsNoTracking();
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public TEntity? Insert(TEntity entity)
        {
            _dbSet.Add(entity);

            return entity;
        }

        public async Task<TEntity>? InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            return entity;
        }

        public TEntity? Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public TEntity? Update(int id, JsonPatchDocument<TEntity> entity)
        {
            TEntity patchEntity = _dbSet.Find(id);
            entity.ApplyTo(patchEntity);

            return patchEntity;
        }

        public async Task<TEntity>? UpdateAsync(int id, JsonPatchDocument<TEntity> entity)
        {
            TEntity patchEntity = await _dbSet.FindAsync(id);
            entity.ApplyTo(patchEntity);

            return patchEntity;
        }
    }
}