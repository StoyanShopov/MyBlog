namespace Blog.Web.Controllers
{
    using System.Diagnostics;

    using Blog.Services.Data.Contracts;
    using Blog.Web.ViewModels;
    using Blog.Web.ViewModels.Posts.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IPostsService postsService;

        public HomeController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public IActionResult Index()
        {
            var latestPosts = this.postsService
                .GetLastCreatedPosts<IndexPostViewModel>();

            return this.View(latestPosts);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Contacts()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
