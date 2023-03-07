using BookingNet.Domain.Interfaces.Filtering;

using System.Linq.Expressions;

namespace BookingNet.Application.DataFilters
{
    public class QuerySpecification<TEntity> : IQuerySpecification<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
        public Expression<Func<TEntity, object>> GroupBy { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;


        public QuerySpecification()
        {

        }

        public QuerySpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public void AddCriteria(Expression<Func<TEntity, bool>> criteriaExpression) 
        {
            Criteria = criteriaExpression;
        }

        public virtual void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        public virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        public virtual void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        public virtual void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public virtual void ApplyGroupBy(Expression<Func<TEntity, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }
    }
}