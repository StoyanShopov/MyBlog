namespace Blog.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Blog.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public Comment()
        {
            this.Replies = new HashSet<Comment>();
        }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string Text { get; set; }

        public int? ParrentCommentId { get; set; }

        public Comment ParrentComment { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public ICollection<Comment> Replies { get; set; }
    }
}
