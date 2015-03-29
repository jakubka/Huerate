/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huerate.EmailSchedulerConsole
{
    public enum ContactSourceEnum
    {
        Lunchtime,
        RestauraceCZ
    }

    public enum ResponseStatusEnum
    {
        NotResponded,
        EmailDoesNotExist,
        Positive,
        NeedMoreInfo,
        Negative,
    }

    public class RestaurantContact
    {
        [Key]
        public int Id { get; set; }

        [Column("LunchtimeId")]
        public string ExternalId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string City { get; set; }


        public double? Lat { get; set; }
        public double? Lng { get; set; }

        [Column("LunchtimeWeb")]
        public string ExternalWeb { get; set; }


        public string EmailAddress { get; set; }
        public string AllEmails { get; set; }


        public DateTime? EmailSendTime { get; set; }

        public ResponseStatusEnum ResponseStatus { get; set; }
        public string ResponseText { get; set; }
        public DateTime? ResponseDateTime { get; set; }

        public ContactSourceEnum ContactSource { get; set; }
    }
}