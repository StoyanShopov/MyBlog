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
    using CloudinaryDotNet;
    using Common;

    public class CoverLetterService : ICoverLetterService
    {
        private readonly IDeletableEntityRepository<CoverLetter> coverLetterRepository;
        private readonly Cloudinary cloudinary;

        public CoverLetterService(IDeletableEntityRepository<CoverLetter> coverLetterRepository, Cloudinary cloudinary)
        {
            this.coverLetterRepository = coverLetterRepository;
            this.cloudinary = cloudinary;
        }

        public async Task CreateAsync(CreateCoverLetterInputModel inputModel)
        { 
            var newUrls = await ApplicationCloudinary
                .GetImageUrlsAsync(this.cloudinary, inputModel.Content);

            var updatedContent = await AngleSharpExtension
                .UpdateImageSourceAsync(newUrls.ToList(), inputModel.Content);

            var coverLetter = new CoverLetter
            {
                Content = updatedContent,
            };

            await this.coverLetterRepository.AddAsync(coverLetter);
            await this.coverLetterRepository.SaveChangesAsync();
        }

        public async Task EditAsync(EditCoverLetterInputModel inputModel)
        {
            var coverLetter = this.coverLetterRepository
                .All()
                .FirstOrDefault();

            var newUrls = await ApplicationCloudinary
                .GetImageUrlsAsync(this.cloudinary, inputModel.Content);

            var updatedContent = await AngleSharpExtension
                .UpdateImageSourceAsync(newUrls.ToList(), inputModel.Content);

            coverLetter.Content = updatedContent;

            await this.coverLetterRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var coverLetter = await this.coverLetterRepository.GetByIdWithDeletedAsync(id);

            this.coverLetterRepository.Delete(coverLetter);

            await this.coverLetterRepository.SaveChangesAsync();
        }

        public TModel Get<TModel>()
            => this.coverLetterRepository
                .All()
                .To<TModel>()
                .FirstOrDefault();
    }
}
