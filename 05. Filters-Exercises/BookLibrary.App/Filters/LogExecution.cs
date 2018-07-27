using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Diagnostics;
using System.Linq;

namespace BookLibrary.App.Filters
{
    public class LogExecution : IPageFilter, IActionFilter
    {
        private ILogger logger;
        private Stopwatch stopwatch;

        public LogExecution(ILoggerFactory loggerFactory, Stopwatch stopwatch)
        {
            this.logger = loggerFactory.CreateLogger<ConsoleLogger>();
            this.stopwatch = stopwatch;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            this.LogEnteringMessage(context.HttpContext.Request.Method, context.ActionDescriptor.DisplayName, context.ModelState.IsValid);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            this.LogLeavingMessage(context.HttpContext.Request.Method, context.Controller.ToString(), context.ActionDescriptor.DisplayName);
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            this.LogEnteringMessage(context.HttpContext.Request.Method, context.ActionDescriptor.DisplayName, context.ModelState.IsValid);
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            var page = context.ActionDescriptor.PageTypeInfo.Name.Split('_').Last();
            this.LogLeavingMessage(context.HttpContext.Request.Method, page, context.ActionDescriptor.DisplayName);
        }


        private void LogEnteringMessage(string httpMethod, string actionName, bool isModelStateValid)
        {
            var stopWatch = new Stopwatch();
            this.logger.LogInformation($"Executing {httpMethod} with action {actionName}.");
            this.logger.LogInformation($"Model state: {(isModelStateValid ? "valid" : "invalid")}");
      
            this.stopwatch.Restart();
        }

        private void LogLeavingMessage(string httpMethod, string controllerName, string actionName)
        {
            this.stopwatch.Stop();
            var time = stopwatch.Elapsed.TotalSeconds;
            this.logger.LogInformation($"Executed {httpMethod} {controllerName}.{actionName} in {time} s.");
        }


        public void OnPageHandlerSelected(PageHandlerSelectedContext context){}
    }
}
