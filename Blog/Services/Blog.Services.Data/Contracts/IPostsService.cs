namespace Blog.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Blog.Web.ViewModels.Administration.Posts.InputModels;

    public interface IPostsService
    {
        public int TotalPosts { get; }

        Task<int> CreateAsync(string userId, CreatePostInputModel inputModel);

        Task RemoveAsync(int id);

        TModel GetById<TModel>(int id);

        IEnumerable<TModel> GetLastCreatedPosts<TModel>(int defaultCount = 3);

        IEnumerable<TModel> GetByPage<TModel>(int take, int skip);

        IEnumerable<TModel> GetByTagPage<TModel>(int tag, int take, int skip);

        Task<int> EditAsync(EditPostInputModel inputModel);
    }
}
