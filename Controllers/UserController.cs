using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using SimpleDotNETAPI.Models;
using SimpleDotNETAPI.Interfaces;
using SimpleDotNETAPI.ActionFilters;

namespace SimpleDotNETAPI.Controllers
{

    [Route("/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public UserController(
            IUserRepository userRepository,
            IApplicationRepository applicationRepository,
            ITokenRepository tokenRepository,
            IPasswordService passwordService,
            IMapper mapper
            )
        {
            this._userRepository = userRepository;
            this._applicationRepository = applicationRepository;
            this._tokenRepository = tokenRepository;
            this._passwordService = passwordService;
            this._mapper = mapper;
        }

        [HttpGet("{response_type}/{client_id}/{redirect_uri?}/{scope?}")]
        [AuthorizationScopesActionFilter]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult OAuth2(string response_type, string client_id, string? redirect_uri = null, string scope = "all")
        {
            string responseType = Base64UrlEncoder.Decode(response_type);
            string clientId = Base64UrlEncoder.Decode(client_id);

            Application? application = this._applicationRepository.GetApplicationFromPublicKey(clientId);

            if (application == null)
                return BadRequest("Application does not exists.");

            ViewData["client_id"] = clientId;

            string redirectUri = "";

            if (redirect_uri == null)
            {
                if (application.RedirectURIs == null)
                    return BadRequest("No Redirect URI was provided (1).");

                if (application.RedirectURIs.Count == 0)
                    return BadRequest("No Redirect URI was provided (2).");

                if (application.RedirectURIs.Count != 1)
                    return BadRequest("No Redirect URI was selected (1).");

                redirectUri = application.RedirectURIs.First().URI;
            }
            else if (application.RedirectURIs == null)
                redirectUri = Base64UrlEncoder.Decode(redirect_uri);
            else
            {
                redirectUri = Base64UrlEncoder.Decode(redirect_uri);
                if (!application.RedirectURIs.Any(r => r.URI == redirectUri))
                    return BadRequest("An invalid Redirect URI was provided.");
            }

            ViewData["redirect_uri"] = redirectUri;
            ViewData["scope"] = Base64UrlEncoder.Decode(scope);

            ViewData["LoggedUsers"] = new LoggedUser[]
            {
                new LoggedUser("Comenista", "juni127@live.com", "https://lh3.googleusercontent.com/-YQf1XZdLHy0/AAAAAAAAAAI/AAAAAAAAAAA/AJIwbgbx74VWBDzkFJMpuc-niN1T5YYB_g/s128-c/photo.jpg", "token"),
                new LoggedUser("Reinaldo A. Junior", "reinaldo.apolinario96@gmail.com", "https://lh3.googleusercontent.com/-MkVOAy-iDNI/AAAAAAAAAAI/AAAAAAAAAAA/AJIwbgZlmkzluU7bQ88NuZaavVZPqJPlBw/s128-c/photo.jpg", "token"),
            };
            return View();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult AuthWithToken([FromForm] AuthTokenModel queries)
        {
            // pass the selected access token to the View to be posted in Authorization
            // show user the scopes required by the client application
            return View("Claims");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult AuthWithEmailAndPassword([FromForm] AuthEmailAndPasswordModel queries)
        {
            // if queries has not email or password
            if (queries.Email == null || queries.Password == null)
                return BadRequest("No Email and/or Password provided.");
            // get salt associeted with email (GetUserFromEmail(queries.email))
            User? user = this._userRepository.GetUserFromEmail(queries.Email);
            if (user == null)
                return NotFound(String.Format("No user matching these credentials was found. (Email: {0})", queries.Email));
            // compare hashed passwords
            if (!this._passwordService.PasswordsMatch(queries.Password, user.Password, user.Salt))
                return Unauthorized(String.Format("No user matching these credentials was found. (Email: {0})", queries.Email));
            // generate access token for this device
            List<Scope> scopes = new List<Scope>(){
                new Scope("all")
            };
            TokenObject deviceToken = this._tokenRepository.GenerateAccessToken(user, scopes);
            // save the this device access token, with the user info, on a cookie to be used latter
            TokenController.AddTokenToCookie(HttpContext, deviceToken);
            // pass the generated access token to the View
            // show user the scopes required by the client application
            return View("Claims");
        }

        [HttpPost]
        [AuthorizationScopesActionFilter("write/token")]
        [ProducesResponseType(302)]
        [ProducesResponseType(500)]
        public IActionResult Authorization()
        {
            // get user response authorazing (or not) the client application requested scopes
            // generate authorization token
            // The authorization code must expire shortly after it is issued. The OAuth 2.0 spec recommends a maximum lifetime of 10 minutes, but in practice, most services set the expiration much shorter, around 30-60 seconds.
            // redirect user to callback page with authorization token
            return Redirect("");
        }
    }

    public class AuthTokenModel
    {
        public string? Token { get; set; }
        public string ResponseType { get; set; } = "code";
        public string? ClientId { get; set; }
        public string? RedirectURI { get; set; }
        public string Scope { get; set; } = "all";
    }

    public class AuthEmailAndPasswordModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string ResponseType { get; set; } = "code";
        public string? ClientId { get; set; }
        public string? RedirectURI { get; set; }
        public string Scope { get; set; } = "all";
    }

    public class AuthenticationModel
    {
        public string AccessToken { get; set; }
        public string ResponseType { get; set; } = "code";
        public string? ClientId { get; set; }
        public string? RedirectURI { get; set; }
        public string Scope { get; set; } = "all";
    }

    public struct LoggedUser
    {
        public string DisplayName, Email, Avatar, Token;
        public LoggedUser(string displayName, string email, string avatar, string token)
        {
            DisplayName = displayName;
            Email = email;
            Avatar = avatar;
            Token = token;
        }
    }
}