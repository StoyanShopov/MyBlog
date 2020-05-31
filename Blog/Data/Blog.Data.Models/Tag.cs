namespace Blog.Data.Models
{
    using System.Collections.Generic;

    using Blog.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.TagPosts = new HashSet<TagPost>();
        }

        public string Name { get; set; }

        public ICollection<TagPost> TagPosts { get; set; }
    }
}
