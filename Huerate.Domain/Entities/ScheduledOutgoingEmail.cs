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
    public class ScheduledOutgoingEmail : IPrimaryKeyEntity<int>
    {
        public int Id { get; set; }

        [ForeignKey("EmailTemplate")]
        public int? EmailTemplateId { get; set; }

        public virtual EmailTemplate EmailTemplate { get; set; }

        public DateTime? ScheduledSendDateTime { get; set; }

        [Required]
        public string ReceiverAddress { get; set; }

        public DateTime? SendDateTime { get; set; }

        public string SentFromAddress { get; set; }
    }
}