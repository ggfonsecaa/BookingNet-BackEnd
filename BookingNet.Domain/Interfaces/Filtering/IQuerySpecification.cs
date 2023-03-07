using System.Linq.Expressions;

namespace BookingNet.Domain.Interfaces.Filtering
{
    public interface IQuerySpecification<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> GroupBy { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }

        public void AddCriteria(Expression<Func<TEntity, bool>> criteriaExpression);

        public void AddInclude(Expression<Func<TEntity, object>> includeExpression);

        public void AddInclude(string includeString);

        public void ApplyPaging(int skip, int take);

        public void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression);

        public void ApplyGroupBy(Expression<Func<TEntity, object>> groupByExpression);
    }
}