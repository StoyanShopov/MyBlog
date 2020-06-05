namespace Blog.Web.ViewModels.Administration.Posts.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using Blog.Data.Models;
    using Blog.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditPostInputModel : IMapFrom<Post>, IHaveCustomMappings
    {
        private const string TitleLengthMessage = "Title must be between 4 and 10 (including) symbols!";
        private const string DescriptionErrorMessage = "Description must be at least 250 symbols!";
        private const string ShortDescriptionErrorMessage = "Short descriptionbetween 200 and 250 symbols!";
        private const string TagsErrorMessage = "Tags must be between 4 and 30 (including) symbols!";

        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = ValidationConstants.NullOrEmptyField)]
        [StringLength(10, MinimumLength = 4, ErrorMessage = TitleLengthMessage)]
        public string Title { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = ValidationConstants.NullOrEmptyField)]
        [StringLength(int.MaxValue, MinimumLength = 250, ErrorMessage = DescriptionErrorMessage)]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = ValidationConstants.NullOrEmptyField)]
        [StringLength(100, MinimumLength = 50, ErrorMessage = ShortDescriptionErrorMessage)]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = ValidationConstants.NullOrEmptyField)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = ValidationConstants.NullOrEmptyField)]
        [StringLength(30, MinimumLength = 4, ErrorMessage = TagsErrorMessage)]
        public string Tags { get; set; }

        public string ImageUrl { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, EditPostInputModel>()
                .ForMember(
                    source => source.Tags,
                    from => from.MapFrom(d => string.Join(", ", d.TagPosts.Select(s => s.Tag.Name))));
        }
    }
}
