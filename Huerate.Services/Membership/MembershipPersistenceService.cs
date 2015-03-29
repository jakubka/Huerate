/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Globalization;
using System.Web;
using System.Web.Security;

namespace Huerate.Services.Membership
{
    internal class MembershipPersistenceService : IMembershipPersistenceService
    {
        private HttpContextBase HttpContextBase { get; set; }

        public MembershipPersistenceService(HttpContextBase httpContextBase)
        {
            HttpContextBase = httpContextBase;
        }

        public int? GetStoredUserId()
        {
            int id;
            if (HttpContextBase.User == null || !int.TryParse(HttpContextBase.User.Identity.Name, out id))
            {
                return null;
            }

            return id;
        }

        public void StoreUserId(int userId, bool persistentOverSessions)
        {
            FormsAuthentication.SetAuthCookie(userId.ToString(CultureInfo.InvariantCulture), persistentOverSessions);
        }

        public void Clear()
        {
            FormsAuthentication.SignOut();
        }
    }
}