namespace Blog.Web.ViewModels.Posts.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Blog.Data.Models;
    using Blog.Services.Mapping;
    using Ganss.XSS;

    public class DetailPostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public string SanitizedContent
            => new HtmlSanitizer().Sanitize(this.Description);

        public IEnumerable<string> Tags { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, DetailPostViewModel>()
                .ForMember(
                    source => source.Tags,
                    destination => destination.MapFrom(member => member.TagPosts.Select(x => x.Tag.Name)))
                .ForMember(
                    source => source.Author,
                    destination => destination.MapFrom(member => member.ApplicationUser.UserName));
        }
    }
}
