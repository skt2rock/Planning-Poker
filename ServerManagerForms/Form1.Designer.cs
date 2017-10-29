namespace RoomManagerForms
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.RoomName = new System.Windows.Forms.MaskedTextBox();
            this.PortNumber = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DeleteRoom = new System.Windows.Forms.Button();
            this.RefreshRoomList = new System.Windows.Forms.Button();
            this.RoomContainer = new System.Windows.Forms.DataGridView();
            this.MessageSuccess = new System.Windows.Forms.Label();
            this.MessageFail = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnHostWebClient = new System.Windows.Forms.Button();
            this.MessageWebHost = new System.Windows.Forms.Label();
            this.ClientUrl = new System.Windows.Forms.LinkLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.UrlToHostAt = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoomContainer)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Room Name";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(116, 136);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // RoomName
            // 
            this.RoomName.Location = new System.Drawing.Point(118, 28);
            this.RoomName.Name = "RoomName";
            this.RoomName.Size = new System.Drawing.Size(175, 20);
            this.RoomName.TabIndex = 4;
            // 
            // PortNumber
            // 
            this.PortNumber.Location = new System.Drawing.Point(118, 99);
            this.PortNumber.Name = "PortNumber";
            this.PortNumber.ReadOnly = true;
            this.PortNumber.Size = new System.Drawing.Size(70, 20);
            this.PortNumber.TabIndex = 20;
            this.PortNumber.Text = "8000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port No";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(118, 63);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(175, 20);
            this.Password.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Admin Password";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DeleteRoom);
            this.groupBox1.Controls.Add(this.RefreshRoomList);
            this.groupBox1.Controls.Add(this.RoomContainer);
            this.groupBox1.Location = new System.Drawing.Point(12, 293);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 274);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rooms Available";
            // 
            // DeleteRoom
            // 
            this.DeleteRoom.Location = new System.Drawing.Point(263, 11);
            this.DeleteRoom.Name = "DeleteRoom";
            this.DeleteRoom.Size = new System.Drawing.Size(113, 23);
            this.DeleteRoom.TabIndex = 11;
            this.DeleteRoom.Text = "Delete Room";
            this.DeleteRoom.UseVisualStyleBackColor = true;
            this.DeleteRoom.Click += new System.EventHandler(this.DeleteRoom_Click);
            // 
            // RefreshRoomList
            // 
            this.RefreshRoomList.Location = new System.Drawing.Point(121, 11);
            this.RefreshRoomList.Name = "RefreshRoomList";
            this.RefreshRoomList.Size = new System.Drawing.Size(124, 23);
            this.RefreshRoomList.TabIndex = 10;
            this.RefreshRoomList.Text = "Refresh Room List";
            this.RefreshRoomList.UseVisualStyleBackColor = true;
            this.RefreshRoomList.Click += new System.EventHandler(this.RefreshRoomList_Click);
            // 
            // RoomContainer
            // 
            this.RoomContainer.AllowUserToAddRows = false;
            this.RoomContainer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RoomContainer.Location = new System.Drawing.Point(6, 37);
            this.RoomContainer.Name = "RoomContainer";
            this.RoomContainer.ReadOnly = true;
            this.RoomContainer.Size = new System.Drawing.Size(403, 231);
            this.RoomContainer.TabIndex = 0;
            // 
            // MessageSuccess
            // 
            this.MessageSuccess.AutoSize = true;
            this.MessageSuccess.ForeColor = System.Drawing.Color.Green;
            this.MessageSuccess.Location = new System.Drawing.Point(197, 141);
            this.MessageSuccess.Name = "MessageSuccess";
            this.MessageSuccess.Size = new System.Drawing.Size(48, 13);
            this.MessageSuccess.TabIndex = 8;
            this.MessageSuccess.Text = "Success";
            // 
            // MessageFail
            // 
            this.MessageFail.AutoSize = true;
            this.MessageFail.ForeColor = System.Drawing.Color.Red;
            this.MessageFail.Location = new System.Drawing.Point(197, 141);
            this.MessageFail.Name = "MessageFail";
            this.MessageFail.Size = new System.Drawing.Size(29, 13);
            this.MessageFail.TabIndex = 9;
            this.MessageFail.Text = "Error";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(194, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Auto Created Between 8000 and 9000";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RoomName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.MessageFail);
            this.groupBox2.Controls.Add(this.btnCreate);
            this.groupBox2.Controls.Add(this.MessageSuccess);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.PortNumber);
            this.groupBox2.Controls.Add(this.Password);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(415, 171);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create Room";
            // 
            // btnHostWebClient
            // 
            this.btnHostWebClient.Location = new System.Drawing.Point(14, 60);
            this.btnHostWebClient.Name = "btnHostWebClient";
            this.btnHostWebClient.Size = new System.Drawing.Size(110, 23);
            this.btnHostWebClient.TabIndex = 2;
            this.btnHostWebClient.Text = "Host Web Client";
            this.btnHostWebClient.UseVisualStyleBackColor = true;
            this.btnHostWebClient.Click += new System.EventHandler(this.btnHostWebClient_Click);
            // 
            // MessageWebHost
            // 
            this.MessageWebHost.AutoSize = true;
            this.MessageWebHost.ForeColor = System.Drawing.Color.Gray;
            this.MessageWebHost.Location = new System.Drawing.Point(132, 65);
            this.MessageWebHost.Name = "MessageWebHost";
            this.MessageWebHost.Size = new System.Drawing.Size(88, 13);
            this.MessageWebHost.TabIndex = 11;
            this.MessageWebHost.Text = "Web Host Status";
            // 
            // ClientUrl
            // 
            this.ClientUrl.AutoSize = true;
            this.ClientUrl.Location = new System.Drawing.Point(223, 65);
            this.ClientUrl.Name = "ClientUrl";
            this.ClientUrl.Size = new System.Drawing.Size(49, 13);
            this.ClientUrl.TabIndex = 3;
            this.ClientUrl.TabStop = true;
            this.ClientUrl.Text = "Client Url";
            this.ClientUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ClientUrl_LinkClicked);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.UrlToHostAt);
            this.groupBox3.Controls.Add(this.btnHostWebClient);
            this.groupBox3.Controls.Add(this.ClientUrl);
            this.groupBox3.Controls.Add(this.MessageWebHost);
            this.groupBox3.Location = new System.Drawing.Point(12, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(415, 100);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Web Host";
            // 
            // UrlToHostAt
            // 
            this.UrlToHostAt.Location = new System.Drawing.Point(6, 25);
            this.UrlToHostAt.Name = "UrlToHostAt";
            this.UrlToHostAt.Size = new System.Drawing.Size(403, 20);
            this.UrlToHostAt.TabIndex = 1;
            this.UrlToHostAt.Text = "http://localhost:5000/";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 579);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Poker Manager Console";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoomContainer)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.MaskedTextBox RoomName;
        private System.Windows.Forms.MaskedTextBox PortNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView RoomContainer;
        private System.Windows.Forms.Label MessageSuccess;
        private System.Windows.Forms.Label MessageFail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnHostWebClient;
        private System.Windows.Forms.Label MessageWebHost;
        private System.Windows.Forms.LinkLabel ClientUrl;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MaskedTextBox UrlToHostAt;
        private System.Windows.Forms.Button RefreshRoomList;
        private System.Windows.Forms.Button DeleteRoom;
    }
}

