/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Linq;
using Huerate.Domain.Entities;

namespace Huerate.Domain.Repositories
{
    internal class RestaurantAccountEfRepository : IntKeyEfRepository<RestaurantAccount>, IRestaurantAccountRepository
    {
        public RestaurantAccountEfRepository(HuerateContext context)
            : base(context)
        {
        }

        public RestaurantAccount GetRestaurantAccountByCodeName(string codeName)
        {
            return Fetch().SingleOrDefault(u => u.CodeName == codeName);
        }

        public RestaurantAccount GetUnlockedRestaurantAccountByCodeName(string codeName)
        {
            return Fetch().SingleOrDefault(r => r.CodeName == codeName && !r.IsLockedOut);
        }

        public RestaurantAccount GetRestaurantAccountByEmail(string email)
        {
            return Fetch().SingleOrDefault(u => u.Email == email);
        }

        public RestaurantAccount GetRestaurantAccountByCodeNameOrEmail(string codeNameOrEmail)
        {
            return Fetch().SingleOrDefault(u => u.Email == codeNameOrEmail || u.CodeName == codeNameOrEmail);
        }
    }
}