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
using System.Threading.Tasks;

namespace Huerate.Domain.Entities
{
    public class CustomTranslation : IPrimaryKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CodeName { get; set; }

        [Required]
        public string Language { get; set; }

        public string Value { get; set; }

        [ForeignKey("RestaurantAccountId")]
        public RestaurantAccount RestaurantAccount { get; set; }

        public int? RestaurantAccountId { get; set; }
    }
}
