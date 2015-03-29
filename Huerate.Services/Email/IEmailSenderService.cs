/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Huerate.Services.Email
{
    public interface IEmailSenderService
    {
        Task<string> SendEmailAsync(Email email);

        Task SendErrorEmailAsync(string message, Exception exception = null);

        Task SendEmailFromTemplateAsync(string templateCodeName, string language, IList<string> receivers, Dictionary<string, object> templateParameters = null);
    }
}
