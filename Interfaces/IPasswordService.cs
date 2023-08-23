namespace SimpleDotNETAPI.Interfaces
{
    public interface IPasswordService
    {
        string HashPasword(string password, byte[] salt);

        string HashPasword(string password, string salt);

        string NewHashPasword(string password, out string salt);

        public bool PasswordsMatch(string password, string hash, string salt);
    }
}