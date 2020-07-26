namespace Blog.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Blog.Data.Models;
    using Blog.Services.Data.Contracts;
    using Blog.Web.Common;
    using Blog.Web.ViewModels.Administration.Posts.InputModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Posts.ViewModels;

    [Area("Administration")]
    public class PostsController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(
            ICategoriesService categoriesService,
            IPostsService postsService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.postsService = postsService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            this.ViewData["AllCategories"] = SelectListGenerator
                .GetAllCategories(this.categoriesService);

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["AllCategories"] = SelectListGenerator
                    .GetAllCategories(this.categoriesService);

                return this.View(inputModel);
            }

            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            var postId = await this.postsService
                .CreateAsync(userId, inputModel);

            return this.RedirectToAction("Details", "Posts", new { postId, area = string.Empty });
        }

        public IActionResult Edit(int postId)
        {
            var post = this.postsService.GetById<EditPostInputModel>(postId);

            if (post == null)
            {
                return this.RedirectToAction("All", new { area = string.Empty });
            }

            this.ViewData["AllCategories"] = SelectListGenerator
                .GetAllCategories(this.categoriesService);

            return this.View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPostInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["AllCategories"] = SelectListGenerator
                    .GetAllCategories(this.categoriesService);

                return this.View(inputModel);
            }

            var postId = await this.postsService
                .EditAsync(inputModel);

            return this.RedirectToAction("Details", "Posts", new { postId, area = string.Empty });
        }

        public async Task<IActionResult> Delete(int postId)
        {
            await this.postsService.RemoveAsync(postId);

            return this.RedirectToAction("All");
        }

        public IActionResult All(int page = 1, int perPage = 9)
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
