using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Taste
{
    public class ApplicationPageModel<T> : ApplicationPageModel
    {
        public ApplicationPageModel(T bodyModel, string title) : base(title)
        {
            Body = bodyModel;
        }

        public HeaderModel Header { get; set; }

        public NavigationModel Navigation { get; set; }

        public T Body { get; set; }

        public FooterModel Footer { get; set; }
    }

    public class ApplicationPageModel : PageModel
    {
        public ApplicationPageModel(string title)
        {
            Title = title;
        }

        public string Application => "Taste";

        public string Title { get; }
    }

    public class HeaderModel
    {
    }

    public class NavigationModel
    {
    }

    public class FooterModel
    {
    }
}