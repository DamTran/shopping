using Microsoft.Extensions.Logging;

namespace Taste.Pages.PrivacyPolicy
{
    public class IndexModel : ApplicationPageModel
    {
        public override string Title => "Privacy Policy";

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}