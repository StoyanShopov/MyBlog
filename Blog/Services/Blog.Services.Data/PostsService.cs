﻿namespace Blog.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Blog.Data.Common.Repositories;
    using Blog.Data.Models;
    using CloudinaryDotNet;
    using Common;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Web.ViewModels.Administration.Posts.InputModels;

    public class PostsService
    {
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IDeletableEntityRepository<Tag> tagRepository;
        private readonly Cloudinary cloudinary;

        public PostsService(
            IDeletableEntityRepository<Post> postRepository,
            IDeletableEntityRepository<Category> categoryRepository,
            IDeletableEntityRepository<Tag> tagRepository,
            Cloudinary cloudinary)
        {
            this.postRepository = postRepository;
            this.categoryRepository = categoryRepository;
            this.tagRepository = tagRepository;
            this.cloudinary = cloudinary;
        }

        public int TotalPosts
            => this.postRepository.AllAsNoTracking().Count();

        public async Task<int> CreateAsync(CreatePostInputModel inputModel)
        {
            var imageUrl = await ApplicationCloudinary
                .UploadImage(this.cloudinary, inputModel.Image, inputModel.Title);

            var post = new Post
            {
                Title = inputModel.Title,
                Description = inputModel.Description,
                ShortDescription = inputModel.ShortDescription,
                ImageUrl = imageUrl,
            };

            post.PostCategories.Add(new PostCategory
            {
                CategoryId = inputModel.CategoryId,
            });

            var tags = inputModel.Tags
                .Split(separator: new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .ToArray();

            foreach (var tag in tags)
            {
                post.TagPosts.Add(new TagPost
                {
                    Tag = new Tag
                    {
                        Name = tag,
                    },
                });
            }

            await this.postRepository.AddAsync(post);
            await this.postRepository.SaveChangesAsync();

            return post.Id;
        }

        public int EditAsync()
        {
            return 0;
        }

        public IEnumerable<TModel> GetAll<TModel>()
            => this.postRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .To<TModel>()
            .ToList();

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

        public IEnumerable<TModel> GetLastCreatedPosts<TModel>(int defaultCount = 5)
            => this.postRepository
                .AllAsNoTracking()
                .OrderBy(x => x.CreatedOn)
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
        {
            var result = this.postRepository
             .AllAsNoTracking()
             .Include(x => x.ApplicationUser)
             .Include(x => x.TagPosts)
             .ThenInclude(t => t.Tag)
             .OrderByDescending(x => x.CreatedOn)
             .Skip(skip * (take - 1))
             .Take(skip)
             .To<TModel>()
             .ToList();

            return result;
        }
    }
}
