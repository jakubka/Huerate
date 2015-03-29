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
    internal class CustomTranslationEfRepository : IntKeyEfRepository<CustomTranslation>, ICustomTranslationRepository
    {
        public CustomTranslationEfRepository(HuerateContext context) : base(context)
        {
        }

        public IDictionary<string, Dictionary<string, string>> GetAllByLanguageAndCodeName()
        {
            return Fetch().GroupBy(cr => cr.Language).ToDictionary(g => g.Key, g => g.ToDictionary(cr => cr.CodeName, cr => cr.Value));
        }

        public IEnumerable<CustomTranslation> GetAllByRestaurant(int restaurantAccountId)
        {
            return Fetch().Where(ct => ct.RestaurantAccountId == null || ct.RestaurantAccountId == restaurantAccountId).AsEnumerable();
        }

        public CustomTranslation GetByLanguageAndCodeName(string languageString, string codeName)
        {
            return Fetch().SingleOrDefault(ct => ct.Language == languageString && ct.CodeName == codeName);
        }
    }
}