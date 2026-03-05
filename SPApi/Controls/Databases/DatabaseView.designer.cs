namespace SPApi.Controls.Databases
{
    partial class DatabaseView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseView));
            this.imgTrans = new System.Windows.Forms.ImageList(this.components);
            this.cTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstLog = new System.Windows.Forms.ListView();
            this.cQuery = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_bar = new System.Windows.Forms.Label();
            this.pn_Top = new System.Windows.Forms.Panel();
            this.btn_WinState = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.lbl_Tilte = new System.Windows.Forms.Label();
            this.pnl_Center = new System.Windows.Forms.Panel();
            this.gb_Info = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_RunMode = new System.Windows.Forms.Label();
            this.lbl_cap_ip = new System.Windows.Forms.Label();
            this.lblSystem = new System.Windows.Forms.Label();
            this.lblUid = new System.Windows.Forms.Label();
            this.lblDbName = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_cap_state = new System.Windows.Forms.Label();
            this.lbl_MessageWaitCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblState = new SPApi.Controls.UI.SLabel();
            this.tlp_Main.SuspendLayout();
            this.pn_Top.SuspendLayout();
            this.pnl_Center.SuspendLayout();
            this.gb_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgTrans
            // 
            this.imgTrans.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTrans.ImageStream")));
            this.imgTrans.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTrans.Images.SetKeyName(0, "fail.jpg");
            this.imgTrans.Images.SetKeyName(1, "success.jpg");
            // 
            // cTime
            // 
            this.cTime.Text = "Time";
            this.cTime.Width = 180;
            // 
            // cIcon
            // 
            this.cIcon.Text = "";
            this.cIcon.Width = 30;
            // 
            // lstLog
            // 
            this.lstLog.BackColor = System.Drawing.SystemColors.Window;
            this.lstLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cIcon,
            this.cTime,
            this.cQuery});
            this.tlp_Main.SetColumnSpan(this.lstLog, 2);
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lstLog.FullRowSelect = true;
            this.lstLog.GridLines = true;
            this.lstLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstLog.Location = new System.Drawing.Point(0, 130);
            this.lstLog.Margin = new System.Windows.Forms.Padding(0);
            this.lstLog.MultiSelect = false;
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(1008, 600);
            this.lstLog.SmallImageList = this.imgTrans;
            this.lstLog.TabIndex = 7;
            this.lstLog.UseCompatibleStateImageBehavior = false;
            this.lstLog.View = System.Windows.Forms.View.Details;
            this.lstLog.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstLog_KeyDown);
            this.lstLog.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstLog_MouseDoubleClick);
            // 
            // cQuery
            // 
            this.cQuery.Text = "Query";
            this.cQuery.Width = 605;
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 2;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.lbl_bar, 0, 1);
            this.tlp_Main.Controls.Add(this.pn_Top, 0, 0);
            this.tlp_Main.Controls.Add(this.pnl_Center, 0, 2);
            this.tlp_Main.Controls.Add(this.lstLog, 0, 3);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 4;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(1008, 730);
            this.tlp_Main.TabIndex = 2;
            // 
            // lbl_bar
            // 
            this.lbl_bar.BackColor = System.Drawing.Color.DarkBlue;
            this.lbl_bar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_bar.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_bar.Location = new System.Drawing.Point(1, 50);
            this.lbl_bar.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lbl_bar.Name = "lbl_bar";
            this.lbl_bar.Size = new System.Drawing.Size(148, 3);
            this.lbl_bar.TabIndex = 5;
            this.lbl_bar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pn_Top
            // 
            this.pn_Top.BackColor = System.Drawing.Color.White;
            this.tlp_Main.SetColumnSpan(this.pn_Top, 2);
            this.pn_Top.Controls.Add(this.btn_WinState);
            this.pn_Top.Controls.Add(this.btnHide);
            this.pn_Top.Controls.Add(this.lbl_Tilte);
            this.pn_Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_Top.Location = new System.Drawing.Point(1, 1);
            this.pn_Top.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.pn_Top.Name = "pn_Top";
            this.pn_Top.Size = new System.Drawing.Size(1006, 49);
            this.pn_Top.TabIndex = 6;
            // 
            // btn_WinState
            // 
            this.btn_WinState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_WinState.BackColor = System.Drawing.SystemColors.Control;
            this.btn_WinState.BackgroundImage = global::SPApi.Properties.Resources.btn_FullScreen;
            this.btn_WinState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_WinState.Location = new System.Drawing.Point(927, 9);
            this.btn_WinState.Name = "btn_WinState";
            this.btn_WinState.Size = new System.Drawing.Size(33, 33);
            this.btn_WinState.TabIndex = 8;
            this.btn_WinState.UseVisualStyleBackColor = false;
            this.btn_WinState.Click += new System.EventHandler(this.btn_WinState_Click);
            // 
            // btnHide
            // 
            this.btnHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHide.BackColor = System.Drawing.SystemColors.Control;
            this.btnHide.BackgroundImage = global::SPApi.Properties.Resources.btn_Min;
            this.btnHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHide.Location = new System.Drawing.Point(962, 9);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(33, 33);
            this.btnHide.TabIndex = 7;
            this.btnHide.UseVisualStyleBackColor = false;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // lbl_Tilte
            // 
            this.lbl_Tilte.BackColor = System.Drawing.Color.White;
            this.lbl_Tilte.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lbl_Tilte.Location = new System.Drawing.Point(7, 7);
            this.lbl_Tilte.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.lbl_Tilte.Name = "lbl_Tilte";
            this.lbl_Tilte.Size = new System.Drawing.Size(222, 35);
            this.lbl_Tilte.TabIndex = 5;
            this.lbl_Tilte.Text = "Database Viewer";
            this.lbl_Tilte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl_Center
            // 
            this.pnl_Center.BackColor = System.Drawing.Color.LightGray;
            this.tlp_Main.SetColumnSpan(this.pnl_Center, 2);
            this.pnl_Center.Controls.Add(this.gb_Info);
            this.pnl_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Center.Location = new System.Drawing.Point(0, 53);
            this.pnl_Center.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_Center.Name = "pnl_Center";
            this.pnl_Center.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_Center.Size = new System.Drawing.Size(1008, 77);
            this.pnl_Center.TabIndex = 7;
            // 
            // gb_Info
            // 
            this.gb_Info.BackColor = System.Drawing.Color.LightGray;
            this.gb_Info.Controls.Add(this.lbl_MessageWaitCount);
            this.gb_Info.Controls.Add(this.label3);
            this.gb_Info.Controls.Add(this.label1);
            this.gb_Info.Controls.Add(this.lbl_RunMode);
            this.gb_Info.Controls.Add(this.lblState);
            this.gb_Info.Controls.Add(this.lbl_cap_ip);
            this.gb_Info.Controls.Add(this.lblSystem);
            this.gb_Info.Controls.Add(this.lblUid);
            this.gb_Info.Controls.Add(this.lblDbName);
            this.gb_Info.Controls.Add(this.label10);
            this.gb_Info.Controls.Add(this.lbl_cap_state);
            this.gb_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_Info.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gb_Info.Location = new System.Drawing.Point(3, 3);
            this.gb_Info.Name = "gb_Info";
            this.gb_Info.Size = new System.Drawing.Size(1002, 71);
            this.gb_Info.TabIndex = 29;
            this.gb_Info.TabStop = false;
            this.gb_Info.Text = "연결 정보";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(17, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Mode :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_RunMode
            // 
            this.lbl_RunMode.BackColor = System.Drawing.Color.Transparent;
            this.lbl_RunMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_RunMode.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_RunMode.ForeColor = System.Drawing.Color.Black;
            this.lbl_RunMode.Location = new System.Drawing.Point(73, 33);
            this.lbl_RunMode.Name = "lbl_RunMode";
            this.lbl_RunMode.Size = new System.Drawing.Size(100, 15);
            this.lbl_RunMode.TabIndex = 23;
            this.lbl_RunMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_cap_ip
            // 
            this.lbl_cap_ip.AutoSize = true;
            this.lbl_cap_ip.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cap_ip.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_cap_ip.ForeColor = System.Drawing.Color.Black;
            this.lbl_cap_ip.Location = new System.Drawing.Point(186, 33);
            this.lbl_cap_ip.Name = "lbl_cap_ip";
            this.lbl_cap_ip.Size = new System.Drawing.Size(76, 13);
            this.lbl_cap_ip.TabIndex = 16;
            this.lbl_cap_ip.Text = "DataBase :";
            this.lbl_cap_ip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSystem
            // 
            this.lblSystem.BackColor = System.Drawing.Color.Transparent;
            this.lblSystem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSystem.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSystem.ForeColor = System.Drawing.Color.Black;
            this.lblSystem.Location = new System.Drawing.Point(504, 32);
            this.lblSystem.Name = "lblSystem";
            this.lblSystem.Size = new System.Drawing.Size(159, 15);
            this.lblSystem.TabIndex = 19;
            this.lblSystem.Text = "_________";
            this.lblSystem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUid
            // 
            this.lblUid.BackColor = System.Drawing.Color.Transparent;
            this.lblUid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUid.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblUid.ForeColor = System.Drawing.Color.Black;
            this.lblUid.Location = new System.Drawing.Point(713, 32);
            this.lblUid.Name = "lblUid";
            this.lblUid.Size = new System.Drawing.Size(159, 15);
            this.lblUid.TabIndex = 19;
            this.lblUid.Text = "_____";
            this.lblUid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDbName
            // 
            this.lblDbName.BackColor = System.Drawing.Color.Transparent;
            this.lblDbName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDbName.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDbName.ForeColor = System.Drawing.Color.Black;
            this.lblDbName.Location = new System.Drawing.Point(270, 32);
            this.lblDbName.Name = "lblDbName";
            this.lblDbName.Size = new System.Drawing.Size(159, 15);
            this.lblDbName.TabIndex = 19;
            this.lblDbName.Text = "_____";
            this.lblDbName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(435, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "System :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_cap_state
            // 
            this.lbl_cap_state.AutoSize = true;
            this.lbl_cap_state.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cap_state.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_cap_state.ForeColor = System.Drawing.Color.Black;
            this.lbl_cap_state.Location = new System.Drawing.Point(669, 34);
            this.lbl_cap_state.Name = "lbl_cap_state";
            this.lbl_cap_state.Size = new System.Drawing.Size(40, 13);
            this.lbl_cap_state.TabIndex = 18;
            this.lbl_cap_state.Text = "UID : ";
            this.lbl_cap_state.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_MessageWaitCount
            // 
            this.lbl_MessageWaitCount.BackColor = System.Drawing.Color.Transparent;
            this.lbl_MessageWaitCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_MessageWaitCount.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_MessageWaitCount.ForeColor = System.Drawing.Color.Black;
            this.lbl_MessageWaitCount.Location = new System.Drawing.Point(713, 51);
            this.lbl_MessageWaitCount.Name = "lbl_MessageWaitCount";
            this.lbl_MessageWaitCount.Size = new System.Drawing.Size(159, 15);
            this.lbl_MessageWaitCount.TabIndex = 25;
            this.lbl_MessageWaitCount.Text = "_____";
            this.lbl_MessageWaitCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(569, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "MessageWaitCount : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblState
            // 
            this.lblState.BorderColor = System.Drawing.Color.Black;
            this.lblState.BorderVisible = true;
            this.lblState.ExBorderStyle = SPApi.Controls.UI.CustomBorderStyle.None;
            this.lblState.GradientEnabled = true;
            this.lblState.GradientEndColor = System.Drawing.Color.Red;
            this.lblState.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.lblState.GradientStartColor = System.Drawing.Color.White;
            this.lblState.ImageEnabled = false;
            this.lblState.ImageLocation = new System.Drawing.Point(0, 0);
            this.lblState.Location = new System.Drawing.Point(895, 27);
            this.lblState.Name = "lblState";
            this.lblState.Picture = null;
            this.lblState.Size = new System.Drawing.Size(94, 24);
            this.lblState.TabIndex = 21;
            this.lblState.Text = "CLOSE";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DatabaseView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.ControlBox = false;
            this.Controls.Add(this.tlp_Main);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "DatabaseView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Viewer";
            this.tlp_Main.ResumeLayout(false);
            this.pn_Top.ResumeLayout(false);
            this.pnl_Center.ResumeLayout(false);
            this.gb_Info.ResumeLayout(false);
            this.gb_Info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imgTrans;
        private System.Windows.Forms.ColumnHeader cTime;
        private System.Windows.Forms.ColumnHeader cIcon;
        private System.Windows.Forms.ListView lstLog;
        private System.Windows.Forms.ColumnHeader cQuery;
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.Label lbl_bar;
        private System.Windows.Forms.Panel pn_Top;
        private System.Windows.Forms.Button btn_WinState;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Label lbl_Tilte;
        private System.Windows.Forms.Panel pnl_Center;
        private System.Windows.Forms.GroupBox gb_Info;
        private UI.SLabel lblState;
        private System.Windows.Forms.Label lbl_cap_ip;
        private System.Windows.Forms.Label lblSystem;
        private System.Windows.Forms.Label lblUid;
        private System.Windows.Forms.Label lblDbName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_cap_state;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_RunMode;
        private System.Windows.Forms.Label lbl_MessageWaitCount;
        private System.Windows.Forms.Label label3;
    }
}