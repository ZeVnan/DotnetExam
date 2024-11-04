using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebEC.Models.Authentication
{
    public class Authentication:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("UserName") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller", "Access" },
                        {"Action", "Login" }
                    }
                );
            }
            else
            {
                var controllerName = context.ActionDescriptor.RouteValues["controller"]?.ToString();
                if (controllerName == "HomeAdmin" &&
                    context.HttpContext.Session.GetString("UserRole") == "0")
                {
                    return;
                }
                if (controllerName == "Home" &&
                    context.HttpContext.Session.GetString("UserRole") == "1")
                {
                    return;
                }
            }
        }
    }
}
