namespace Blog.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Blog.Web.ViewModels.Administration.Posts.InputModels;

    public interface IPostsService
    {
        public int TotalPosts { get; }

        Task<int> CreateAsync(CreatePostInputModel inputModel);

        //public Task<int> EditAsync();

        Task RemoveAsync(int id);

        IEnumerable<TModel> GetAll<TModel>();

        TModel GetById<TModel>(int id);

        IEnumerable<TModel> GetLastCreatedPosts<TModel>(int defaultCount = 3);

        IEnumerable<TModel> GetByPage<TModel>(int take, int skip);
    }
}
