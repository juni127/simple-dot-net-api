using SimpleDotNETAPI.Data;
using SimpleDotNETAPI.Models;
using SimpleDotNETAPI.Interfaces;

namespace SimpleDotNETAPI.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DataContext _context;

        public TokenRepository(DataContext context)
        {
            this._context = context;
        }

        public ICollection<Token>? GetTokens()
        {
            return this._context.Tokens.OrderBy(t => t.Created).ToList();
        }

        public bool TokenExists(string uid)
        {
            return this._context.Tokens.Any(t => t.UID == uid);
        }

        public bool TokenIsValid(string uid, string applicationUid)
        {
            throw new NotImplementedException();
        }

        public User? GetUser(string uid)
        {
            throw new NotImplementedException();
        }

        public Application? GetApplication(string uid)
        {
            throw new NotImplementedException();
        }

        public Token CreateToken(TokenType tokenType, ICollection<Scope> scopes, Application? application, DateTime dateTime)
        {
            Token newToken = new Token();

            newToken.Type = tokenType;
            foreach (Scope scope in scopes)
                newToken.Scopes.Add(scope);
            newToken.Application = application;
            newToken.TimeToLive = dateTime;

            return newToken;
        }

        public Token SaveToken(TokenType tokenType, ICollection<Scope> scopes, Application? application, DateTime dateTime)
        {
            Token newToken = this.CreateToken(tokenType, scopes, application, dateTime);
            this._context.Tokens.Add(newToken);
            this._context.SaveChanges();
            return newToken;
        }

        public TokenObject GenerateAccessToken(User user, ICollection<Scope> scopes, Application? application)
        {
            Token newRefreshToken = this.CreateToken(TokenType.Refresh, scopes, application, DateTime.Now.AddDays(7));
            Token newAccessToken = this.CreateToken(TokenType.Access, scopes, application, DateTime.Now.AddHours(24));

            newRefreshToken.AccessToken = newAccessToken;
            newAccessToken.RefreshToken = newRefreshToken;

            this._context.Tokens.Add(newAccessToken);

            this._context.SaveChanges();

            TokenObject refreshTokenObject = new TokenObject();
            refreshTokenObject.UID = newRefreshToken.UID;
            refreshTokenObject.TimeToLive = newRefreshToken.TimeToLive;
            refreshTokenObject.Type = newRefreshToken.Type;

            TokenObject accessTokenObject = new TokenObject();
            accessTokenObject.UID = newAccessToken.UID;
            accessTokenObject.TimeToLive = newAccessToken.TimeToLive;
            accessTokenObject.Type = newAccessToken.Type;
            accessTokenObject.Refresh = refreshTokenObject;

            return accessTokenObject;
        }

        public TokenObject GenerateAccessToken(User user, ICollection<Scope> scopes)
        {
            return this.GenerateAccessToken(user, scopes, null);
        }
    }
}