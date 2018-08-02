using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DataValidations.PhoneNumber.API.Filters
{
    public class ThrottlingAttribute : ActionFilterAttribute
    {
        public string Name { get; set; } = "RequestThrottling";
        public double MilliSeconds { get; set; } = 100;
        public string Message { get; set; } = "Access Blocked due to Throttling.";

        private static MemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions());

        public override void OnActionExecuting(ActionExecutingContext c)
        {
            var key = string.Concat(Name, "-", c.HttpContext.Request.HttpContext.Connection.RemoteIpAddress);

            if (!Cache.TryGetValue(key, out bool entry))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMilliseconds(MilliSeconds));

                Cache.Set(key, true, cacheEntryOptions);
            }
            else
            {
                if (string.IsNullOrEmpty(Message))
                    Message = "Access Blocked due to Throttling.";

                c.Result = new ContentResult { Content = Message };
                c.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
        }

    }


}
