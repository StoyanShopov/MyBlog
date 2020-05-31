namespace Blog.Data.Models
{
    using System.Collections.Generic;

    using Blog.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.PostCategories = new HashSet<PostCategory>();
        }

        public string Name { get; set; }

        public ICollection<PostCategory> PostCategories { get; set; }
    }
}
