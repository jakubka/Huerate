/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using Huerate.Domain.Entities;

namespace Huerate.Domain.Repositories
{
    internal abstract class EfRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IPrimaryKeyEntity<TKey>
    {
        public EfRepositoryBase(HuerateContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            Context = context;
            DbSet = context.Set<TEntity>();
        }

        protected HuerateContext Context { get; private set; }

        protected DbSet<TEntity> DbSet { get; private set; }

        public virtual IQueryable<TEntity> Fetch()
        {
            return DbSet;
        }

        public List<TEntity> GetAll()
        {
            return Fetch().ToList();
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void Remove(TKey id)
        {
            TEntity entity = GetById(id);

            if (entity == null)
            {
                throw new DomainException("Remove: Entity not found");
            }

            Remove(entity);
        }

        public virtual void Attach(TEntity entity)
        {
            DbSet.Attach(entity);
        }

        public void Detach(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Detached;
        }

        public abstract TEntity GetById(TKey id);
    }
}