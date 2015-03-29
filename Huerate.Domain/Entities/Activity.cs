/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Huerate.Domain.Entities
{
    public class Activity : IPrimaryKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public DateTime Recorded { get; set; }
        public string CustomData { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        public Contact Contact { get; set; }


        [ForeignKey("LoggedInRestaurant")]
        public int? LoggedInRestaurantId { get; set; }

        public RestaurantAccount LoggedInRestaurant { get; set; }

        [ForeignKey("ActivityType")]
        public ActivityTypeEnum ActivityTypeId { get; set; }

        public ActivityType ActivityType { get; set; }
    }
}