namespace Blog.Data.Models
{
    using System;

    using Blog.Data.Common.Models;

    public class CoverLetter : BaseDeletableModel<int>
    {
        public string Content { get; set; }
    }
}
