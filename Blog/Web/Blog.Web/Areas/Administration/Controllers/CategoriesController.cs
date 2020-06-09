namespace Blog.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Blog.Services.Data.Contracts;
    using Blog.Web.ViewModels.Administration.Categories.InputModels;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCategoryInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.categoriesService.CreateAsync(inputModel.Name);

            return this.RedirectToAction("Create", "Posts");
        }

    }
}
