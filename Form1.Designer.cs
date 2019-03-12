namespace c_sharp_ais_manager_gui
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
            this.components = new System.ComponentModel.Container();
            this.bStart = new System.Windows.Forms.Button();
            this.tInfo = new System.Windows.Forms.RichTextBox();
            this.bGetTimeFromDevices = new System.Windows.Forms.Button();
            this.tKeepAlive = new System.Windows.Forms.Timer(this.components);
            this.ShellStatus = new System.Windows.Forms.Label();
            this.bStop = new System.Windows.Forms.Button();
            this.bGetLogs = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bClear = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tEndTime = new System.Windows.Forms.RichTextBox();
            this.tStartTime = new System.Windows.Forms.RichTextBox();
            this.tEndIndex = new System.Windows.Forms.RichTextBox();
            this.tStartIndex = new System.Windows.Forms.RichTextBox();
            this.TimeTo = new System.Windows.Forms.Label();
            this.EndIndex = new System.Windows.Forms.Label();
            this.bGetLogsByTime = new System.Windows.Forms.Button();
            this.bGetLogsByIndex = new System.Windows.Forms.Button();
            this.bReadWhitelistFromDB = new System.Windows.Forms.Button();
            this.bSetTimeToDevices = new System.Windows.Forms.Button();
            this.bReadWhitelistFromReader = new System.Windows.Forms.Button();
            this.bReadBlacklistFromReaders = new System.Windows.Forms.Button();
            this.TimeFrom = new System.Windows.Forms.Label();
            this.bReadBlacklistFromDB = new System.Windows.Forms.Button();
            this.StartIndex = new System.Windows.Forms.Label();
            this.bSendAllLogs = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gridIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridLogIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridLogAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridReaderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridSystemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridUIDLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdProgressBar = new System.Windows.Forms.ProgressBar();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(0, 19);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(75, 25);
            this.bStart.TabIndex = 0;
            this.bStart.Text = "START";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // tInfo
            // 
            this.tInfo.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tInfo.Location = new System.Drawing.Point(610, 12);
            this.tInfo.Name = "tInfo";
            this.tInfo.Size = new System.Drawing.Size(454, 265);
            this.tInfo.TabIndex = 1;
            this.tInfo.Text = "";
            // 
            // bGetTimeFromDevices
            // 
            this.bGetTimeFromDevices.Location = new System.Drawing.Point(318, 61);
            this.bGetTimeFromDevices.Name = "bGetTimeFromDevices";
            this.bGetTimeFromDevices.Size = new System.Drawing.Size(125, 25);
            this.bGetTimeFromDevices.TabIndex = 2;
            this.bGetTimeFromDevices.Text = "Get time from devices";
            this.bGetTimeFromDevices.UseVisualStyleBackColor = true;
            this.bGetTimeFromDevices.Click += new System.EventHandler(this.bGetTimeFromDevices_Click);
            // 
            // tKeepAlive
            // 
            this.tKeepAlive.Interval = 750;
            this.tKeepAlive.Tick += new System.EventHandler(this.tKeepAlive_Tick);
            // 
            // ShellStatus
            // 
            this.ShellStatus.AutoSize = true;
            this.ShellStatus.Location = new System.Drawing.Point(164, 25);
            this.ShellStatus.Name = "ShellStatus";
            this.ShellStatus.Size = new System.Drawing.Size(99, 13);
            this.ShellStatus.TabIndex = 3;
            this.ShellStatus.Text = "Shell is not running.";
            // 
            // bStop
            // 
            this.bStop.Location = new System.Drawing.Point(81, 19);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(75, 25);
            this.bStop.TabIndex = 4;
            this.bStop.Text = "STOP";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bGetLogs
            // 
            this.bGetLogs.Location = new System.Drawing.Point(318, 21);
            this.bGetLogs.Name = "bGetLogs";
            this.bGetLogs.Size = new System.Drawing.Size(75, 23);
            this.bGetLogs.TabIndex = 5;
            this.bGetLogs.Text = "Get logs";
            this.bGetLogs.UseVisualStyleBackColor = true;
            this.bGetLogs.Click += new System.EventHandler(this.bGetLogs_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bClear);
            this.groupBox1.Controls.Add(this.bStart);
            this.groupBox1.Controls.Add(this.bStop);
            this.groupBox1.Controls.Add(this.ShellStatus);
            this.groupBox1.Location = new System.Drawing.Point(21, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 61);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application";
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(279, 19);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(75, 25);
            this.bClear.TabIndex = 5;
            this.bClear.Text = "CLEAR";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tEndTime);
            this.groupBox2.Controls.Add(this.tStartTime);
            this.groupBox2.Controls.Add(this.tEndIndex);
            this.groupBox2.Controls.Add(this.tStartIndex);
            this.groupBox2.Controls.Add(this.TimeTo);
            this.groupBox2.Controls.Add(this.EndIndex);
            this.groupBox2.Controls.Add(this.bGetLogsByTime);
            this.groupBox2.Controls.Add(this.bGetLogsByIndex);
            this.groupBox2.Controls.Add(this.bReadWhitelistFromDB);
            this.groupBox2.Controls.Add(this.bSetTimeToDevices);
            this.groupBox2.Controls.Add(this.bReadWhitelistFromReader);
            this.groupBox2.Controls.Add(this.bReadBlacklistFromReaders);
            this.groupBox2.Controls.Add(this.TimeFrom);
            this.groupBox2.Controls.Add(this.bReadBlacklistFromDB);
            this.groupBox2.Controls.Add(this.StartIndex);
            this.groupBox2.Controls.Add(this.bSendAllLogs);
            this.groupBox2.Controls.Add(this.bGetTimeFromDevices);
            this.groupBox2.Controls.Add(this.bGetLogs);
            this.groupBox2.Location = new System.Drawing.Point(21, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(583, 186);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Commands";
            // 
            // tEndTime
            // 
            this.tEndTime.Location = new System.Drawing.Point(276, 131);
            this.tEndTime.Name = "tEndTime";
            this.tEndTime.Size = new System.Drawing.Size(125, 25);
            this.tEndTime.TabIndex = 13;
            this.tEndTime.Text = "hh:mm:ss dd.mm.yyyy";
            // 
            // tStartTime
            // 
            this.tStartTime.Location = new System.Drawing.Point(72, 128);
            this.tStartTime.Name = "tStartTime";
            this.tStartTime.Size = new System.Drawing.Size(125, 25);
            this.tStartTime.TabIndex = 14;
            this.tStartTime.Text = "hh:mm:ss dd.mm.yyyy";
            // 
            // tEndIndex
            // 
            this.tEndIndex.Location = new System.Drawing.Point(276, 100);
            this.tEndIndex.Name = "tEndIndex";
            this.tEndIndex.Size = new System.Drawing.Size(125, 25);
            this.tEndIndex.TabIndex = 12;
            this.tEndIndex.Text = "";
            // 
            // tStartIndex
            // 
            this.tStartIndex.Location = new System.Drawing.Point(72, 97);
            this.tStartIndex.Name = "tStartIndex";
            this.tStartIndex.Size = new System.Drawing.Size(125, 25);
            this.tStartIndex.TabIndex = 15;
            this.tStartIndex.Text = "";
            // 
            // TimeTo
            // 
            this.TimeTo.AutoSize = true;
            this.TimeTo.Location = new System.Drawing.Point(215, 137);
            this.TimeTo.Name = "TimeTo";
            this.TimeTo.Size = new System.Drawing.Size(45, 13);
            this.TimeTo.TabIndex = 10;
            this.TimeTo.Text = "Time to:";
            // 
            // EndIndex
            // 
            this.EndIndex.AutoSize = true;
            this.EndIndex.Location = new System.Drawing.Point(206, 106);
            this.EndIndex.Name = "EndIndex";
            this.EndIndex.Size = new System.Drawing.Size(57, 13);
            this.EndIndex.TabIndex = 11;
            this.EndIndex.Text = "End index:";
            // 
            // bGetLogsByTime
            // 
            this.bGetLogsByTime.Location = new System.Drawing.Point(429, 137);
            this.bGetLogsByTime.Name = "bGetLogsByTime";
            this.bGetLogsByTime.Size = new System.Drawing.Size(125, 25);
            this.bGetLogsByTime.TabIndex = 8;
            this.bGetLogsByTime.Text = "Get logs by time";
            this.bGetLogsByTime.UseVisualStyleBackColor = true;
            this.bGetLogsByTime.Click += new System.EventHandler(this.bGetLogsByTime_Click);
            // 
            // bGetLogsByIndex
            // 
            this.bGetLogsByIndex.Location = new System.Drawing.Point(429, 100);
            this.bGetLogsByIndex.Name = "bGetLogsByIndex";
            this.bGetLogsByIndex.Size = new System.Drawing.Size(125, 25);
            this.bGetLogsByIndex.TabIndex = 14;
            this.bGetLogsByIndex.Text = "Get logs by index";
            this.bGetLogsByIndex.UseVisualStyleBackColor = true;
            this.bGetLogsByIndex.Click += new System.EventHandler(this.bGetLogsByIndex_Click);
            // 
            // bReadWhitelistFromDB
            // 
            this.bReadWhitelistFromDB.Location = new System.Drawing.Point(6, 19);
            this.bReadWhitelistFromDB.Name = "bReadWhitelistFromDB";
            this.bReadWhitelistFromDB.Size = new System.Drawing.Size(150, 25);
            this.bReadWhitelistFromDB.TabIndex = 8;
            this.bReadWhitelistFromDB.Text = "Read whitelist from DB";
            this.bReadWhitelistFromDB.UseVisualStyleBackColor = true;
            this.bReadWhitelistFromDB.Click += new System.EventHandler(this.bReadWhitelistFromDB_Click);
            // 
            // bSetTimeToDevices
            // 
            this.bSetTimeToDevices.Location = new System.Drawing.Point(449, 61);
            this.bSetTimeToDevices.Name = "bSetTimeToDevices";
            this.bSetTimeToDevices.Size = new System.Drawing.Size(125, 25);
            this.bSetTimeToDevices.TabIndex = 13;
            this.bSetTimeToDevices.Text = "Set time to devices";
            this.bSetTimeToDevices.UseVisualStyleBackColor = true;
            this.bSetTimeToDevices.Click += new System.EventHandler(this.bSetTimeToDevices_Click);
            // 
            // bReadWhitelistFromReader
            // 
            this.bReadWhitelistFromReader.Location = new System.Drawing.Point(162, 19);
            this.bReadWhitelistFromReader.Name = "bReadWhitelistFromReader";
            this.bReadWhitelistFromReader.Size = new System.Drawing.Size(150, 25);
            this.bReadWhitelistFromReader.TabIndex = 9;
            this.bReadWhitelistFromReader.Text = "Read whitelist from readers";
            this.bReadWhitelistFromReader.UseVisualStyleBackColor = true;
            this.bReadWhitelistFromReader.Click += new System.EventHandler(this.bReadWhitelistFromReader_Click);
            // 
            // bReadBlacklistFromReaders
            // 
            this.bReadBlacklistFromReaders.Location = new System.Drawing.Point(162, 61);
            this.bReadBlacklistFromReaders.Name = "bReadBlacklistFromReaders";
            this.bReadBlacklistFromReaders.Size = new System.Drawing.Size(150, 25);
            this.bReadBlacklistFromReaders.TabIndex = 12;
            this.bReadBlacklistFromReaders.Text = "Read blacklist from readers";
            this.bReadBlacklistFromReaders.UseVisualStyleBackColor = true;
            this.bReadBlacklistFromReaders.Click += new System.EventHandler(this.bReadBlacklistFromReaders_Click);
            // 
            // TimeFrom
            // 
            this.TimeFrom.AutoSize = true;
            this.TimeFrom.Location = new System.Drawing.Point(6, 132);
            this.TimeFrom.Name = "TimeFrom";
            this.TimeFrom.Size = new System.Drawing.Size(56, 13);
            this.TimeFrom.TabIndex = 9;
            this.TimeFrom.Text = "Time from:";
            // 
            // bReadBlacklistFromDB
            // 
            this.bReadBlacklistFromDB.Location = new System.Drawing.Point(6, 59);
            this.bReadBlacklistFromDB.Name = "bReadBlacklistFromDB";
            this.bReadBlacklistFromDB.Size = new System.Drawing.Size(150, 25);
            this.bReadBlacklistFromDB.TabIndex = 10;
            this.bReadBlacklistFromDB.Text = "Read blacklist from DB";
            this.bReadBlacklistFromDB.UseVisualStyleBackColor = true;
            this.bReadBlacklistFromDB.Click += new System.EventHandler(this.bReadBlacklistFromDB_Click);
            // 
            // StartIndex
            // 
            this.StartIndex.AutoSize = true;
            this.StartIndex.Location = new System.Drawing.Point(6, 100);
            this.StartIndex.Name = "StartIndex";
            this.StartIndex.Size = new System.Drawing.Size(60, 13);
            this.StartIndex.TabIndex = 8;
            this.StartIndex.Text = "Start index:";
            // 
            // bSendAllLogs
            // 
            this.bSendAllLogs.Location = new System.Drawing.Point(399, 21);
            this.bSendAllLogs.Name = "bSendAllLogs";
            this.bSendAllLogs.Size = new System.Drawing.Size(75, 23);
            this.bSendAllLogs.TabIndex = 11;
            this.bSendAllLogs.Text = "Send all logs";
            this.bSendAllLogs.UseVisualStyleBackColor = true;
            this.bSendAllLogs.Click += new System.EventHandler(this.bSendAllLogs_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonShadow;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridIndex,
            this.gridLogIndex,
            this.gridLogAction,
            this.gridReaderID,
            this.gridCardID,
            this.gridSystemID,
            this.gridUID,
            this.gridUIDLength,
            this.gridTime,
            this.gridType});
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridView1.Location = new System.Drawing.Point(21, 283);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1040, 310);
            this.dataGridView1.TabIndex = 8;
            // 
            // gridIndex
            // 
            this.gridIndex.HeaderText = "#";
            this.gridIndex.Name = "gridIndex";
            // 
            // gridLogIndex
            // 
            this.gridLogIndex.HeaderText = "Log index";
            this.gridLogIndex.Name = "gridLogIndex";
            // 
            // gridLogAction
            // 
            this.gridLogAction.HeaderText = "Log action";
            this.gridLogAction.Name = "gridLogAction";
            // 
            // gridReaderID
            // 
            this.gridReaderID.HeaderText = "Reader ID";
            this.gridReaderID.Name = "gridReaderID";
            // 
            // gridCardID
            // 
            this.gridCardID.HeaderText = "Card ID";
            this.gridCardID.Name = "gridCardID";
            // 
            // gridSystemID
            // 
            this.gridSystemID.HeaderText = "System ID";
            this.gridSystemID.Name = "gridSystemID";
            // 
            // gridUID
            // 
            this.gridUID.HeaderText = "UID";
            this.gridUID.Name = "gridUID";
            // 
            // gridUIDLength
            // 
            this.gridUIDLength.HeaderText = "UID length";
            this.gridUIDLength.Name = "gridUIDLength";
            // 
            // gridTime
            // 
            this.gridTime.HeaderText = "Time";
            this.gridTime.Name = "gridTime";
            // 
            // gridType
            // 
            this.gridType.HeaderText = "Type";
            this.gridType.Name = "gridType";
            // 
            // cmdProgressBar
            // 
            this.cmdProgressBar.Location = new System.Drawing.Point(395, 31);
            this.cmdProgressBar.Name = "cmdProgressBar";
            this.cmdProgressBar.Size = new System.Drawing.Size(200, 25);
            this.cmdProgressBar.TabIndex = 9;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Location = new System.Drawing.Point(394, 15);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(96, 13);
            this.ProgressLabel.TabIndex = 10;
            this.ProgressLabel.Text = "command progress";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 593);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.cmdProgressBar);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tInfo);
            this.Name = "Form1";
            this.Text = "C#-AIS-MANAGER";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.RichTextBox tInfo;
        private System.Windows.Forms.Button bGetTimeFromDevices;
        private System.Windows.Forms.Timer tKeepAlive;
        private System.Windows.Forms.Label ShellStatus;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bGetLogs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bReadWhitelistFromDB;
        private System.Windows.Forms.Button bReadWhitelistFromReader;
        private System.Windows.Forms.Button bReadBlacklistFromDB;
        private System.Windows.Forms.Button bSendAllLogs;
        private System.Windows.Forms.Button bReadBlacklistFromReaders;
        private System.Windows.Forms.Button bSetTimeToDevices;
        private System.Windows.Forms.Button bGetLogsByIndex;
        private System.Windows.Forms.Button bGetLogsByTime;
        private System.Windows.Forms.RichTextBox tStartIndex;
        private System.Windows.Forms.Label TimeFrom;
        private System.Windows.Forms.Label StartIndex;
        private System.Windows.Forms.Label TimeTo;
        private System.Windows.Forms.Label EndIndex;
        private System.Windows.Forms.RichTextBox tEndIndex;
        private System.Windows.Forms.RichTextBox tEndTime;
        private System.Windows.Forms.RichTextBox tStartTime;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridLogIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridLogAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridReaderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridSystemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridUIDLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridType;
        private System.Windows.Forms.ProgressBar cmdProgressBar;
        private System.Windows.Forms.Label ProgressLabel;
    }
}

