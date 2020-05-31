namespace Blog.Data.Models
{
    using Blog.Data.Common.Models;

    public class PostLike : BaseModel<int>
    {
        public Post Post { get; set; }

        public int PostId { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public bool IsLiked { get; set; }
    }
}
