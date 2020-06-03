namespace Blog.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
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


        public IActionResult All(int page = 1, int perPage = 9)
        {
            var postsCount = this.postsService.TotalPosts;

            var allPosts = this.postsService
                .GetByPage<IndexPostViewModel>(page, perPage)
                .ToList();

            var pagesCount = (int) Math.Ceiling(postsCount / (decimal)perPage);

            var model = new AllPostViewModel
            {
                Posts = allPosts,
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }
    }
}
