using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Base
{
    public class ResponseHeaderAttribute : ActionFilterAttribute
    {
        private readonly string name;
        private readonly string value;

        public ResponseHeaderAttribute(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add(name, value);
            base.OnActionExecuted(context);
        }
    }
}
