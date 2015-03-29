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

namespace Huerate.Domain.Entities
{
    public class AnswerSet : IPrimaryKeyEntity<Guid>
    {
        public AnswerSet()
        {
            Answers = new HashSet<Answer>();
        }
    
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }

        public int RestaurantAccountId { get; set; }
        [ForeignKey("RestaurantAccountId")]
        public virtual RestaurantAccount Restaurant { get; set; }
    
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
