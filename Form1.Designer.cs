namespace MailChat
{
    partial class mailchatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mailchatForm));
            this.mailBody = new System.Windows.Forms.RichTextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.conversationBox = new System.Windows.Forms.RichTextBox();
            this.warnLbl = new System.Windows.Forms.Label();
            this.titleBtn = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mailBody
            // 
            this.mailBody.Location = new System.Drawing.Point(48, 389);
            this.mailBody.Name = "mailBody";
            this.mailBody.Size = new System.Drawing.Size(416, 90);
            this.mailBody.TabIndex = 1;
            this.mailBody.Text = "";
            this.mailBody.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mailBody_KeyUp);
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(48, 485);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(75, 23);
            this.sendBtn.TabIndex = 2;
            this.sendBtn.Text = "发送";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // conversationBox
            // 
            this.conversationBox.Location = new System.Drawing.Point(48, 13);
            this.conversationBox.Name = "conversationBox";
            this.conversationBox.Size = new System.Drawing.Size(416, 309);
            this.conversationBox.TabIndex = 3;
            this.conversationBox.Text = "";
            this.conversationBox.TextChanged += new System.EventHandler(this.conversationBox_TextChange);
            // 
            // warnLbl
            // 
            this.warnLbl.AutoSize = true;
            this.warnLbl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.warnLbl.ForeColor = System.Drawing.Color.LightCoral;
            this.warnLbl.Location = new System.Drawing.Point(48, 515);
            this.warnLbl.Name = "warnLbl";
            this.warnLbl.Size = new System.Drawing.Size(0, 12);
            this.warnLbl.TabIndex = 4;
            // 
            // titleBtn
            // 
            this.titleBtn.Location = new System.Drawing.Point(48, 362);
            this.titleBtn.Name = "titleBtn";
            this.titleBtn.Size = new System.Drawing.Size(416, 21);
            this.titleBtn.TabIndex = 0;
            // 
            // mailchatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 554);
            this.Controls.Add(this.titleBtn);
            this.Controls.Add(this.warnLbl);
            this.Controls.Add(this.conversationBox);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.mailBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mailchatForm";
            this.Text = "老贤与媛媛的悄悄话";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mailchat_Close);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox mailBody;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.RichTextBox conversationBox;
        private System.Windows.Forms.Label warnLbl;
        private System.Windows.Forms.TextBox titleBtn;
    }
}