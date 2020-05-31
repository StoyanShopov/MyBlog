namespace Blog.Web.ViewModels.Administration.Categories
{
    using Blog.Data.Models;
    using Blog.Services.Mapping;

    public class PostCategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
