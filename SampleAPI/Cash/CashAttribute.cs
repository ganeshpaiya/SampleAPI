using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Cash
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CashAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timeToLive;

        public CashAttribute(int timeToLive)
        {
            this.timeToLive = timeToLive;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
        }
    }
}
