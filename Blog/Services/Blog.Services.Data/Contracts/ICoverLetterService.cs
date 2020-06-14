namespace Blog.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Blog.Web.ViewModels.Administration.CoverLetter.InputModels;

    public interface ICoverLetterService
    {
        Task CreateAsync(CreateCoverLetterInputModel inputModel);

        Task<int> EditAsync(EditCoverLetterInputModel inputModel);

        Task DeleteAsync(int id);

        IEnumerable<TModel> GetAll<TModel>();
    }
}
