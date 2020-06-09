using System;

namespace WindowsForms4HXPos
{
    partial class MainForm
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
            this.requestBtn = new System.Windows.Forms.Button();
            this.startWebsocketBtn = new System.Windows.Forms.Button();
            this.infoLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // requestBtn
            // 
            this.requestBtn.Location = new System.Drawing.Point(494, 12);
            this.requestBtn.Name = "requestBtn";
            this.requestBtn.Size = new System.Drawing.Size(75, 23);
            this.requestBtn.TabIndex = 0;
            this.requestBtn.Text = "请求刷卡";
            this.requestBtn.UseVisualStyleBackColor = true;
            this.requestBtn.Click += new System.EventHandler(this.requestBtn_Click);
            // 
            // startWebsocketBtn
            // 
            this.startWebsocketBtn.Location = new System.Drawing.Point(44, 37);
            this.startWebsocketBtn.Name = "startWebsocketBtn";
            this.startWebsocketBtn.Size = new System.Drawing.Size(75, 23);
            this.startWebsocketBtn.TabIndex = 1;
            this.startWebsocketBtn.Text = "启动服务";
            this.startWebsocketBtn.UseVisualStyleBackColor = true;
            this.startWebsocketBtn.Click += new System.EventHandler(this.startWebsocketBtn_Click);
            // 
            // infoLbl
            // 
            this.infoLbl.AutoSize = true;
            this.infoLbl.Location = new System.Drawing.Point(42, 101);
            this.infoLbl.Name = "infoLbl";
            this.infoLbl.Size = new System.Drawing.Size(53, 12);
            this.infoLbl.TabIndex = 2;
            this.infoLbl.Text = "服务情况";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 396);
            this.Controls.Add(this.infoLbl);
            this.Controls.Add(this.startWebsocketBtn);
            this.Controls.Add(this.requestBtn);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button requestBtn;
        private System.Windows.Forms.Button startWebsocketBtn;
        private System.Windows.Forms.Label infoLbl;
    }
}