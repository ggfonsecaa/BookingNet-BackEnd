using BookingNet.Domain.Aggregates;
using BookingNet.Domain.Entities;

using Microsoft.AspNetCore.JsonPatch;

using System.Linq.Expressions;

namespace BookingNet.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, IAggregateRoot
    {
        public void Delete(int id);
        public void Delete(TEntity entity);
        public Task DeleteAsync(int id);
        public IEnumerable<TEntity>? GetAll();
        public Task<IEnumerable<TEntity>>? GetAllAsync();
        public TEntity? GetById(int id);
        public Task<TEntity>? GetByIdAsync(int id);
        public IEnumerable<TEntity>? GetByQuery(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "",
            bool asTracking = false);
        public Task<IEnumerable<TEntity>>? GetByQueryAsync(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "",
            bool asTracking = false);
        public TEntity? Insert(TEntity entity);
        public Task<TEntity>? InsertAsync(TEntity entity);
        public TEntity? Update(TEntity entity);
        public TEntity? Update(int id, JsonPatchDocument<TEntity> entity);
        public Task<TEntity>? UpdateAsync(int id, JsonPatchDocument<TEntity> entity);
    }
}