/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Collections.Generic;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;

namespace Huerate.Services
{
    public interface IDataService<TEntity, TKey> : IDataService<IRepository<TEntity, TKey>, TEntity, TKey> 
        where TEntity : IPrimaryKeyEntity<TKey>
    {
    }

    public interface IDataService<TRepository, TEntity, TKey>
        where TEntity : IPrimaryKeyEntity<TKey>
        where TRepository : IRepository<TEntity, TKey>
    {
        TRepository Repository { get; }

        TEntity GetById(TKey id);

        void Create(TEntity entity);

        void Delete(TEntity entity);

        void Delete(TKey id);

        IList<TEntity> GetAll();

        int Save();
    }
}