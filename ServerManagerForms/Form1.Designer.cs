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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnBrowsePokerServerFile = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.PokerServerFilePath = new System.Windows.Forms.MaskedTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.WebHostRoomListTrigger = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.WebHostDefaultFile = new System.Windows.Forms.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnWebClientPath = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.UrlToHostAt = new System.Windows.Forms.MaskedTextBox();
            this.WebClientFolderPath = new System.Windows.Forms.MaskedTextBox();
            this.btnHostWebClient = new System.Windows.Forms.Button();
            this.ClientUrl = new System.Windows.Forms.LinkLabel();
            this.MessageWebHost = new System.Windows.Forms.Label();
            this.IP = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAutoDetectIP = new System.Windows.Forms.Button();
            this.FileDialogPokerServer = new System.Windows.Forms.OpenFileDialog();
            this.FolderBrowserWebClient = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.FailMessagePokerServerFileStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoomContainer)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Room Name";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(95, 173);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // RoomName
            // 
            this.RoomName.Location = new System.Drawing.Point(97, 27);
            this.RoomName.Name = "RoomName";
            this.RoomName.Size = new System.Drawing.Size(175, 20);
            this.RoomName.TabIndex = 4;
            // 
            // PortNumber
            // 
            this.PortNumber.Location = new System.Drawing.Point(97, 136);
            this.PortNumber.Name = "PortNumber";
            this.PortNumber.ReadOnly = true;
            this.PortNumber.Size = new System.Drawing.Size(70, 20);
            this.PortNumber.TabIndex = 20;
            this.PortNumber.Text = "8000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port No";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(97, 62);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(175, 20);
            this.Password.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 65);
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
            this.groupBox1.Location = new System.Drawing.Point(433, 279);
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
            this.MessageSuccess.Location = new System.Drawing.Point(176, 178);
            this.MessageSuccess.Name = "MessageSuccess";
            this.MessageSuccess.Size = new System.Drawing.Size(48, 13);
            this.MessageSuccess.TabIndex = 8;
            this.MessageSuccess.Text = "Success";
            // 
            // MessageFail
            // 
            this.MessageFail.AutoSize = true;
            this.MessageFail.ForeColor = System.Drawing.Color.Red;
            this.MessageFail.Location = new System.Drawing.Point(176, 178);
            this.MessageFail.Name = "MessageFail";
            this.MessageFail.Size = new System.Drawing.Size(29, 13);
            this.MessageFail.TabIndex = 9;
            this.MessageFail.Text = "Error";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(173, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Auto Created Between 8000 and 9000";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(832, 261);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Room";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.FailMessagePokerServerFileStatus);
            this.groupBox4.Controls.Add(this.btnBrowsePokerServerFile);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.PokerServerFilePath);
            this.groupBox4.Location = new System.Drawing.Point(6, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(397, 142);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Poker Room Server";
            // 
            // btnBrowsePokerServerFile
            // 
            this.btnBrowsePokerServerFile.Location = new System.Drawing.Point(328, 49);
            this.btnBrowsePokerServerFile.Name = "btnBrowsePokerServerFile";
            this.btnBrowsePokerServerFile.Size = new System.Drawing.Size(63, 23);
            this.btnBrowsePokerServerFile.TabIndex = 26;
            this.btnBrowsePokerServerFile.Text = "Browse";
            this.btnBrowsePokerServerFile.UseVisualStyleBackColor = true;
            this.btnBrowsePokerServerFile.Click += new System.EventHandler(this.btnBrowsePokerServerFile_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Poker Server File";
            // 
            // PokerServerFilePath
            // 
            this.PokerServerFilePath.Location = new System.Drawing.Point(6, 51);
            this.PokerServerFilePath.Name = "PokerServerFilePath";
            this.PokerServerFilePath.Size = new System.Drawing.Size(315, 20);
            this.PokerServerFilePath.TabIndex = 26;
            this.PokerServerFilePath.TextChanged += new System.EventHandler(this.PokerServerFilePath_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.WebHostRoomListTrigger);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.WebHostDefaultFile);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.btnWebClientPath);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.UrlToHostAt);
            this.groupBox3.Controls.Add(this.WebClientFolderPath);
            this.groupBox3.Controls.Add(this.btnHostWebClient);
            this.groupBox3.Controls.Add(this.ClientUrl);
            this.groupBox3.Controls.Add(this.MessageWebHost);
            this.groupBox3.Location = new System.Drawing.Point(421, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(397, 236);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Web Host";
            // 
            // WebHostRoomListTrigger
            // 
            this.WebHostRoomListTrigger.Location = new System.Drawing.Point(97, 61);
            this.WebHostRoomListTrigger.Name = "WebHostRoomListTrigger";
            this.WebHostRoomListTrigger.Size = new System.Drawing.Size(175, 20);
            this.WebHostRoomListTrigger.TabIndex = 31;
            this.WebHostRoomListTrigger.Text = "/getroomlist";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 61);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Room Name";
            // 
            // WebHostDefaultFile
            // 
            this.WebHostDefaultFile.Location = new System.Drawing.Point(97, 25);
            this.WebHostDefaultFile.Name = "WebHostDefaultFile";
            this.WebHostDefaultFile.Size = new System.Drawing.Size(175, 20);
            this.WebHostDefaultFile.TabIndex = 26;
            this.WebHostDefaultFile.Text = "index.html";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Default File";
            // 
            // btnWebClientPath
            // 
            this.btnWebClientPath.Location = new System.Drawing.Point(328, 121);
            this.btnWebClientPath.Name = "btnWebClientPath";
            this.btnWebClientPath.Size = new System.Drawing.Size(63, 23);
            this.btnWebClientPath.TabIndex = 27;
            this.btnWebClientPath.Text = "Browse";
            this.btnWebClientPath.UseVisualStyleBackColor = true;
            this.btnWebClientPath.Click += new System.EventHandler(this.btnWebClientPath_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 154);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Web Host URL";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "Web Client Folder";
            // 
            // UrlToHostAt
            // 
            this.UrlToHostAt.Location = new System.Drawing.Point(86, 151);
            this.UrlToHostAt.Name = "UrlToHostAt";
            this.UrlToHostAt.Size = new System.Drawing.Size(305, 20);
            this.UrlToHostAt.TabIndex = 1;
            this.UrlToHostAt.Text = "http://localhost:5000/";
            this.UrlToHostAt.TextChanged += new System.EventHandler(this.UrlToHostAt_TextChanged);
            // 
            // WebClientFolderPath
            // 
            this.WebClientFolderPath.Location = new System.Drawing.Point(7, 122);
            this.WebClientFolderPath.Name = "WebClientFolderPath";
            this.WebClientFolderPath.Size = new System.Drawing.Size(315, 20);
            this.WebClientFolderPath.TabIndex = 29;
            this.WebClientFolderPath.TextChanged += new System.EventHandler(this.WebClientFolderPath_TextChanged);
            // 
            // btnHostWebClient
            // 
            this.btnHostWebClient.Location = new System.Drawing.Point(14, 186);
            this.btnHostWebClient.Name = "btnHostWebClient";
            this.btnHostWebClient.Size = new System.Drawing.Size(110, 23);
            this.btnHostWebClient.TabIndex = 2;
            this.btnHostWebClient.Text = "Host Web Client";
            this.btnHostWebClient.UseVisualStyleBackColor = true;
            this.btnHostWebClient.Click += new System.EventHandler(this.btnHostWebClient_Click);
            // 
            // ClientUrl
            // 
            this.ClientUrl.AutoSize = true;
            this.ClientUrl.Location = new System.Drawing.Point(223, 191);
            this.ClientUrl.Name = "ClientUrl";
            this.ClientUrl.Size = new System.Drawing.Size(49, 13);
            this.ClientUrl.TabIndex = 3;
            this.ClientUrl.TabStop = true;
            this.ClientUrl.Text = "Client Url";
            this.ClientUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ClientUrl_LinkClicked);
            // 
            // MessageWebHost
            // 
            this.MessageWebHost.AutoSize = true;
            this.MessageWebHost.ForeColor = System.Drawing.Color.Gray;
            this.MessageWebHost.Location = new System.Drawing.Point(132, 191);
            this.MessageWebHost.Name = "MessageWebHost";
            this.MessageWebHost.Size = new System.Drawing.Size(88, 13);
            this.MessageWebHost.TabIndex = 11;
            this.MessageWebHost.Text = "Web Host Status";
            // 
            // IP
            // 
            this.IP.Location = new System.Drawing.Point(97, 98);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(108, 20);
            this.IP.TabIndex = 21;
            this.IP.Text = "127.0.0.1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Room IP Address";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(211, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Enter D for auto detect";
            // 
            // btnAutoDetectIP
            // 
            this.btnAutoDetectIP.Location = new System.Drawing.Point(332, 98);
            this.btnAutoDetectIP.Name = "btnAutoDetectIP";
            this.btnAutoDetectIP.Size = new System.Drawing.Size(74, 21);
            this.btnAutoDetectIP.TabIndex = 24;
            this.btnAutoDetectIP.Text = "Auto Detect";
            this.btnAutoDetectIP.UseVisualStyleBackColor = true;
            this.btnAutoDetectIP.Click += new System.EventHandler(this.btnAutoDetectIP_Click);
            // 
            // FileDialogPokerServer
            // 
            this.FileDialogPokerServer.FileName = "PokerServer";
            this.FileDialogPokerServer.Filter = "Executable(*.exe)|*.exe";
            this.FileDialogPokerServer.FileOk += new System.ComponentModel.CancelEventHandler(this.FileDialogPokerServer_FileOk);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.RoomName);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.btnAutoDetectIP);
            this.groupBox5.Controls.Add(this.Password);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.PortNumber);
            this.groupBox5.Controls.Add(this.IP);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.MessageSuccess);
            this.groupBox5.Controls.Add(this.btnCreate);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.MessageFail);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(9, 279);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(417, 238);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Create Room";
            // 
            // FailMessagePokerServerFileStatus
            // 
            this.FailMessagePokerServerFileStatus.AutoSize = true;
            this.FailMessagePokerServerFileStatus.ForeColor = System.Drawing.Color.Red;
            this.FailMessagePokerServerFileStatus.Location = new System.Drawing.Point(6, 84);
            this.FailMessagePokerServerFileStatus.Name = "FailMessagePokerServerFileStatus";
            this.FailMessagePokerServerFileStatus.Size = new System.Drawing.Size(23, 13);
            this.FailMessagePokerServerFileStatus.TabIndex = 27;
            this.FailMessagePokerServerFileStatus.Text = "Fail";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 562);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Poker Manager Console";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoomContainer)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
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
        private System.Windows.Forms.MaskedTextBox IP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAutoDetectIP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox PokerServerFilePath;
        private System.Windows.Forms.OpenFileDialog FileDialogPokerServer;
        private System.Windows.Forms.Button btnBrowsePokerServerFile;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserWebClient;
        private System.Windows.Forms.Button btnWebClientPath;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MaskedTextBox WebClientFolderPath;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.MaskedTextBox WebHostRoomListTrigger;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MaskedTextBox WebHostDefaultFile;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label FailMessagePokerServerFileStatus;
    }
}

