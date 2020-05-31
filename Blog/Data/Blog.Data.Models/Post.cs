namespace Blog.Data.Models
{
    using System.Collections.Generic;

    using Blog.Data.Common.Models;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.PostCategories = new HashSet<PostCategory>();
            this.TagPosts = new HashSet<TagPost>();
            this.Comments = new HashSet<Comment>();
            this.PostLikes = new HashSet<PostLike>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public string ApplicationUserId { get; set; }

        public string ImageUrl { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<PostLike> PostLikes { get; set; }

        public ICollection<PostCategory> PostCategories { get; set; }

        public ICollection<TagPost> TagPosts { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
