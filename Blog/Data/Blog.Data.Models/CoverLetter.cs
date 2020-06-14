namespace Blog.Data.Models
{
    using System;

    using Blog.Data.Common.Models;

    public class CoverLetter : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string ImageUrl { get; set; }
    }
}
