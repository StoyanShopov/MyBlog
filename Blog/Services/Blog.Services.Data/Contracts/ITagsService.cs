namespace Blog.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface ITagsService
    {
        IEnumerable<T> GetAllTags<T>();

        IEnumerable<T> GetAllTagsById<T>(int id);
    }
}
