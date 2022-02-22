using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MailChat
{
    class Tools:mailchatForm
    {
        public static bool SendMail(SmtpClient smtpClient, AccountInfo account, string title, string body)
        {
            MimeMessage message = new MimeMessage();
            message.Subject = title;
            message.Body = new TextPart("plain"){
                Text = body
            };

            Dictionary<String, String> userInfo = GetUserInfo(account);
            string fromAddr = userInfo["fromAddr"];
            string fromName = userInfo["fromName"];
            string toAddr = userInfo["toAddr"];
            string toName = userInfo["toName"];

            message.From.Add(new MailboxAddress(fromName, fromAddr));
            message.To.Add(new MailboxAddress(toName, toAddr));

            smtpClient.Send(message);
            message.Dispose();
            return true;
        }


        public static List<Tuple<String, String, UniqueId>> FindNewMessage(IMailFolder folder)
        {
            IList<UniqueId> uids = folder.Search(MailKit.Search.SearchQuery.NotSeen);
            string subject;
            string body;
            MimeMessage email;
            List<Tuple<String, String, UniqueId>> ret = new List<Tuple<string, string, UniqueId>>();
            foreach (UniqueId uid in uids)
            {
                email = folder.GetMessage(uid);
                subject = email.Subject;
                body = email.TextBody;
                ret.Add(Tuple.Create(subject, body, uid));
            }

            return ret;
        }


        public static ImapClient GetImap(AccountInfo info)
        {
            string imapHost = info.imapInfo.ImapHost;
            string imapUser = info.imapInfo.ImapUser;
            string imapPass = info.imapInfo.ImapPass;

            ImapClient client = new ImapClient();
            client.Connect(imapHost);
            client.Authenticate(imapUser, imapPass);
            ImapImplementation imapImp = new ImapImplementation
            {
                Name = "qiaoqiaohua",
                Version = "3.1.1"
            };
            client.Identify(imapImp);

            return client;
        }

        public static SmtpClient GetSmtp(AccountInfo info)
        {
            SmtpClient client = new SmtpClient();

            string smtpHost = info.smtpInfo.SmtpHost;
            string smtpUser = info.smtpInfo.SmtpUser;
            string smtpPass = info.smtpInfo.SmtpPass;

            client.Connect(smtpHost);
            client.Authenticate(smtpUser, smtpPass);
           

            return client;
        }

        public static Dictionary<String, String> GetUserInfo(AccountInfo info)
        {
            Dictionary<String, String> userInfo = new Dictionary<string, string>();

            userInfo.Add("fromAddr", info.account.Sender.Address);
            userInfo.Add("fromName", info.account.Sender.Name);
            userInfo.Add("toAddr", info.account.Receiver.Address);
            userInfo.Add("toName", info.account.Receiver.Name);

            return userInfo;
        }

        public static void Log2File(string log)
        {
            string logPath = "log.log";
            FileStream fs = new FileStream(logPath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(log);
            sw.Close();
            fs.Close();
        }
        
        public static AccountInfo GetConfig()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string confileFileName = "config.json";
            string fullPath = Path.Combine(baseDir, confileFileName);
            string json = File.ReadAllText(fullPath);
            AccountInfo config = JsonConvert.DeserializeObject<AccountInfo>(json);

            return config;
        }
    }
}
