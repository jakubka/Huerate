/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Huerate.Domain.Entities;

namespace Huerate.Domain.Repositories
{
    internal class OrderableRepository<TEntity, TKey> : IOrderableRepository<TEntity, TKey> where TEntity : class, IPrimaryKeyEntity<TKey>, IOrderableEntity
    {
        private readonly IRepository<TEntity, TKey> _repository;
        private readonly Func<TEntity, Expression<Func<TEntity, bool>>> _filterCreator;

        public OrderableRepository(IRepository<TEntity, TKey> repository, Func<TEntity, Expression<Func<TEntity, bool>>> filterCreator)
        {
            _repository = repository;
            _filterCreator = filterCreator;
        }

        public void MoveDown(TKey id)
        {
            Swap(id, (set, entity) => set.Where(_filterCreator(entity)).Where(e => e.Order > entity.Order).OrderBy(e => e.Order).FirstOrDefault());
        }

        public void MoveUp(TKey id)
        {
            Swap(id, (set, entity) => set.Where(_filterCreator(entity)).Where(e => e.Order < entity.Order).OrderByDescending(e => e.Order).FirstOrDefault());
        }

        private void Swap(TKey id, Func<IQueryable<TEntity>, TEntity, TEntity> secondEntitySelector)
        {
            TEntity entity = _repository.GetById(id);

            if (entity == null)
            {
                return;
            }

            TEntity entity2 = secondEntitySelector(_repository.Fetch(), entity);

            if (entity2 == null)
            {
                return;
            }

            int temp = entity.Order;
            entity.Order = entity2.Order;
            entity2.Order = temp;
        }

    }
}