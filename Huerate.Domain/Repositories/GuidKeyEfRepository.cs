/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using Huerate.Domain.Entities;

namespace Huerate.Domain.Repositories
{
    internal class GuidKeyEfRepository<TEntity> : EfRepositoryBase<TEntity, Guid> where TEntity : class, IPrimaryKeyEntity<Guid>
    {
        public GuidKeyEfRepository(HuerateContext context) : base(context)
        {
        }

        public override TEntity GetById(Guid id)
        {
            return Fetch().SingleOrDefault(entity => entity.Id == id);
        }
    }
}