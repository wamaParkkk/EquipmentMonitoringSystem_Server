
namespace EquipmentMonitoringSystem_Server
{
    partial class ServerForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
            this.richTextBoxRecvMsg = new System.Windows.Forms.RichTextBox();
            this.richTextBoxServerStatus = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnSend = new System.Windows.Forms.Button();
            this.textBoxSendMsg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // richTextBoxRecvMsg
            // 
            this.richTextBoxRecvMsg.BackColor = System.Drawing.Color.White;
            this.richTextBoxRecvMsg.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxRecvMsg.Location = new System.Drawing.Point(15, 309);
            this.richTextBoxRecvMsg.Name = "richTextBoxRecvMsg";
            this.richTextBoxRecvMsg.ReadOnly = true;
            this.richTextBoxRecvMsg.Size = new System.Drawing.Size(800, 582);
            this.richTextBoxRecvMsg.TabIndex = 6;
            this.richTextBoxRecvMsg.Text = "";
            // 
            // richTextBoxServerStatus
            // 
            this.richTextBoxServerStatus.BackColor = System.Drawing.Color.White;
            this.richTextBoxServerStatus.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxServerStatus.Location = new System.Drawing.Point(15, 29);
            this.richTextBoxServerStatus.Name = "richTextBoxServerStatus";
            this.richTextBoxServerStatus.ReadOnly = true;
            this.richTextBoxServerStatus.Size = new System.Drawing.Size(800, 240);
            this.richTextBoxServerStatus.TabIndex = 7;
            this.richTextBoxServerStatus.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Server status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(12, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Message received from the client";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "ClientConnect.png");
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(740, 897);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 25);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // textBoxSendMsg
            // 
            this.textBoxSendMsg.BackColor = System.Drawing.Color.White;
            this.textBoxSendMsg.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSendMsg.ForeColor = System.Drawing.Color.Black;
            this.textBoxSendMsg.Location = new System.Drawing.Point(15, 898);
            this.textBoxSendMsg.Name = "textBoxSendMsg";
            this.textBoxSendMsg.Size = new System.Drawing.Size(719, 25);
            this.textBoxSendMsg.TabIndex = 12;
            this.textBoxSendMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSendMsg_KeyDown);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 935);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.textBoxSendMsg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBoxServerStatus);
            this.Controls.Add(this.richTextBoxRecvMsg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerForm";
            this.Text = "Equipment Monitoring System Server";
            this.Activated += new System.EventHandler(this.ServerForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxRecvMsg;
        private System.Windows.Forms.RichTextBox richTextBoxServerStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox textBoxSendMsg;
    }
}

