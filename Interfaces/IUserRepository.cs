using SimpleDotNETAPI.Models;

namespace SimpleDotNETAPI.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User>? GetUsers();
        bool UserExists(string uid);
        User? GetUser(string uid);
        User? GetUserFromEmailAndPassword(string email, string password);
        User? GetUserFromToken(TokenObject token);

        User? GetUserFromEmail(string email);
    }
}