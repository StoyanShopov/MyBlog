namespace Blog.Web.ViewModels.Administration.Posts.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreatePostInputModel
    {
        private const string TitleLengthMessage = "Title must be between 4 and 10 (including) symbols!";
        private const string DescriptionErrorMessage = "Description must be at least 250 symbols!";
        private const string ShortDescriptionErrorMessage = "Short descriptionbetween 200 and 250 symbols!";
        private const string TagsErrorMessage = "Tags must be between 4 and 30 (including) symbols!";

        [DataType(DataType.Text)]
        [Required(ErrorMessage = ValidationConstants.NullOrEmptyField)]
        [StringLength(10, MinimumLength = 4, ErrorMessage = TitleLengthMessage)]
        public string Title { get; set; }

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = ValidationConstants.ImageRequired)]
        public IFormFile Image { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = ValidationConstants.NullOrEmptyField)]
        [StringLength(int.MaxValue, MinimumLength = 250, ErrorMessage = DescriptionErrorMessage)]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = ValidationConstants.NullOrEmptyField)]
        [StringLength(200, MinimumLength = 50, ErrorMessage = ShortDescriptionErrorMessage)]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = ValidationConstants.NullOrEmptyField)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = ValidationConstants.NullOrEmptyField)]
        [StringLength(30, MinimumLength = 4, ErrorMessage = TagsErrorMessage)]
        public string Tags { get; set; }
    }
}
