using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Taste
{
    public class ApplicationPageModel<T> : ApplicationPageModel
    {
        public ApplicationPageModel(T bodyModel, string title) : base(title)
        {
            Body = bodyModel;
        }

        public new T Body { get; set; }
    }

    public class ApplicationPageModel : PageModel
    {
        public ApplicationPageModel(string title = null)
        {
            Title = title ?? GetType().Name;
        }

        public string Application => "Taste";

        public string Title { get; }

        public HeaderModel Header => new HeaderModel();

        public NavigationModel Navigation => new NavigationModel();

        public object Body { get; set; }

        public FooterModel Footer => new FooterModel();
    }

    public class HeaderModel
    {
		public string Telephone => "+1 (844) 123 456 78";
		
		public string EmailAddress => "info@demolink.org";
    }

    public class NavigationModel
    {
    }

    public class FooterModel
    {
    }
}