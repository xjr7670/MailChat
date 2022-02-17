using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;

namespace MailChat
{
    class Tools:mailchatForm
    {
        public static bool SendMail(SmtpClient smtpClient, string title, string body)
        {
            MimeMessage message = new MimeMessage();
            message.Subject = title;
            message.Body = new TextPart("plain"){
                Text = body
            };

            Dictionary<String, String> userInfo = GetUserInfo();
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


        public static ImapClient GetImap()
        {
            Dictionary<String, String> imapInfo = GetImapInfo();
            string imapHost = imapInfo["imapHost"];
            string imapUser = imapInfo["imapUser"];
            string imapPass = imapInfo["imapPass"];

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

        public static Dictionary<String, String> GetImapInfo()
        {
            Dictionary<String, String> imapInfo = new Dictionary<string, string>();
            imapInfo.Add("imapHost", "imap.126.com");
            imapInfo.Add("imapUser", "xjr30226@126.com");
            imapInfo.Add("imapPass", "");

            return imapInfo;
        }

        public static SmtpClient GetSmtp()
        {
            SmtpClient client = new SmtpClient();
            Dictionary<String, String> userInfo = GetUserInfo();
            string smtpHost = userInfo["smtpHost"];
            string smtpUser = userInfo["smtpUser"];
            string smtpPass = userInfo["smtpPass"];

            client.Connect(smtpHost);
            client.Authenticate(smtpUser, smtpPass);
           

            return client;
        }

        public static Dictionary<String, String> GetUserInfo()
        {
            Dictionary<String, String> userInfo = new Dictionary<string, string>();

            userInfo.Add("fromAddr", "xjr30226@126.com");
            userInfo.Add("fromName", "CavinX");
            userInfo.Add("toAddr", "284182470@qq.com");
            userInfo.Add("toName", "XQQ");
            userInfo.Add("smtpHost", "smtp.126.com");
            userInfo.Add("smtpUser", "xjr30226@126.com");
            userInfo.Add("smtpPass", "");
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
    }
}
