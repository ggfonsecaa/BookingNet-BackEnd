using BookingNet.Domain.Aggregates.UserAggregate;

namespace BookingNet.Domain.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(User user);
    }
}