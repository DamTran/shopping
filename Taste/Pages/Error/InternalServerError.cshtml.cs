using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net;
using Taste.Enums;
using Taste.Utility.Helpers;

namespace Taste.Pages.Error
{
    // TODO: Define information to show
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class InternalServerErrorIndexModel : IndexModel
    {
        public override HttpStatusCode? ResponseHttpStatusCode => HttpStatusCode.InternalServerError;
        public Exception Exception { get; set; }
        public bool HasException => Exception != null;
        public string Path { get; set; }
        public bool HasPath => !string.IsNullOrEmpty(Path);
        public string RequestId { get; set; }
        public bool HasRequestId => !string.IsNullOrEmpty(RequestId);

        public InternalServerErrorIndexModel(ILogger<IndexModel> logger) : base(logger)
        {
        }

        public override void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature == null)
            {
                return;
            }

            Exception = exceptionHandlerPathFeature.Error;
            Path = exceptionHandlerPathFeature.Path;
        }
    }
}