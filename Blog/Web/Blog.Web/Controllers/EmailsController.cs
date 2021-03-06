﻿namespace Blog.Web.Controllers
{
    using System.Threading.Tasks;

    using Blog.Services.Messaging;
    using Blog.Web.ViewModels.Emails.InputModels;
    using Blog.Web.ViewModels.Emails.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailSender sendGridEmailSender;

        public EmailsController(IEmailSender sendGridEmailSender)
        {
            this.sendGridEmailSender = sendGridEmailSender;
        }

        [HttpPost]
        public async Task<ActionResult> Post(SendEmailInputModel inputModel)
        {
            const string myEmail = "stoyanshopov032@gmail.com";

            await this.sendGridEmailSender.SendEmailAsync(
                inputModel.Email,
                inputModel.Name,
                myEmail,
                inputModel.Subject,
                inputModel.Message);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
