namespace SPApi.Controls.UI
{
    partial class LogBox
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tc_Logs = new System.Windows.Forms.TabControl();
            this.t_All = new System.Windows.Forms.TabPage();
            this.lst_ALL = new System.Windows.Forms.ListBox();
            this.t_Info = new System.Windows.Forms.TabPage();
            this.lst_INFO = new System.Windows.Forms.ListBox();
            this.t_Error = new System.Windows.Forms.TabPage();
            this.lst_ERROR = new System.Windows.Forms.ListBox();
            this.t_Ulink = new System.Windows.Forms.TabPage();
            this.lst_ULINK = new System.Windows.Forms.ListBox();
            this.t_Plc = new System.Windows.Forms.TabPage();
            this.lst_PLC = new System.Windows.Forms.ListBox();
            this.t_Upload = new System.Windows.Forms.TabPage();
            this.lst_UPLOAD = new System.Windows.Forms.ListBox();
            this.t_Download = new System.Windows.Forms.TabPage();
            this.lst_DOWNLOAD = new System.Windows.Forms.ListBox();
            this.t_BackUp = new System.Windows.Forms.TabPage();
            this.lst_BACKUP = new System.Windows.Forms.ListBox();
            this.t_Rfid = new System.Windows.Forms.TabPage();
            this.lst_RFID = new System.Windows.Forms.ListBox();
            this.btn_Up = new SPApi.Controls.UI.SButton();
            this.btn_Down = new SPApi.Controls.UI.SButton();
            this.lst_FAIL = new System.Windows.Forms.ListBox();
            this.t_Fail = new System.Windows.Forms.TabPage();
            this.tc_Logs.SuspendLayout();
            this.t_All.SuspendLayout();
            this.t_Info.SuspendLayout();
            this.t_Error.SuspendLayout();
            this.t_Ulink.SuspendLayout();
            this.t_Plc.SuspendLayout();
            this.t_Upload.SuspendLayout();
            this.t_Download.SuspendLayout();
            this.t_BackUp.SuspendLayout();
            this.t_Rfid.SuspendLayout();
            this.t_Fail.SuspendLayout();
            this.SuspendLayout();
            // 
            // tc_Logs
            // 
            this.tc_Logs.Controls.Add(this.t_All);
            this.tc_Logs.Controls.Add(this.t_Info);
            this.tc_Logs.Controls.Add(this.t_Error);
            this.tc_Logs.Controls.Add(this.t_Ulink);
            this.tc_Logs.Controls.Add(this.t_Plc);
            this.tc_Logs.Controls.Add(this.t_Upload);
            this.tc_Logs.Controls.Add(this.t_Download);
            this.tc_Logs.Controls.Add(this.t_BackUp);
            this.tc_Logs.Controls.Add(this.t_Rfid);
            this.tc_Logs.Controls.Add(this.t_Fail);
            this.tc_Logs.Cursor = System.Windows.Forms.Cursors.Default;
            this.tc_Logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_Logs.Font = new System.Drawing.Font("Tahoma", 9F);
            this.tc_Logs.Location = new System.Drawing.Point(0, 0);
            this.tc_Logs.Margin = new System.Windows.Forms.Padding(0);
            this.tc_Logs.Name = "tc_Logs";
            this.tc_Logs.SelectedIndex = 0;
            this.tc_Logs.Size = new System.Drawing.Size(554, 56);
            this.tc_Logs.TabIndex = 0;
            // 
            // t_All
            // 
            this.t_All.BackColor = System.Drawing.SystemColors.Control;
            this.t_All.Controls.Add(this.lst_ALL);
            this.t_All.Location = new System.Drawing.Point(4, 23);
            this.t_All.Margin = new System.Windows.Forms.Padding(0);
            this.t_All.Name = "t_All";
            this.t_All.Size = new System.Drawing.Size(546, 29);
            this.t_All.TabIndex = 7;
            this.t_All.Text = "ALL";
            // 
            // lst_ALL
            // 
            this.lst_ALL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_ALL.FormattingEnabled = true;
            this.lst_ALL.HorizontalScrollbar = true;
            this.lst_ALL.IntegralHeight = false;
            this.lst_ALL.ItemHeight = 14;
            this.lst_ALL.Location = new System.Drawing.Point(0, 0);
            this.lst_ALL.Margin = new System.Windows.Forms.Padding(0);
            this.lst_ALL.Name = "lst_ALL";
            this.lst_ALL.Size = new System.Drawing.Size(546, 29);
            this.lst_ALL.TabIndex = 1;
            // 
            // t_Info
            // 
            this.t_Info.BackColor = System.Drawing.SystemColors.Control;
            this.t_Info.Controls.Add(this.lst_INFO);
            this.t_Info.Font = new System.Drawing.Font("Tahoma", 9F);
            this.t_Info.Location = new System.Drawing.Point(4, 23);
            this.t_Info.Name = "t_Info";
            this.t_Info.Size = new System.Drawing.Size(546, 29);
            this.t_Info.TabIndex = 0;
            this.t_Info.Text = "INFO";
            // 
            // lst_INFO
            // 
            this.lst_INFO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_INFO.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_INFO.FormattingEnabled = true;
            this.lst_INFO.HorizontalScrollbar = true;
            this.lst_INFO.IntegralHeight = false;
            this.lst_INFO.ItemHeight = 14;
            this.lst_INFO.Location = new System.Drawing.Point(0, 0);
            this.lst_INFO.Margin = new System.Windows.Forms.Padding(0);
            this.lst_INFO.Name = "lst_INFO";
            this.lst_INFO.Size = new System.Drawing.Size(546, 29);
            this.lst_INFO.TabIndex = 0;
            // 
            // t_Error
            // 
            this.t_Error.BackColor = System.Drawing.SystemColors.Control;
            this.t_Error.Controls.Add(this.lst_ERROR);
            this.t_Error.Location = new System.Drawing.Point(4, 23);
            this.t_Error.Name = "t_Error";
            this.t_Error.Size = new System.Drawing.Size(546, 29);
            this.t_Error.TabIndex = 1;
            this.t_Error.Text = "ERROR";
            // 
            // lst_ERROR
            // 
            this.lst_ERROR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_ERROR.FormattingEnabled = true;
            this.lst_ERROR.HorizontalScrollbar = true;
            this.lst_ERROR.IntegralHeight = false;
            this.lst_ERROR.ItemHeight = 14;
            this.lst_ERROR.Location = new System.Drawing.Point(0, 0);
            this.lst_ERROR.Margin = new System.Windows.Forms.Padding(0);
            this.lst_ERROR.Name = "lst_ERROR";
            this.lst_ERROR.Size = new System.Drawing.Size(546, 29);
            this.lst_ERROR.TabIndex = 1;
            // 
            // t_Ulink
            // 
            this.t_Ulink.BackColor = System.Drawing.SystemColors.Control;
            this.t_Ulink.Controls.Add(this.lst_ULINK);
            this.t_Ulink.Location = new System.Drawing.Point(4, 23);
            this.t_Ulink.Name = "t_Ulink";
            this.t_Ulink.Size = new System.Drawing.Size(546, 29);
            this.t_Ulink.TabIndex = 2;
            this.t_Ulink.Text = "ULINK";
            // 
            // lst_ULINK
            // 
            this.lst_ULINK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_ULINK.FormattingEnabled = true;
            this.lst_ULINK.HorizontalScrollbar = true;
            this.lst_ULINK.IntegralHeight = false;
            this.lst_ULINK.ItemHeight = 14;
            this.lst_ULINK.Location = new System.Drawing.Point(0, 0);
            this.lst_ULINK.Margin = new System.Windows.Forms.Padding(0);
            this.lst_ULINK.Name = "lst_ULINK";
            this.lst_ULINK.Size = new System.Drawing.Size(546, 29);
            this.lst_ULINK.TabIndex = 1;
            // 
            // t_Plc
            // 
            this.t_Plc.BackColor = System.Drawing.SystemColors.Control;
            this.t_Plc.Controls.Add(this.lst_PLC);
            this.t_Plc.Location = new System.Drawing.Point(4, 23);
            this.t_Plc.Name = "t_Plc";
            this.t_Plc.Size = new System.Drawing.Size(546, 29);
            this.t_Plc.TabIndex = 3;
            this.t_Plc.Text = "PLC";
            // 
            // lst_PLC
            // 
            this.lst_PLC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_PLC.FormattingEnabled = true;
            this.lst_PLC.HorizontalScrollbar = true;
            this.lst_PLC.IntegralHeight = false;
            this.lst_PLC.ItemHeight = 14;
            this.lst_PLC.Location = new System.Drawing.Point(0, 0);
            this.lst_PLC.Margin = new System.Windows.Forms.Padding(0);
            this.lst_PLC.Name = "lst_PLC";
            this.lst_PLC.Size = new System.Drawing.Size(546, 29);
            this.lst_PLC.TabIndex = 1;
            // 
            // t_Upload
            // 
            this.t_Upload.BackColor = System.Drawing.SystemColors.Control;
            this.t_Upload.Controls.Add(this.lst_UPLOAD);
            this.t_Upload.Location = new System.Drawing.Point(4, 23);
            this.t_Upload.Name = "t_Upload";
            this.t_Upload.Size = new System.Drawing.Size(546, 29);
            this.t_Upload.TabIndex = 4;
            this.t_Upload.Text = "UPLOAD";
            // 
            // lst_UPLOAD
            // 
            this.lst_UPLOAD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_UPLOAD.FormattingEnabled = true;
            this.lst_UPLOAD.HorizontalScrollbar = true;
            this.lst_UPLOAD.IntegralHeight = false;
            this.lst_UPLOAD.ItemHeight = 14;
            this.lst_UPLOAD.Location = new System.Drawing.Point(0, 0);
            this.lst_UPLOAD.Margin = new System.Windows.Forms.Padding(0);
            this.lst_UPLOAD.Name = "lst_UPLOAD";
            this.lst_UPLOAD.Size = new System.Drawing.Size(546, 29);
            this.lst_UPLOAD.TabIndex = 1;
            // 
            // t_Download
            // 
            this.t_Download.BackColor = System.Drawing.SystemColors.Control;
            this.t_Download.Controls.Add(this.lst_DOWNLOAD);
            this.t_Download.Location = new System.Drawing.Point(4, 23);
            this.t_Download.Name = "t_Download";
            this.t_Download.Size = new System.Drawing.Size(546, 29);
            this.t_Download.TabIndex = 5;
            this.t_Download.Text = "DOWNLOAD";
            // 
            // lst_DOWNLOAD
            // 
            this.lst_DOWNLOAD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_DOWNLOAD.FormattingEnabled = true;
            this.lst_DOWNLOAD.HorizontalScrollbar = true;
            this.lst_DOWNLOAD.IntegralHeight = false;
            this.lst_DOWNLOAD.ItemHeight = 14;
            this.lst_DOWNLOAD.Location = new System.Drawing.Point(0, 0);
            this.lst_DOWNLOAD.Margin = new System.Windows.Forms.Padding(0);
            this.lst_DOWNLOAD.Name = "lst_DOWNLOAD";
            this.lst_DOWNLOAD.Size = new System.Drawing.Size(546, 29);
            this.lst_DOWNLOAD.TabIndex = 1;
            // 
            // t_BackUp
            // 
            this.t_BackUp.BackColor = System.Drawing.SystemColors.Control;
            this.t_BackUp.Controls.Add(this.lst_BACKUP);
            this.t_BackUp.Location = new System.Drawing.Point(4, 23);
            this.t_BackUp.Name = "t_BackUp";
            this.t_BackUp.Size = new System.Drawing.Size(546, 29);
            this.t_BackUp.TabIndex = 6;
            this.t_BackUp.Text = "BACKUP";
            // 
            // lst_BACKUP
            // 
            this.lst_BACKUP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_BACKUP.FormattingEnabled = true;
            this.lst_BACKUP.HorizontalScrollbar = true;
            this.lst_BACKUP.IntegralHeight = false;
            this.lst_BACKUP.ItemHeight = 14;
            this.lst_BACKUP.Location = new System.Drawing.Point(0, 0);
            this.lst_BACKUP.Margin = new System.Windows.Forms.Padding(0);
            this.lst_BACKUP.Name = "lst_BACKUP";
            this.lst_BACKUP.Size = new System.Drawing.Size(546, 29);
            this.lst_BACKUP.TabIndex = 1;
            // 
            // t_Rfid
            // 
            this.t_Rfid.BackColor = System.Drawing.Color.White;
            this.t_Rfid.Controls.Add(this.lst_RFID);
            this.t_Rfid.Location = new System.Drawing.Point(4, 23);
            this.t_Rfid.Margin = new System.Windows.Forms.Padding(0);
            this.t_Rfid.Name = "t_Rfid";
            this.t_Rfid.Size = new System.Drawing.Size(546, 29);
            this.t_Rfid.TabIndex = 8;
            this.t_Rfid.Text = "RFID";
            // 
            // lst_RFID
            // 
            this.lst_RFID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_RFID.FormattingEnabled = true;
            this.lst_RFID.HorizontalScrollbar = true;
            this.lst_RFID.IntegralHeight = false;
            this.lst_RFID.ItemHeight = 14;
            this.lst_RFID.Location = new System.Drawing.Point(0, 0);
            this.lst_RFID.Margin = new System.Windows.Forms.Padding(0);
            this.lst_RFID.Name = "lst_RFID";
            this.lst_RFID.Size = new System.Drawing.Size(546, 29);
            this.lst_RFID.TabIndex = 2;
            // 
            // btn_Up
            // 
            this.btn_Up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Up.BorderColor = System.Drawing.Color.Black;
            this.btn_Up.BorderStyle = SPApi.Controls.UI.CustomBorderStyle.RaisedInner;
            this.btn_Up.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Up.Font = new System.Drawing.Font("맑은 고딕", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Up.Location = new System.Drawing.Point(526, 1);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(26, 10);
            this.btn_Up.TabIndex = 2;
            this.btn_Up.Text = "▲";
            this.btn_Up.Visible = false;
            this.btn_Up.Click += new System.EventHandler(this.btn_UpDown_Click);
            // 
            // btn_Down
            // 
            this.btn_Down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Down.BorderColor = System.Drawing.Color.Black;
            this.btn_Down.BorderStyle = SPApi.Controls.UI.CustomBorderStyle.RaisedInner;
            this.btn_Down.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Down.Font = new System.Drawing.Font("맑은 고딕", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Down.Location = new System.Drawing.Point(526, 10);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(26, 10);
            this.btn_Down.TabIndex = 1;
            this.btn_Down.Text = "▼";
            this.btn_Down.Visible = false;
            this.btn_Down.Click += new System.EventHandler(this.btn_UpDown_Click);
            // 
            // lst_FAIL
            // 
            this.lst_FAIL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_FAIL.FormattingEnabled = true;
            this.lst_FAIL.HorizontalScrollbar = true;
            this.lst_FAIL.IntegralHeight = false;
            this.lst_FAIL.ItemHeight = 14;
            this.lst_FAIL.Location = new System.Drawing.Point(0, 0);
            this.lst_FAIL.Margin = new System.Windows.Forms.Padding(0);
            this.lst_FAIL.Name = "lst_FAIL";
            this.lst_FAIL.Size = new System.Drawing.Size(546, 29);
            this.lst_FAIL.TabIndex = 2;
            // 
            // t_Fail
            // 
            this.t_Fail.Controls.Add(this.lst_FAIL);
            this.t_Fail.Location = new System.Drawing.Point(4, 23);
            this.t_Fail.Margin = new System.Windows.Forms.Padding(0);
            this.t_Fail.Name = "t_Fail";
            this.t_Fail.Size = new System.Drawing.Size(546, 29);
            this.t_Fail.TabIndex = 9;
            this.t_Fail.Text = "FAIL";
            this.t_Fail.UseVisualStyleBackColor = true;
            // 
            // LogBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btn_Up);
            this.Controls.Add(this.btn_Down);
            this.Controls.Add(this.tc_Logs);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(0, 21);
            this.Name = "LogBox";
            this.Size = new System.Drawing.Size(554, 56);
            this.Load += new System.EventHandler(this.LogBox_Load);
            this.tc_Logs.ResumeLayout(false);
            this.t_All.ResumeLayout(false);
            this.t_Info.ResumeLayout(false);
            this.t_Error.ResumeLayout(false);
            this.t_Ulink.ResumeLayout(false);
            this.t_Plc.ResumeLayout(false);
            this.t_Upload.ResumeLayout(false);
            this.t_Download.ResumeLayout(false);
            this.t_BackUp.ResumeLayout(false);
            this.t_Rfid.ResumeLayout(false);
            this.t_Fail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tc_Logs;
        private System.Windows.Forms.TabPage t_Info;
        private System.Windows.Forms.ListBox lst_INFO;
        private System.Windows.Forms.TabPage t_Error;
        private System.Windows.Forms.TabPage t_Ulink;
        private System.Windows.Forms.TabPage t_Plc;
        private System.Windows.Forms.TabPage t_Upload;
        private System.Windows.Forms.TabPage t_Download;
        private System.Windows.Forms.TabPage t_BackUp;
        private System.Windows.Forms.TabPage t_All;
        private System.Windows.Forms.ListBox lst_ALL;
        private System.Windows.Forms.ListBox lst_ERROR;
        private System.Windows.Forms.ListBox lst_ULINK;
        private System.Windows.Forms.ListBox lst_PLC;
        private System.Windows.Forms.ListBox lst_UPLOAD;
        private System.Windows.Forms.ListBox lst_DOWNLOAD;
        private System.Windows.Forms.ListBox lst_BACKUP;
        private SButton btn_Down;
        private SButton btn_Up;
        private System.Windows.Forms.TabPage t_Rfid;
        private System.Windows.Forms.ListBox lst_RFID;
        private System.Windows.Forms.TabPage t_Fail;
        private System.Windows.Forms.ListBox lst_FAIL;
    }
}
