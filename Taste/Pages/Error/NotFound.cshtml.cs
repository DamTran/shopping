using Microsoft.Extensions.Logging;
using System.Net;

namespace Taste.Pages.Error
{
    public class NotFoundIndexModel : IndexModel
    {
        public override HttpStatusCode? ResponseHttpStatusCode => HttpStatusCode.NotFound;

        public NotFoundIndexModel(ILogger<IndexModel> logger) : base(logger)
        {
        }

        public override void OnGet()
        {
        }
    }
}