/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Huerate.Domain.Entities
{
    public class Contact : IPrimaryKeyEntity<int>
    {
        public Contact()
        {
            Activities = new HashSet<Activity>();
        }

        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }

        public string UserAgent { get; set; }

        public ICollection<Activity> Activities { get; set; } 
    }
}
