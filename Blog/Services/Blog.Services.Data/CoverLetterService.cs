namespace Blog.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Tracing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Blog.Data.Common.Repositories;
    using Blog.Data.Models;
    using Blog.Services.Data.Contracts;
    using Blog.Services.Mapping;
    using Blog.Web.ViewModels.Administration.CoverLetter.InputModels;

    public class CoverLetterService : ICoverLetterService
    {
        private readonly IDeletableEntityRepository<CoverLetter> coverLetterRepository;

        public CoverLetterService(IDeletableEntityRepository<CoverLetter> coverLetterRepository)
        {
            this.coverLetterRepository = coverLetterRepository;
        }

        public async Task CreateAsync(CreateCoverLetterInputModel inputModel)
        {
            var coverLetter = new CoverLetter
            {
                Title = inputModel.Title,
                Content = inputModel.Content,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                ImageUrl = inputModel.ImageUrl,
            };

            await this.coverLetterRepository.AddAsync(coverLetter);
            await this.coverLetterRepository.SaveChangesAsync();
        }

        public async Task<int> EditAsync(EditCoverLetterInputModel inputModel)
        {
            var coverLetter = await this.coverLetterRepository.GetByIdWithDeletedAsync(inputModel.Id);

            coverLetter.Title = inputModel.Title;
            coverLetter.Content = inputModel.Content;
            coverLetter.StartDate = inputModel.StartDate;
            coverLetter.EndDate = inputModel.EndDate;
            coverLetter.ImageUrl = inputModel.ImageUrl;

            await this.coverLetterRepository.SaveChangesAsync();

            return coverLetter.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var coverLetter = await this.coverLetterRepository.GetByIdWithDeletedAsync(id);

            this.coverLetterRepository.Delete(coverLetter);
            await this.coverLetterRepository.SaveChangesAsync();
        }

        public IEnumerable<TModel> GetAll<TModel>()
            => this.coverLetterRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .To<TModel>()
                .ToList();
    }
}
