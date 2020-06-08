namespace Blog.Web.Areas.Administration.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Blog.Services.Data.Contracts;
    using Blog.Web.Common;
    using Blog.Web.ViewModels.Administration.Posts.InputModels;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var postId = await this.postsService
                .CreateAsync(userId, inputModel);

            return this.RedirectToAction("Details", "Posts", new { postId });
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
    }
}
