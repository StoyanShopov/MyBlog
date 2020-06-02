namespace Blog.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Blog.Services.Messaging;
    using Blog.Web.ViewModels.Emails.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Emails.ViewModels;

    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly IEmailSender sendGridEmailSender;

        public LikesController(IEmailSender sendGridEmailSender)
        {
            this.sendGridEmailSender = sendGridEmailSender;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseEmailModel>> Post(SendEmailInputModel inputModel)
        {
            const string myEmail = "stoyanshopov032@gmail.com";

            await this.sendGridEmailSender.SendEmailAsync(
                inputModel.Email,
                inputModel.Name,
                myEmail,
                inputModel.Subject,
                inputModel.Message);

            return new ResponseEmailModel
            {
                Status = "200",
            };
        }
    }
}
