/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using Huerate.Domain.Entities;

namespace Huerate.Services.Contacts
{
    public interface IContactService
    {
        Contact GetCurrentContact();

        void TrackActivity(Activity activity);

        void TrackActivity(ActivityTypeEnum activityTypeEnum, params string[] customData);
    }
}
