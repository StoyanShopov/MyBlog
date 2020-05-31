namespace Blog.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : BaseController
    {
        public IActionResult Details()
        {
            return this.View();
        }
    }
}
