﻿namespace MvcTemplate.Data.Common
{
    using System.Linq;

    using MvcTemplate.Data.Common.Models;

    public interface IDbRepository<T>
        where T : class, IAuditInfo, IDeletableEntity
    {
        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        T GetById(object id);

        void Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);

        void Save();

        void Dispose();
    }
}
