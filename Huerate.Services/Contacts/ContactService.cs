/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Web;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Logging;
using Huerate.Services.Membership;

namespace Huerate.Services.Contacts
{
    internal class ContactService : ServiceBase, IContactService
    {
        private HttpContextBase HttpContext { get; set; }
        private IContactPersistenceService ContactPersistenceService { get; set; }
        private IMembershipService MembershipService { get; set; }

        public ContactService(HttpContextBase httpContext, IContactPersistenceService contactPersistenceService, IMembershipService membershipService, IUnitOfWork unitOfWork, ILogService logService) : base(unitOfWork, logService)
        {
            HttpContext = httpContext;
            ContactPersistenceService = contactPersistenceService;
            MembershipService = membershipService;
        }

        public Contact GetCurrentContact()
        {
            var contactId = ContactPersistenceService.GetCurrentContactId();

            Contact contact;
            if (contactId == null || (contact = UnitOfWork.ContactRepository.GetById(contactId.Value)) == null)
            {
                contact = new Contact()
                              {
                                  Created = DateTime.UtcNow,
                                  UserAgent = HttpContext.Request.UserAgent,
                              };

                UnitOfWork.ContactRepository.Add(contact);

                UnitOfWork.Save();

                ContactPersistenceService.StoreContactId(contact.Id);

                return contact;
            }

            return contact;
        }

        public void TrackActivity(Activity activity)
        {
            if (HttpContext.Request.Browser.Crawler)
            {
                return;
            }

            var contact = GetCurrentContact();

            activity.ContactId = contact.Id;
            activity.Recorded = DateTime.UtcNow;

            UnitOfWork.ActivityRepository.Add(activity);

            UnitOfWork.Save();
        }

        public void TrackActivity(ActivityTypeEnum activityTypeEnum, params string[] customData)
        {
            if (HttpContext.Request.Browser.Crawler)
            {
                return;
            }

            var activity = new Activity()
                               {
                                   ActivityTypeId = activityTypeEnum,
                                   CustomData = string.Join("|", customData),
                               };

            var restaurant = MembershipService.CurrentRestaurantAccount;

            if (restaurant != null)
            {
                activity.LoggedInRestaurantId = restaurant.Id;
            }

            TrackActivity(activity);
        }
    }
}