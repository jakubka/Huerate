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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Services;
using Huerate.Services.Email;

namespace Huerate.EmailTemplateEditorWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private IUnitOfWork GetUnitOfWork()
        {
            return new EfUnitOfWork(null, txtConnectionString.Text);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var unitOfWork = GetUnitOfWork();
            var ete = GetEmailTemplate(unitOfWork.EmailTemplateRepository);

            if (ete != null)
            {
                txtSubject.Text = ete.SubjectTemplate;
                txtHtmlText.Text = ete.HtmlBodyTemplate;
                txtPlainText.Text = ete.PlainTextBodyTemplate;
            }
            else
            {
                MessageBox.Show("NULL");
            }
        }

        private EmailTemplate GetEmailTemplate(IEmailTemplateRepository emailTemplateRepository)
        {
            var query = from et in emailTemplateRepository.Fetch()
                        where et.CodeName == txtCodeName.Text
                        where txtLanguage.Text == "" && et.Language == null
                        || txtLanguage.Text != "" && et.Language == txtLanguage.Text
                                        select et;
            return query.SingleOrDefault();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var unitOfWork = GetUnitOfWork();

            var emailTemplate = GetEmailTemplate(unitOfWork.EmailTemplateRepository);

            if (emailTemplate == null)
            {
                emailTemplate = new EmailTemplate()
                                    {
                                        CodeName = txtCodeName.Text,
                                        Language = string.IsNullOrWhiteSpace(txtLanguage.Text) ? null : txtLanguage.Text,
                                    };
                unitOfWork.EmailTemplateRepository.Add(emailTemplate);
            }

            emailTemplate.SubjectTemplate = txtSubject.Text;
            emailTemplate.HtmlBodyTemplate = txtHtmlText.Text;
            emailTemplate.PlainTextBodyTemplate = txtPlainText.Text;

            unitOfWork.Save();
        }
    }
}