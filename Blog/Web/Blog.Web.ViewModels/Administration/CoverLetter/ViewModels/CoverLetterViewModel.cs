namespace Blog.Web.ViewModels.Administration.CoverLetter.ViewModels
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class CoverLetterViewModel : IMapFrom<CoverLetter>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string ImageUrl { get; set; }
    }
}
