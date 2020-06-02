namespace Blog.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Blog.Data.Common.Repositories;
    using Blog.Data.Models;
    using Blog.Services.Data.Contracts;
    using Blog.Services.Mapping;

    public class TagsService : ITagsService
    {
        private readonly IDeletableEntityRepository<Tag> tagRepository;

        public TagsService(IDeletableEntityRepository<Tag> tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public IEnumerable<T> GetAllTags<T>()
            => this.tagRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();
    }
}
