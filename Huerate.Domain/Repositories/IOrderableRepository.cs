/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using Huerate.Domain.Entities;

namespace Huerate.Domain.Repositories
{
    public interface IOrderableRepository<TEntity, TKey> where TEntity : IPrimaryKeyEntity<TKey>
    {
        void MoveUp(TKey id);

        void MoveDown(TKey id);
    }
}