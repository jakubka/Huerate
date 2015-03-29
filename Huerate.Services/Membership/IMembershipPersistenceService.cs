/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huerate.Services.Membership
{
    internal interface IMembershipPersistenceService
    {
        int? GetStoredUserId();

        void StoreUserId(int userId, bool persistentOverSessions);

        void Clear();
    }
}
