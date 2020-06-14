namespace Blog.Web.ViewModels.Administration.CoverLetter.InputModels
{
    using System;

    public class EditCoverLetterInputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string ImageUrl { get; set; }
    }
}
