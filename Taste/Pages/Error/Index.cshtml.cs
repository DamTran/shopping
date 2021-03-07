using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net;
using Taste.Utility.Enums;
using Taste.Utility.Helpers;

namespace Taste.Pages.Error
{
    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class IndexModel : ApplicationPageModel
    {
        public override string Title => ResponseHttpStatusCode == null ? "Error" : StringHelpers.SplitCamelCase(ResponseHttpStatusCode.ToString());

        public virtual HttpStatusCode? ResponseHttpStatusCode { get; }

        public ErrorCodeEnum ErrorCode { get; set; }

        public string Path { get; set; }

        public Exception Exception { get; set; }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public virtual void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            // https://andrewlock.net/creating-a-custom-error-handler-middleware-function/
            // Try and retrieve the error from the ExceptionHandler middleware
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exceptionDetails == null)
            {
                return;
            }

            //Path = exceptionDetails.Path;

            var exception = exceptionDetails.Error;

            // Should always exist, but best to be safe!
            if (exception != null)
            {
                HttpContext.Response.ContentType = "application/problem+json";

                // Get the details to display, depending on whether we want to expose the raw exception
                var title = "An error occured: " + exception.Message;
                var details = exception.ToString();

                var problem = new ProblemDetails
                {
                    Status = 500,
                    Title = title,
                    Detail = details,
                };

                // This is often very handy information for tracing the specific request
                var traceId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
                if (traceId != null)
                {
                    problem.Extensions["traceId"] = traceId;
                }

                //Serialize the problem details object to the Response as JSON (using System.Text.Json)
                //var stream = HttpContext.Response.Body;
                //await JsonSerializer.SerializeAsync(stream, problem);
            }
        }
    }
}