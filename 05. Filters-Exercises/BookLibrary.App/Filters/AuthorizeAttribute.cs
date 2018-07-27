using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BookLibrary.App.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IPageFilter, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
              if (!this.userIsAuthorized(context))
            {
                var returnUrl = context.HttpContext.Request.Path.HasValue ? context.HttpContext.Request.Path.Value : "/";
                context.Result = new RedirectToActionResult("Login", "Users", new { returnUrl });
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!this.userIsAuthorized(context))
            {
                var returnUrl = context.HttpContext.Request.Path.HasValue ? context.HttpContext.Request.Path.Value : "/";
                context.Result = new RedirectToActionResult("Login", "Users", new { returnUrl });
            }
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            if (!this.userIsAuthorized(context))
            {
                var returnUrl = context.HttpContext.Request.Path.HasValue ? context.HttpContext.Request.Path.Value : "/";
                context.Result = new RedirectToActionResult("Login", "Users", new { returnUrl });
            }
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (!this.userIsAuthorized(context))
            {
                var returnUrl = context.HttpContext.Request.Path.HasValue ? context.HttpContext.Request.Path.Value : "/";
                context.Result = new RedirectToActionResult("Login", "Users", new { returnUrl });
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context){}

        public bool userIsAuthorized(FilterContext context)
        {
            var contextAccessor = context.HttpContext.RequestServices.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
            var username = SessionExtensions.GetString(contextAccessor.HttpContext.Session, "_$CurrentUserSessionKey$_");

            if (username == null)
            {
                return false;
            }

            return true;
        }
    }
}
