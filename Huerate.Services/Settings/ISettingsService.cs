/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Collections.Generic;
using System.Globalization;
using Huerate.Domain.Entities;

namespace Huerate.Services.Settings
{
    public interface ISettingsService
    {
        bool SendEmails { get; }

        SmtpSettings SmtpSettings { get; }

        SendGridSettings SendGridSettings { get; }

        string DefaultLanguage { get; }

        IList<string> MonitoringEmailAddresses { get; }

        bool ContentFilesOnBlob { get; }

        string ContentFilesBlobUri { get; }
    }
}