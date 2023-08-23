using Microsoft.EntityFrameworkCore;

using SimpleDotNETAPI.Data;
using SimpleDotNETAPI.Models;
using SimpleDotNETAPI.Interfaces;
using SimpleDotNETAPI.Controllers;

namespace SimpleDotNETAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            this._context = context;
        }

        public ICollection<User>? GetUsers()
        {
            return this._context.Users.OrderBy(c => c.Created).ToList();
        }

        public bool UserExists(string uid)
        {
            return this._context.Users.Any(u => u.UID == uid); ;
        }

        public User? GetUser(string uid)
        {
            return this._context.Users.Where(u => u.UID == uid).FirstOrDefault();
        }

        public User? GetUserFromEmailAndPassword(string email, string password)
        {
            return this._context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
        }

        public User? GetUserFromToken(TokenObject token)
        {
            if (TokenController.IsTokenValid(this._context.Tokens, token))
                return this._context.Users.Include(u => u.Tokens).Where(u => u.Tokens.Any(t => t.UID == token.UID)).FirstOrDefault();
            return null;
        }

        public User? GetUserFromEmail(string email)
        {
            return this._context.Users.Where(u => u.Email == email).FirstOrDefault();
        }
    }
}