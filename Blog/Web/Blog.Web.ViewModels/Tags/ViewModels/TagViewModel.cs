namespace Blog.Web.ViewModels.Tags.ViewModels
{
    using Blog.Data.Models;
    using Blog.Services.Mapping;

    public class TagViewModel : IMapFrom<Tag>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
