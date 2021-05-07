using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Utils
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                Log.Logger.Error(exception.InnerExceptionToLog, String.Empty);
                context.Result = new ObjectResult(exception.MessageToClient)
                {
                    StatusCode = exception.Status,                    
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
