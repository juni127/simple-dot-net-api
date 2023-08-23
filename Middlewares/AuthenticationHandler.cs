using System.Text.Json;
using SimpleDotNETAPI.Models;
using SimpleDotNETAPI.Controllers;

namespace SimpleDotNETAPI.Middlewares
{
    public class AuthenticationHandler

    {

        private readonly RequestDelegate _next;
        private readonly string _realm;
        public AuthenticationHandler(string realm, RequestDelegate next)
        {
            this._next = next;
            this._realm = realm;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // get access tokens of users that are logged to the api from cookies
            List<TokenObject>? accessTokensFromCookie = TokenController.GetTokensFromCookie(context);
            // set the users to context.Items["LoggedUsers"]
            context.Items["AccessTokens"] = accessTokensFromCookie;

            // try to get authorization/access/refresh token from Header
            var token = "";
            if (context.Request.Headers.ContainsKey("Authorization"))
                token = context.Request.Headers["Authorization"];
            else
            { // if token not present
                // insert anonymous scope on context (context.Items["Scopes"] = { "anonymous" };)
                context.Items["Scopes"] = new[] { "anonymous" };
                // call next Middleware
                await this._next(context);
                // return to avoid unwanted code execution
                return;
            }
            // validade token
            // -> if not valid return 401 Unauthorized
            // find which type of token it is
            // -> if authorization or refresh token return access token
            // -> else get scopes
            // -> insert scopes on context (context.Items["Scopes"] = scopes;)
            // call next Middleware
            await this._next(context);
        }
    }
}