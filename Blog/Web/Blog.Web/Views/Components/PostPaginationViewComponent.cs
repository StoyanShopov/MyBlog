namespace Blog.Web.Views.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Blog.Services.Data.Contracts;
    using Blog.Web.ViewModels.Posts.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class PostPaginationViewComponent : ViewComponent
    {
        private readonly IPostsService postsService;

        public PostPaginationViewComponent(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public IViewComponentResult Invoke(int page = 1, int perPage = 1)
        {
            var postsCount = this.postsService.TotalPosts;

            var allPosts = this.postsService
                .GetByPage<IndexPostViewModel>(page, perPage)
                .ToList();

            var pagesCount = (int)Math.Ceiling(postsCount / (decimal)perPage);

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
