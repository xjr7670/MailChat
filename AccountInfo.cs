using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailChat
{
    class AccountInfo
    {
        public Account account { get; set; }
        public SmtpInfo smtpInfo { get; set; }
        public ImapInfo imapInfo { get; set; }
    }
    public class Account
    {
        public EmailAccount Sender { get; set; }
        public EmailAccount Receiver { get; set; }
    }
    public class EmailAccount
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
    public class SmtpInfo
    {
        public string SmtpHost { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
    }
    public class ImapInfo
    {
        public string ImapHost { get; set; }
        public string ImapUser { get; set; }
        public string ImapPass { get; set; }
    }
}
