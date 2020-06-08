namespace Blog.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Blog.Data.Common.Repositories;
    using Blog.Data.Models;
    using Blog.Services.Data.Contracts;
    using Blog.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(string name)
        {
            var categoryExists = await this.categoryRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);

            if (categoryExists)
            {
                return;
            }

            var category = new Category
            {
                Name = name,
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public IEnumerable<TModel> GetAll<TModel>()
            => this.categoryRepository
                .AllAsNoTracking()
                .To<TModel>()
                .ToList();
    }
}
