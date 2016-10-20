using Hangfire.Dashboard;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace iBalekaWeb.Controllers.Filters
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            if (httpContext.Authentication.HttpContext.User.Identity.IsAuthenticated)
                return httpContext.Authentication.HttpContext.User.Identity.IsAuthenticated;
            else
                return false;
        }
    }
}
