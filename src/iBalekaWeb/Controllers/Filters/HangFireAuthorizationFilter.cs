using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
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
        //private readonly IHttpContextAccessor _contextAccessor;
        
        //public HangFireAuthorizationFilter(IHttpContextAccessor context)
        //{
        //    _contextAccessor = context;
        //}
        public bool Authorize(DashboardContext context)
        {
            return true;
            //return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;           

        }
    }
}
