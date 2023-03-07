using BookingNet.Domain.Interfaces.Security;

namespace BookingNet.Infraestructure.Tools.Security
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string CreateRandomPassword(int PasswordLength = 8)
        {
            string _allowedChars = "0123456789abcdefghijkmnñopqrstuvwxyzABCDEFGHJKLMNÑOPQRSTUVWXYZ._/!#@%*+";
            Random randNum = new();
            char[] chars = new char[PasswordLength];
            //int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }

            return new string(chars);
        }
    }
}