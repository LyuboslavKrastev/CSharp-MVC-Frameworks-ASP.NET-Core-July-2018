using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace BookLibrary.App.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ConsoleLogger> logger;

        /* use movies/excep to test */

        public ExceptionFilter(ILogger<ConsoleLogger> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            this.logger.LogError($"{context.Exception.GetType()}, {context.Exception.StackTrace}");
        }
    }
}
