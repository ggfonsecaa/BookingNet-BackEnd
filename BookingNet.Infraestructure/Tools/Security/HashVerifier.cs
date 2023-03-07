using BookingNet.Domain.Interfaces.Security;

using System.Security.Cryptography;

namespace BookingNet.Infraestructure.Tools.Security
{
    public class HashVerifier : IHashVerifier
    {
        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;

            if (hashedPassword == null)
            {
                return false;
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            byte[] src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }

            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);

            using (Rfc2898DeriveBytes bytes = new(password, dst, 0x3e8, HashAlgorithmName.SHA256))
            {
                buffer4 = bytes.GetBytes(0x20);
            }

            return buffer3.SequenceEqual(buffer4);
        }
    }
}