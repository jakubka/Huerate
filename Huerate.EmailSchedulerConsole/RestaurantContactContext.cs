/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Data.Entity;

namespace Huerate.EmailSchedulerConsole
{
    public class RestaurantContactContext : DbContext
    {
        public RestaurantContactContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<RestaurantContact> RestaurantContacts { get; set; }
    }
}