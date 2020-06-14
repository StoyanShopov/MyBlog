namespace Blog.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Blog.Data.Common.Repositories;
    using Blog.Data.Models;
    using Blog.Data.Repositories;
    using Blog.Services.Data.Contracts;
    using Blog.Services.Data.Tests.Common;
    using Blog.Services.Mapping;
    using Blog.Web.ViewModels;
    using Blog.Web.ViewModels.Administration.Categories.ViewModels;
    using Moq;
    using Xunit;

    public class CategoryServiceTests
    {
        private readonly EfDeletableEntityRepository<Category> categoryRepository;
        private readonly ICategoriesService categoriesService;

        public CategoryServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            this.categoryRepository = new EfDeletableEntityRepository<Category>(context);
            this.categoriesService = new CategoriesService(this.categoryRepository);
        }

        [Fact]
        public async Task CreateAsyncCategoryShouldWorkAsIsExpected()
        {
            const string categoryName = "MyCategory";

            var totalObjectsBeforeAdding = this.categoryRepository.AllAsNoTracking().Count();

            await this.categoriesService.CreateAsync(categoryName);

            var totalObjectsAfterAdding = this.categoryRepository.AllAsNoTracking().Count();

            var actualObject = await this.categoryRepository.GetByIdWithDeletedAsync(1);
            Assert.NotNull(actualObject);

            Assert.Equal(categoryName, actualObject.Name);

            Assert.Equal(0, totalObjectsBeforeAdding);
            Assert.Equal(1, totalObjectsAfterAdding);
        }

        [Fact]
        public async Task GetAllCategoryShouldWorkAsIsExpected()
        {
            var categories = new[]
            {
                "Business",
                "Programming",
                "Seminars",
                "Coding",
            };

            foreach (var category in categories)
            {
                await this.categoriesService.CreateAsync(category);
            }

            var allCategories = this.categoriesService.GetAll<PostCategoryViewModel>();

            var index = 0;

            foreach (var category in allCategories)
            {
                Assert.Equal(categories[index++], category.Name);
            }
        }
    }
}
