/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.ComponentModel.DataAnnotations;

namespace Huerate.WebViewModels.Admin
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword1 { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword1")]
        public string NewPassword2 { get; set; }
    }
}
