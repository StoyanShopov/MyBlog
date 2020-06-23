namespace Blog.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using Blog.Web.ViewModels.Administration.CoverLetter.InputModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualBasic;
    using Services.Data.Contracts;
    using ViewModels.Administration.CoverLetter.ViewModels;

    public class CoverLetterController : AdministrationController
    {
        private readonly ICoverLetterService coverLetterService;

        public CoverLetterController(ICoverLetterService coverLetterService)
        {
            this.coverLetterService = coverLetterService;
        }

        public IActionResult Add()
        {
            var cv = this.coverLetterService.Get<CoverLetterViewModel>();

            if (cv != null)
            {
                return this.RedirectToAction("Edit", "CoverLetter", new { area = "Administration" });
            }

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

        public IActionResult Edit()
        {
            var coverLetter = this.coverLetterService.Get<EditCoverLetterInputModel>();

            return this.View(coverLetter);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCoverLetterInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.coverLetterService.EditAsync(model);

            return this.RedirectToAction("About", "Home", new { area = string.Empty });
        }
    }
}
