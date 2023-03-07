namespace BookingNet.Domain.Interfaces.Security
{
    public interface IPasswordGenerator
    {
        public string CreateRandomPassword(int PasswordLength = 8);
    }
}