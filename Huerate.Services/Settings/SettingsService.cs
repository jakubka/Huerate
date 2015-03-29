/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Huerate.Configuration;
using Huerate.Domain.Entities;
using Huerate.Services.Translations;

namespace Huerate.Services.Settings
{
    internal class SettingsService : ISettingsService
    {
        private ITranslationService TranslationService { get; set; }

        public SettingsService(ITranslationService translationService)
        {
            TranslationService = translationService;
        }

        public bool SendEmails
        {
            get { return HuerateConfiguration.GetSetting<bool>("SendEmails"); }
        }

        public SmtpSettings SmtpSettings
        {
            get
            {
                return new SmtpSettings()
                           {
                               FromAddress = "jakub@huerate.cz",
                               ServerAddress = "smtp.live.com",
                               EnableSsl = true,
                               ServerPort = 587,
                               Username = "jakub@huerate.cz",
                               Password = "Hu3R4t3J4KUB",
                           };
            }
        }

        public SendGridSettings SendGridSettings
        {
            get
            {
                return new SendGridSettings()
                           {
                               FromAddress = "jakub@huerate.cz",
                               FromName = "Jakub Kadlubiec",
                               Password = "ZcZMm75sazM97qOYzQSe",
                               Username = "jakubk",
                           };
            }
        }

        public string DefaultLanguage
        {
            get { return "cs"; }
        }

        public IList<string> MonitoringEmailAddresses
        {
            get { return new[] {"bobek@outlook.com"}; }
        }

        public bool ContentFilesOnBlob
        {
            get { return HuerateConfiguration.GetSetting<bool>("ContentFilesOnBlob"); }
        }

        public string ContentFilesBlobUri
        {
            get { return HuerateConfiguration.GetSetting<string>("ContentFilesBlobUri"); }
        }
    }
}