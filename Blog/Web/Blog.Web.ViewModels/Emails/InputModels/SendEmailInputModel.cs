namespace Blog.Web.ViewModels.Emails.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SendEmailInputModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
