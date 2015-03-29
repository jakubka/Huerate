/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Collections.Generic;

namespace Huerate.WebViewModels.Form
{
    public class FormModel
    {
        public string RestaurantAccountDisplayName { get; set; }
        public int RestaurantAccountId { get; set; }
        public ISet<string> Languages { get; set; } 
        public List<FormStepModel> FormSteps { get; set; } 
    }
}