namespace BookingNet.Domain.Interfaces.Security
{
    public interface IHashVerifier
    {
        public bool VerifyHashedPassword(string hashedPassword, string password);
    }
}