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

namespace Huerate.Domain.Entities
{
    public class RestaurantAccount : IPrimaryKeyEntity<int>
    {
        public RestaurantAccount()
        {
            FormSteps = new HashSet<FormStep>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string CodeName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string PasswordSalt { get; set; }

        public Guid AccountGuid { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsLockedOut { get; set; }
        public bool EmailNotificationsEnabled { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public virtual ICollection<FormStep> FormSteps { get; set; }

        public string DefaultLanguage { get; set; }

        public string Languages { get; set; }

        public ISet<string> LanguagesSet
        {
            get { return new HashSet<string>(Languages.Split(';')); }
            set { Languages = string.Join(";", value); }
        }
    }
}