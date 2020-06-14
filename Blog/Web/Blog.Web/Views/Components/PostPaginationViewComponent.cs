namespace Blog.Web.Views.Components
{
    using System;

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

        public IViewComponentResult Invoke(int page = 1, int perPage = 9)
        {
            var postsCount = this.postsService.TotalPosts;
            var pagesCount = (int)Math.Ceiling(postsCount / (decimal)perPage);

            var model = new AllPostViewModel
            {
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }
    }
}
