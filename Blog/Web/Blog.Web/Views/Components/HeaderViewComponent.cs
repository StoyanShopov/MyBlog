namespace Blog.Web.Views.Components
{
    using Blog.Web.ViewModels.Common;
    using Microsoft.AspNetCore.Mvc;

    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(HeaderViewModel model)
        {
            return this.View(model);
        }
    }
}
