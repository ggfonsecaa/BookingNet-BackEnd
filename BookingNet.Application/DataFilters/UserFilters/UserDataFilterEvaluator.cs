using BookingNet.Application.DataFilters.Extensions;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Interfaces.Filtering;

using System.Linq.Expressions;

namespace BookingNet.Application.DataFilters.UserFilters
{
    public class UserDataFilterEvaluator
    {
        private readonly IQuerySpecification<User> _specification;
        private Expression<Func<User, bool>>? _filter = null;

        public UserDataFilterEvaluator(IQuerySpecification<User> specification)
        {
            _specification = specification;
        }

        public Expression<Func<User, bool>>? GetCriteria(User user)
        {
            if (user.Id != 0) 
            {
                _specification.AddCriteria(x => x.Id == user.Id);
                _filter = _specification.Criteria;
            }

            if (!string.IsNullOrEmpty(user.UserName)) 
            {
                _specification.AddCriteria(x => x.UserName == user.UserName);
                _filter = _filter is null ? _specification.Criteria : _filter.And(_specification.Criteria);
            }

            if (!string.IsNullOrEmpty(user.UserEmail))
            {
                _specification.AddCriteria(x => x.UserEmail == user.UserEmail);
                _filter = _filter is null ? _specification.Criteria : _filter.And(_specification.Criteria);
            }

            return _filter;
        } 
    }
}