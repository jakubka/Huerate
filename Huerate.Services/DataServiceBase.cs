/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Logging;

namespace Huerate.Services
{
    internal abstract class DataServiceBase<TEntity, TKey> : DataServiceBase<IRepository<TEntity, TKey>, TEntity, TKey> where TEntity : IPrimaryKeyEntity<TKey>
    {
        protected DataServiceBase(IUnitOfWork unitOfWork, ILogService logService) : base(unitOfWork, logService)
        {
        }
    }

    internal abstract class DataServiceBase<TRepository, TEntity, TKey> : ServiceBase, IDataService<TRepository, TEntity, TKey>
        where TEntity : IPrimaryKeyEntity<TKey>
        where TRepository : IRepository<TEntity, TKey>
    {
        private readonly Lazy<TRepository> _repositoryLazy;

        protected DataServiceBase(IUnitOfWork unitOfWork, ILogService logService)
            : base(unitOfWork, logService)
        {
            _repositoryLazy = new Lazy<TRepository>(() => UnitOfWork.GetRepositoryForEntity<TRepository, TEntity, TKey>(), true);
        }

        public TRepository Repository
        {
            get { return _repositoryLazy.Value; }
        }

        public TEntity GetById(TKey id)
        {
            return Repository.GetById(id);
        }

        public void Create(TEntity entity)
        {
            Repository.Add(entity);

            UnitOfWork.Save();
        }

        public virtual void Delete(TEntity entity)
        {
            Repository.Remove(entity);

            UnitOfWork.Save();
        }

        public void Delete(TKey id)
        {
            Delete(Repository.GetById(id));
        }

        public IList<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        public int Save()
        {
            return UnitOfWork.Save();
        }
    }
}