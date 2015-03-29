/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Globalization;
using System.Web;

namespace Huerate.Services.Contacts
{
    internal class CookieContactPersistenceService : IContactPersistenceService
    {
        private HttpContextBase HttpContext { get; set; }

        public CookieContactPersistenceService(HttpContextBase httpContext)
        {
            HttpContext = httpContext;
        }

        public void StoreContactId(int contactId)
        {
            HttpContext.Response.Cookies.Add(new HttpCookie("ContactId", contactId.ToString(CultureInfo.InvariantCulture))
                                                 {
                                                     Expires = DateTime.MaxValue,
                                                 });
        }

        public int? GetCurrentContactId()
        {
            var cookie = HttpContext.Request.Cookies.Get("ContactId");

            if (cookie == null)
            {
                return null;
            }

            int contactId;

            if (!int.TryParse(cookie.Value, out contactId))
            {
                return null;
            }

            return contactId;
        }

        public void ClearCurrentContactId()
        {
            HttpContext.Response.Cookies.Remove("ContactId");
        }
    }
}