namespace Blog.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using Blog.Web.ViewModels.Administration.CoverLetter.InputModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualBasic;
    using Services.Data.Contracts;

    public class CoverLetterController : AdministrationController
    {
        private readonly ICoverLetterService coverLetterService;

        public CoverLetterController(ICoverLetterService coverLetterService)
        {
            this.coverLetterService = coverLetterService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCoverLetterInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.coverLetterService.CreateAsync(model);

            return this.RedirectToAction("About", "Home", new { area = string.Empty });
        }
    }
}
