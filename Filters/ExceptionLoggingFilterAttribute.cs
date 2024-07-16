using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace dotnet60_example.Filters
{
    public class ExceptionLoggingFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionLoggingFilterAttribute> _logger;

        public ExceptionLoggingFilterAttribute(ILogger<ExceptionLoggingFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            _logger.LogError(context.Exception, $"{descriptor.ControllerName}/{descriptor.ActionName} Error");
        }
    }
}
