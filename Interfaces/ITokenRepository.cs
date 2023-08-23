using SimpleDotNETAPI.Models;

namespace SimpleDotNETAPI.Interfaces
{
    public interface ITokenRepository
    {
        ICollection<Token>? GetTokens();
        bool TokenExists(string uid);
        bool TokenIsValid(string uid, string applicationUid);
        User? GetUser(string uid);
        Application? GetApplication(string uid);
        TokenObject GenerateAccessToken(User user, ICollection<Scope> scopes);
        TokenObject GenerateAccessToken(User user, ICollection<Scope> scopes, Application? application);

        Token CreateToken(TokenType tokenType, ICollection<Scope> scopes, Application? application, DateTime dateTime);
        Token SaveToken(TokenType tokenType, ICollection<Scope> scopes, Application? application, DateTime dateTime);
    }
}