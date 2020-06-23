namespace Blog.Web.ViewModels.Administration.CoverLetter.InputModels
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class EditCoverLetterInputModel : IMapFrom<CoverLetter>
    {
        public string Content { get; set; }
    }
}
