namespace BookingNet.Domain.Interfaces.Security
{
    public interface IHashGenerator
    {
        public string HashPassword(string password);
    }
}