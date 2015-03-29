/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Linq;
using Huerate.Domain.Entities;

namespace Huerate.Domain.Repositories
{
    internal class EmailTemplateRepository : IntKeyEfRepository<EmailTemplate>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(HuerateContext context) : base(context)
        {
        }

        public EmailTemplate GetEmailTemplate(string templateCodeName, string language)
        {
            var template = Fetch().SingleOrDefault(t => t.CodeName == templateCodeName && t.Language == language);

            if (template != null)
            {
                return template;
            }

            return Fetch().FirstOrDefault(t => t.CodeName == templateCodeName);
        }
    }
}