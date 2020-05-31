namespace Blog.Web.ViewModels.Posts.ViewModels
{
    using System;

    using AutoMapper;
    using Blog.Data.Models;
    using Blog.Services.Mapping;

    public class IndexPostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, IndexPostViewModel>()
                .ForMember(
                    source => source.Author,
                    destination => destination.MapFrom(member => member.ApplicationUser.UserName));
        }
    }
}
