using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Exodus3.Api.Helpers;
using NLog.Extensions.Logging;
using NLog;

namespace Exodus3.Api.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly Logger _logger;

        public ApiExceptionFilter()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            ApiError apiError = null;

            if (context.Exception is ApiException)
            {
                //handle explicit "known" API errors
                var ex = context.Exception as ApiException;
                context.Exception = null;
                apiError = new ApiError(ex.Message);
                apiError.Errors = ex.Errors;

                context.HttpContext.Response.StatusCode = ex.StatusCode;

                //logging
                _logger.Warn($"Application thrown error: {ex.Message}", ex);
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                context.HttpContext.Response.StatusCode = 401;

                //logging 
                _logger.Warn($"Unauthorized Access in Controller Filter when trying to access {context.HttpContext.Request.Path}");
            }
            else
            {
                // Unhandled errors
#if !DEBUG
                var msg = "An unhandled error occurred.";
                string stack = null;
#else
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
#endif

                apiError = new ApiError(msg);
                apiError.Detail = stack;

                context.HttpContext.Response.StatusCode = 500;

                // handle logging here
                _logger.Error(context.Exception, msg);
            }

            // always return a JSON result
            context.Result = new JsonResult(apiError);
           
            return base.OnExceptionAsync(context);
        }
    }
}
