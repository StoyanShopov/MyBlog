namespace Blog.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Blog.Services.Data.Contracts;
    using Blog.Web.Common;
    using Blog.Web.ViewModels.Administration.Posts.InputModels;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class PostsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IPostsService postsService;

        public PostsController(
            ICategoriesService categoriesService,
            IPostsService postsService)
        {
            this.categoriesService = categoriesService;
            this.postsService = postsService;
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

            var postId = await this.postsService
                .CreateAsync(inputModel);

            return this.RedirectToAction("Details", "Posts", new { postId });
        }
    }
}
