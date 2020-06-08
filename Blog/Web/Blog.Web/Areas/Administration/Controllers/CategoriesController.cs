using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Administration.Controllers
{
    using System.Security.Claims;
    using Common;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;
    using ViewModels.Administration.Categories.InputModels;
    using ViewModels.Administration.Posts.InputModels;

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
