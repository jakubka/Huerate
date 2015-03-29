/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huerate.Domain.Entities
{
    public class EmailTemplate : IPrimaryKeyEntity<int>
    {
        public int Id { get; set; }

        public string CodeName { get; set; }

        public string Language { get; set; }

        public string SubjectTemplate { get; set; }

        public string HtmlBodyTemplate { get; set; }

        public string PlainTextBodyTemplate { get; set; }
    }
}
