namespace Blog.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AngleSharp.Css.Values;
    using Blog.Data.Common.Repositories;
    using Blog.Data.Models;
    using Blog.Services.Data.Common;
    using Blog.Services.Data.Contracts;
    using Blog.Services.Mapping;
    using Blog.Web.ViewModels.Administration.Posts.InputModels;
    using CloudinaryDotNet;
    using Microsoft.EntityFrameworkCore;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly IDeletableEntityRepository<Tag> tagRepository;
        private readonly Cloudinary cloudinary;

        public PostsService(
            IDeletableEntityRepository<Post> postRepository,
            IDeletableEntityRepository<Category> categoryRepository,
            IDeletableEntityRepository<Tag> tagRepository,
            Cloudinary cloudinary)
        {
            this.postRepository = postRepository;
            this.tagRepository = tagRepository;
            this.cloudinary = cloudinary;
        }

        public int TotalPosts
            => this.postRepository.AllAsNoTracking().Count();

        public async Task<int> CreateAsync(string userId, CreatePostInputModel inputModel)
        {
            var imageUrl = await ApplicationCloudinary
                .UploadViaFromFile(this.cloudinary, inputModel.Image, inputModel.Title);

            var newUrls = await ApplicationCloudinary
                .GetImageUrlsAsync(this.cloudinary, inputModel.Description);

            var updatedContent = await AngleSharpExtension
                .UpdateImageSourceAsync(newUrls.ToList(), inputModel.Description);

            var post = new Post
            {
                Title = inputModel.Title,
                Description = updatedContent,
                ShortDescription = inputModel.ShortDescription,
                ImageUrl = imageUrl,
                ApplicationUserId = userId,
            };

            post.PostCategories.Add(new PostCategory
            {
                CategoryId = inputModel.CategoryId,
            });

            var tags = inputModel.Tags
                .Split(separator: new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .ToArray();

            foreach (var tagName in tags)
            {
                var currentTag = this.tagRepository
                    .AllAsNoTracking()
                    .FirstOrDefault(x => x.Name == tagName);

                if (currentTag == null)
                {
                    currentTag = new Tag
                    {
                        Name = tagName,
                    };
                }

                if (post.TagPosts.All(x => x.Tag.Name != tagName))
                {
                    post.TagPosts.Add(new TagPost
                    {
                        TagId = currentTag.Id,
                    });
                }
            }

            await this.postRepository.AddAsync(post);
            await this.postRepository.SaveChangesAsync();

            return post.Id;
        }

        public TModel GetById<TModel>(int id)
            => this.postRepository
                .AllAsNoTracking()
                .Include(x => x.TagPosts)
                .ThenInclude(x => x.Tag)
                .Include(x => x.PostCategories)
                .ThenInclude(x => x.Category)
                .Where(x => x.Id == id)
                .To<TModel>()
                .FirstOrDefault();

        public IEnumerable<TModel> GetLastCreatedPosts<TModel>(int defaultCount = 3)
            => this.postRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .To<TModel>()
                .Take(defaultCount)
                .ToList();

        public async Task RemoveAsync(int id)
        {
            var post = await this.postRepository.GetByIdWithDeletedAsync(id);

            this.postRepository.Delete(post);

            await this.postRepository.SaveChangesAsync();
        }

        public IEnumerable<TModel> GetByPage<TModel>(int take, int skip)
            => this.postRepository
             .AllAsNoTracking()
             .Include(x => x.ApplicationUser)
             .Include(x => x.TagPosts)
             .ThenInclude(t => t.Tag)
             .OrderByDescending(x => x.CreatedOn)
             .Skip(skip * (take - 1))
             .Take(skip)
             .To<TModel>()
             .ToList();

        public IEnumerable<TModel> GetByTagPage<TModel>(int tagId, int take, int skip)
            => this.postRepository
                .AllAsNoTracking()
                .Include(x => x.ApplicationUser)
                .Include(x => x.TagPosts)
                .ThenInclude(t => t.Tag)
                .Where(t => t.TagPosts.Any(i => i.TagId == tagId))
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip * (take - 1))
                .Take(skip)
                .To<TModel>()
                .ToList();

        public async Task<int> EditAsync(EditPostInputModel inputModel)
        {
            var post = this.postRepository
                .All()
                .Include(t => t.TagPosts)
                .ThenInclude(x => x.Tag)
                .FirstOrDefault(x => x.Id == inputModel.Id);

            post.Title = inputModel.Title;
            post.Description = inputModel.Description;
            post.ShortDescription = inputModel.ShortDescription;

            if (post.PostCategories.Any(x => x.CategoryId == inputModel.CategoryId))
            {
                post.PostCategories.Add(new PostCategory
                {
                    CategoryId = inputModel.CategoryId,
                });
            }

            if (inputModel.Image != null)
            {
                var imageUrl = await ApplicationCloudinary
                    .UploadViaFromFile(this.cloudinary, inputModel.Image, inputModel.Title);

                post.ImageUrl = imageUrl;
            }

            var tags = inputModel.Tags
                .Split(separator: new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .ToArray();

            foreach (var tagName in tags)
            {
                var currentTag = this.GetOrCreateTag(tagName);

                if (post.TagPosts.All(x => x.Tag.Name != tagName))
                {
                    post.TagPosts.Add(new TagPost
                    {
                        TagId = currentTag.Id,
                    });
                }
            }

            await this.postRepository.SaveChangesAsync();

            return post.Id;
        }

        private Tag GetOrCreateTag(string tagName)
        {
            var tag = this.tagRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == tagName)
                      ?? new Tag
                      {
                          Name = tagName,
                      };

            return tag;
        }
    }
}
