namespace Blog.Data.Models
{
    using Blog.Data.Common.Models;

    public class Email : BaseDeletableModel<int>
    {
        public string RecipientEmail { get; set; }
    }
}
