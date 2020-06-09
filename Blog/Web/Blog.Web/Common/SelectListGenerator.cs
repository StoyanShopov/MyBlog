namespace Blog.Web.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Blog.Services.Data.Contracts;
    using Blog.Web.ViewModels.Administration.Categories;
    using Blog.Web.ViewModels.Administration.Categories.ViewModels;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SelectListGenerator
    {
        public static IEnumerable<SelectListItem> GetAllCategories(
            ICategoriesService categoriesService)
        {
            var categories = categoriesService
                .GetAll<PostCategoryViewModel>();

            var postCategoryViewModels = categories
                as PostCategoryViewModel[] ?? categories.ToArray();

            // var groups = new List<SelectListGroup>();
            // foreach (var category in postCategoryViewModels)
            // {
            //     groups.Add(new SelectListGroup { Name = category.Name });
            // }
            var result = postCategoryViewModels.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            });

            return result;
        }
    }
}
