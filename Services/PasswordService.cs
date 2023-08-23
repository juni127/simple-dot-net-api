using System.Security.Cryptography;
using System.Text;
using SimpleDotNETAPI.Interfaces;

namespace SimpleDotNETAPI.Services
{
    public class PasswordService : IPasswordService
    {
        public PasswordService()
        {
        }

        public string HashPasword(string password, byte[] salt)
        {
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                HashSettings._hashIterations,
                HashSettings._hashAlgorithm,
                HashSettings._hashKeySize
            );
            return Convert.ToHexString(hash);
        }

        public string HashPasword(string password, string salt)
        {
            return this.HashPasword(password, Encoding.UTF8.GetBytes(salt));
        }

        public string NewHashPasword(string password, out string salt)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(HashSettings._hashKeySize);
            salt = Convert.ToHexString(saltBytes);
            return this.HashPasword(password, saltBytes);
        }

        public bool PasswordsMatch(string password, string hash, string salt)
        {
            string hashToCompare = this.HashPasword(password, salt);
            return CryptographicOperations.FixedTimeEquals(Convert.FromHexString(hashToCompare), Convert.FromHexString(hash));
        }
    }

    static class HashSettings
    {
        public static int _hashIterations = 1000000;
        public static int _hashKeySize = 128;
        public static HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;
    }
}