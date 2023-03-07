using BookingNet.Domain.Interfaces.Security;

using System.Security.Cryptography;

namespace BookingNet.Infraestructure.Tools.Security
{
    public class HashGenerator : IHashGenerator
    {
        public string HashPassword(string? password)
        {
            byte[] salt;
            byte[] buffer2;

            if (password == null)
            {
                throw new ArgumentNullException("La contraseña no debe estar vacía");
            }

            using (Rfc2898DeriveBytes bytes = new(password, 0x10, 0x3e8, HashAlgorithmName.SHA256))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }

            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);

            return Convert.ToBase64String(dst);
        }
    }
}