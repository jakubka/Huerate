/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;

namespace Huerate.Domain.Entities
{
    public class NewsletterSubscription : IPrimaryKeyEntity<int>
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string IPAddress { get; set; }
        public string Email { get; set; }
    }
}
