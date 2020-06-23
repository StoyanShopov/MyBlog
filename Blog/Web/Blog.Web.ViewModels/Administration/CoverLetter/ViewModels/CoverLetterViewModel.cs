namespace Blog.Web.ViewModels.Administration.CoverLetter.ViewModels
{
    using System;
    using Data.Models;
    using Ganss.XSS;
    using Services.Mapping;

    public class CoverLetterViewModel : IMapFrom<CoverLetter>
    {
        public string Content { get; set; }

        public string SanitizedContent()
        {
            var sanitizer = new HtmlSanitizer();

            sanitizer.AllowedTags.Add("iframe");
            sanitizer.AllowedTags.Add("src");
            sanitizer.AllowedTags.Add("allowfullscreen");

            return sanitizer.Sanitize(this.Content);
        }
    }
}
