﻿/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

namespace Huerate.Services.Contacts
{
    internal interface IContactPersistenceService
    {
        void StoreContactId(int contactId);

        int? GetCurrentContactId();

        void ClearCurrentContactId();
    }
}
