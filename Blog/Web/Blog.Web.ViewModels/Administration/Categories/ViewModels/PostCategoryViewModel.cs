namespace Blog.Web.ViewModels.Administration.Categories.ViewModels
{
    using Data.Models;
    using Services.Mapping;

    public class PostCategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
