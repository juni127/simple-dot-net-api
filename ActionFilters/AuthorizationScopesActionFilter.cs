using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SimpleDotNETAPI.ActionFilters
{
    public class AuthorizationScopesActionFilter : ActionFilterAttribute
    {
        private readonly string[]? _scopes;

        public AuthorizationScopesActionFilter(params string[] scopes)
        {
            this._scopes = scopes;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this._scopes == null)
                return;
            if (this._scopes.Any(scope => scope == "anonymous"))
                return;
            HttpContext httpContext = filterContext.HttpContext;
            string[]? scopes = (string[]?)httpContext.Items["Scopes"];
            if (scopes == null)
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }
            if (this._scopes.Except(scopes).Any())
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}