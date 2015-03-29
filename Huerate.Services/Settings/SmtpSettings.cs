/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

namespace Huerate.Services.Settings
{
    public class SmtpSettings
    {
        public string FromAddress { get; set; }
        public string ServerAddress { get; set; }
        public bool EnableSsl { get; set; }
        public int ServerPort { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}