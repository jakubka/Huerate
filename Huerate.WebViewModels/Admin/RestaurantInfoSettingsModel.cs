/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.ComponentModel.DataAnnotations;

namespace Huerate.WebViewModels.Admin
{
    public class RestaurantInfoSettingsModel
    {
        [Required]
        public string CodeName { get; set; }

        [Required]
        public string DisplayName { get; set; }
    }
}