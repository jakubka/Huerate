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
    public enum ActivityTypeEnum
    {
        HomePageVisit,
        AdminPageVisit,
        FormPageVisit,
        GenericPageVisit,
        SignIn,
        SignInFail,
        SignUp,
        SignUpFail,
        Ref,
        NewsletterSubscribe,
        LostPasswordRecovery,
        LostPasswordRecoveryFail,
        QrCodeDownload,
    }

    public class ActivityType : IPrimaryKeyEntity<ActivityTypeEnum>
    {
        public ActivityType()
        {
            ActivityTypeUsages = new HashSet<Activity>();
        }

        [Key]
        public ActivityTypeEnum Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }

        public ICollection<Activity> ActivityTypeUsages { get; set; } 
    }
}
