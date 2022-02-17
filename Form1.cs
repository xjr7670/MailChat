using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace MailChat
{
    public partial class mailchatForm : Form
    {
        SmtpClient smtpClient;
        ImapClient imapClient;
        Thread refreshThread;
        CancellationTokenSource cts = new CancellationTokenSource();
        public mailchatForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            imapClient = Tools.GetImap();
            refreshThread = new Thread(new ThreadStart(CheckNewEmail));
            refreshThread.IsBackground = true;
            refreshThread.Start();
            
        }

        public void CheckNewEmail()
        {
            int gap = 2;
            CancellationToken ct = cts.Token;
            IMailFolder folder = imapClient.GetFolder("PrivacyChat");
            folder.Open(FolderAccess.ReadWrite);
            List<Tuple<String, String, UniqueId>> result;
            while (!ct.IsCancellationRequested)
            {
                result = Tools.FindNewMessage(folder);
                foreach (Tuple<String, String, UniqueId> msg in result) {
                    this.conversationBox.AppendText(DateTime.Now + "\t" + msg.Item1 + "\r\n> " + msg.Item2 + "\n");
                    folder.SetFlags(msg.Item3, MessageFlags.Seen, true);
                }
                Thread.Sleep(gap * 1000);
            }
            refreshThread.Abort();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            string body = this.mailBody.Text.Trim();
            string title = this.titleBtn.Text.Trim();
            if (title.Equals(""))
            {
                title = "无标题";

            } else
            {
                var form = new mailchatForm();
                form.Text = title;
            }
            if (body.Equals(""))
            {
                this.warnLbl.Text = "不能发送空内容！";
                return;
            }

            smtpClient = Tools.GetSmtp();
            bool result = Tools.SendMail(smtpClient, title, body);
            if (result)
            {
                this.warnLbl.Text = "发送成功！";
                this.mailBody.Clear();
                this.conversationBox.AppendText(DateTime.Now + "\t" + title + "\r\n" + body + "\n");
            } else
            {
                this.warnLbl.Text = "发送失败！";
            }
        }

        private void mailBody_TextChanged(object sender, EventArgs e)
        {
            // 正文输入框文件变化的时候，清除下方的提示
            
        }

        private void conversationBox_TextChange(object sender, EventArgs e)
        {
            this.conversationBox.ScrollToCaret();
        }

        private void mailchat_Close(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
            if (smtpClient != null)
            {
                smtpClient.Disconnect(true);
            }
            if (imapClient != null)
            {
                imapClient.Disconnect(true);
            }
        }

        private void mailBody_KeyUp(object sender, KeyEventArgs e)
        {
            this.warnLbl.Text = "";
        }

        private void mailchatForm_Load(object sender, EventArgs e)
        {

        }
    }
}
