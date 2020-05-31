namespace Blog.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task AddAsync(string name);

        IEnumerable<TModel> GetAll<TModel>();
    }
}
