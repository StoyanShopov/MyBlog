namespace Blog.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;
    using ViewModels.Posts.ViewModels;

    public class PostsController : BaseController
    {
        private readonly IPostsService postsService;

        public PostsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public IActionResult Details(int postId)
        {
            var post = this.postsService
                .GetById<DetailPostViewModel>(postId);

            return this.View(post);
        }
    }
}
