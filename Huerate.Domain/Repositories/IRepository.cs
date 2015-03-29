/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Collections.Generic;
using System.Linq;
using Huerate.Domain.Entities;

namespace Huerate.Domain.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : IPrimaryKeyEntity<TKey>
    {
        IQueryable<TEntity> Fetch();

        List<TEntity> GetAll();

        TEntity GetById(TKey id);

        void Add(TEntity entity);

        void Remove(TEntity entity);

        void Remove(TKey id);

        void Attach(TEntity entity);

        void Detach(TEntity entity);
    }
}