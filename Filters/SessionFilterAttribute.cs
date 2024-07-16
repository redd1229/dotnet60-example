using dotnet60_example.Enums;
using dotnet60_example.Helpers.Extensions;
using dotnet60_example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace dotnet60_example.Filters
{
    public class SessionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userSession = context.HttpContext.Session.GetObject<UserSession>("UserSession");

            if (userSession == null)
            {
                if (IsAjaxCall(context))
                {
                    context.HttpContext.Response.StatusCode = (int)CustomStatusCode.SessionTimeout;
                }
                else
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Login",
                        action = "Logout"
                    }));
                }
            }
        }

        private static bool IsAjaxCall(ActionExecutingContext context)
        {
            return context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
        }
    }
}
