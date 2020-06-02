namespace Blog.Web.ViewModels.Tags.ViewModels
{
    using Data.Models;
    using Services.Mapping;

    public class TagViewModel : IMapFrom<Tag>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
