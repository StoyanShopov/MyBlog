namespace Blog.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Blog.Services.Data.Contracts;
    using Blog.Web.ViewModels;
    using Blog.Web.ViewModels.Administration.CoverLetter.ViewModels;
    using Blog.Web.ViewModels.Posts.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IPostsService postsService;
        private readonly ICoverLetterService coverLetterService;

        public HomeController(IPostsService postsService, ICoverLetterService coverLetterService)
        {
            this.postsService = postsService;
            this.coverLetterService = coverLetterService;
        }

        public IActionResult Index()
        {
            var latestPosts = this.postsService
                .GetLastCreatedPosts<IndexPostViewModel>();

            return this.View(latestPosts);
        }

        public IActionResult About()
        {
            var coverLetter = this.coverLetterService
                .Get<CoverLetterViewModel>();

            return this.View(coverLetter);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
