using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AutoMapper;
using SimpleDotNETAPI.Models;
using SimpleDotNETAPI.Interfaces;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace SimpleDotNETAPI.Controllers
{
    public class TokenController : Controller
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IMapper _mapper;

        public TokenController(ITokenRepository tokenRepository, IMapper mapper)
        {
            this._tokenRepository = tokenRepository;
            this._mapper = mapper;
        }

        public static bool IsTokenValid(DbSet<Token> context, TokenObject token)
        {
            Token? tokenEntry = context.Where(t => t.UID == token.UID && t.TimeToLive < DateTime.Now).FirstOrDefault();
            return (tokenEntry != null);
        }

        public static TokenObject RefreshToken(TokenObject token)
        {
            throw new NotImplementedException();
        }

        public static List<TokenObject>? GetTokensFromCookie(HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey("Access-Tokens") && context.Request.Cookies["Access-Tokens"] != null)
            {
                string tokens = context.Request.Cookies["Access-Tokens"];
                return JsonSerializer.Deserialize<List<TokenObject>>(tokens);
            }
            return null;
        }

        public static void AddTokenToCookie(HttpContext context, TokenObject token)
        {
            List<TokenObject>? tokens = TokenController.GetTokensFromCookie(context) ?? new List<TokenObject>();

            tokens.Add(token);

            string tokensToString = JsonSerializer.Serialize<List<TokenObject>>(tokens);

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(7);
            context.Response.Cookies.Append("Access-Tokens", tokensToString, cookieOptions);
        }
    }
}