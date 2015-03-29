/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Collections.Generic;

namespace Huerate.WebViewModels.Admin
{
    public class EditFormModel
    {
        public ISet<string> Languages { get; set; }

        public IEnumerable<string> FormStepTypes { get; set; } 

        public List<EditFormStepModel> FormSteps { get; set; }
    }
}