namespace SPApi.Controls.Dio
{
    partial class E1212View
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(E1212View));
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.lstLog = new System.Windows.Forms.ListView();
            this.cIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgTrans = new System.Windows.Forms.ImageList(this.components);
            this.lbl_bar = new System.Windows.Forms.Label();
            this.pn_Top = new System.Windows.Forms.Panel();
            this.lbl_MessageWaitCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_WinState = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.lbl_Tilte = new System.Windows.Forms.Label();
            this.pnl_Center = new System.Windows.Forms.Panel();
            this.gb_DioState = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_DO7 = new System.Windows.Forms.Button();
            this.btn_DO6 = new System.Windows.Forms.Button();
            this.btn_DO5 = new System.Windows.Forms.Button();
            this.btn_DO4 = new System.Windows.Forms.Button();
            this.btn_DO3 = new System.Windows.Forms.Button();
            this.btn_DO2 = new System.Windows.Forms.Button();
            this.btn_DO1 = new System.Windows.Forms.Button();
            this.btn_DO0 = new System.Windows.Forms.Button();
            this.btn_DI7 = new System.Windows.Forms.Button();
            this.btn_DI6 = new System.Windows.Forms.Button();
            this.btn_DI5 = new System.Windows.Forms.Button();
            this.btn_DI4 = new System.Windows.Forms.Button();
            this.btn_DI3 = new System.Windows.Forms.Button();
            this.btn_DI2 = new System.Windows.Forms.Button();
            this.btn_DI1 = new System.Windows.Forms.Button();
            this.lbl_cap_do = new System.Windows.Forms.Label();
            this.lbl_cap_di = new System.Windows.Forms.Label();
            this.btn_DI0 = new System.Windows.Forms.Button();
            this.lbl_cap_no = new System.Windows.Forms.Label();
            this.gb_Info = new System.Windows.Forms.GroupBox();
            this.lbl_State = new System.Windows.Forms.Label();
            this.lbl_Port = new System.Windows.Forms.Label();
            this.lbl_IP = new System.Windows.Forms.Label();
            this.lbl_cap_state = new System.Windows.Forms.Label();
            this.lbl_cap_port = new System.Windows.Forms.Label();
            this.lbl_cap_ip = new System.Windows.Forms.Label();
            this.pnl_Bottom = new System.Windows.Forms.Panel();
            this.lbl_cap_log = new System.Windows.Forms.Label();
            this.lbl_SelLog = new System.Windows.Forms.Label();
            this.tlp_Main.SuspendLayout();
            this.pn_Top.SuspendLayout();
            this.pnl_Center.SuspendLayout();
            this.gb_DioState.SuspendLayout();
            this.gb_Info.SuspendLayout();
            this.pnl_Bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.lstLog, 0, 4);
            this.tlp_Main.Controls.Add(this.lbl_bar, 0, 1);
            this.tlp_Main.Controls.Add(this.pn_Top, 0, 0);
            this.tlp_Main.Controls.Add(this.pnl_Center, 0, 2);
            this.tlp_Main.Controls.Add(this.pnl_Bottom, 0, 3);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 5;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(689, 524);
            this.tlp_Main.TabIndex = 3;
            // 
            // lstLog
            // 
            this.lstLog.BackColor = System.Drawing.SystemColors.Window;
            this.lstLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cIcon,
            this.cTime,
            this.cType,
            this.cData});
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.lstLog.FullRowSelect = true;
            this.lstLog.GridLines = true;
            this.lstLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstLog.HideSelection = false;
            this.lstLog.Location = new System.Drawing.Point(0, 243);
            this.lstLog.Margin = new System.Windows.Forms.Padding(0);
            this.lstLog.MultiSelect = false;
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(689, 281);
            this.lstLog.SmallImageList = this.imgTrans;
            this.lstLog.StateImageList = this.imgTrans;
            this.lstLog.TabIndex = 11;
            this.lstLog.UseCompatibleStateImageBehavior = false;
            this.lstLog.View = System.Windows.Forms.View.Details;
            // 
            // cIcon
            // 
            this.cIcon.Text = "";
            this.cIcon.Width = 51;
            // 
            // cTime
            // 
            this.cTime.Text = "Time";
            this.cTime.Width = 197;
            // 
            // cType
            // 
            this.cType.Text = "Type";
            this.cType.Width = 86;
            // 
            // cData
            // 
            this.cData.Text = "Data";
            this.cData.Width = 348;
            // 
            // imgTrans
            // 
            this.imgTrans.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTrans.ImageStream")));
            this.imgTrans.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTrans.Images.SetKeyName(0, "left_green.png");
            this.imgTrans.Images.SetKeyName(1, "left_red.png");
            this.imgTrans.Images.SetKeyName(2, "right_green.png");
            this.imgTrans.Images.SetKeyName(3, "right_red.png");
            // 
            // lbl_bar
            // 
            this.lbl_bar.BackColor = System.Drawing.Color.DarkBlue;
            this.lbl_bar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_bar.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_bar.Location = new System.Drawing.Point(1, 50);
            this.lbl_bar.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lbl_bar.Name = "lbl_bar";
            this.lbl_bar.Size = new System.Drawing.Size(687, 3);
            this.lbl_bar.TabIndex = 8;
            this.lbl_bar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pn_Top
            // 
            this.pn_Top.BackColor = System.Drawing.Color.White;
            this.pn_Top.Controls.Add(this.lbl_MessageWaitCount);
            this.pn_Top.Controls.Add(this.label3);
            this.pn_Top.Controls.Add(this.btn_WinState);
            this.pn_Top.Controls.Add(this.btnHide);
            this.pn_Top.Controls.Add(this.lbl_Tilte);
            this.pn_Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_Top.Location = new System.Drawing.Point(1, 1);
            this.pn_Top.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.pn_Top.Name = "pn_Top";
            this.pn_Top.Size = new System.Drawing.Size(687, 49);
            this.pn_Top.TabIndex = 7;
            this.pn_Top.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Tilte_MouseMove);
            // 
            // lbl_MessageWaitCount
            // 
            this.lbl_MessageWaitCount.BackColor = System.Drawing.Color.Transparent;
            this.lbl_MessageWaitCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_MessageWaitCount.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_MessageWaitCount.ForeColor = System.Drawing.Color.Black;
            this.lbl_MessageWaitCount.Location = new System.Drawing.Point(443, 27);
            this.lbl_MessageWaitCount.Name = "lbl_MessageWaitCount";
            this.lbl_MessageWaitCount.Size = new System.Drawing.Size(159, 15);
            this.lbl_MessageWaitCount.TabIndex = 29;
            this.lbl_MessageWaitCount.Text = "_____";
            this.lbl_MessageWaitCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(299, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "MessageWaitCount : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_WinState
            // 
            this.btn_WinState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_WinState.BackColor = System.Drawing.SystemColors.Control;
            this.btn_WinState.BackgroundImage = global::SPApi.Properties.Resources.btn_FullScreen;
            this.btn_WinState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_WinState.Location = new System.Drawing.Point(608, 9);
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
            this.btnHide.Location = new System.Drawing.Point(643, 9);
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
            this.lbl_Tilte.Size = new System.Drawing.Size(285, 35);
            this.lbl_Tilte.TabIndex = 5;
            this.lbl_Tilte.Text = "ioLogik E1212 Viewer";
            this.lbl_Tilte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Tilte.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Tilte_MouseMove);
            // 
            // pnl_Center
            // 
            this.pnl_Center.BackColor = System.Drawing.Color.LightGray;
            this.pnl_Center.Controls.Add(this.gb_DioState);
            this.pnl_Center.Controls.Add(this.gb_Info);
            this.pnl_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Center.Location = new System.Drawing.Point(0, 53);
            this.pnl_Center.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_Center.Name = "pnl_Center";
            this.pnl_Center.Size = new System.Drawing.Size(689, 160);
            this.pnl_Center.TabIndex = 0;
            // 
            // gb_DioState
            // 
            this.gb_DioState.BackColor = System.Drawing.Color.LightGray;
            this.gb_DioState.Controls.Add(this.label5);
            this.gb_DioState.Controls.Add(this.label4);
            this.gb_DioState.Controls.Add(this.btn_DO7);
            this.gb_DioState.Controls.Add(this.btn_DO6);
            this.gb_DioState.Controls.Add(this.btn_DO5);
            this.gb_DioState.Controls.Add(this.btn_DO4);
            this.gb_DioState.Controls.Add(this.btn_DO3);
            this.gb_DioState.Controls.Add(this.btn_DO2);
            this.gb_DioState.Controls.Add(this.btn_DO1);
            this.gb_DioState.Controls.Add(this.btn_DO0);
            this.gb_DioState.Controls.Add(this.btn_DI7);
            this.gb_DioState.Controls.Add(this.btn_DI6);
            this.gb_DioState.Controls.Add(this.btn_DI5);
            this.gb_DioState.Controls.Add(this.btn_DI4);
            this.gb_DioState.Controls.Add(this.btn_DI3);
            this.gb_DioState.Controls.Add(this.btn_DI2);
            this.gb_DioState.Controls.Add(this.btn_DI1);
            this.gb_DioState.Controls.Add(this.lbl_cap_do);
            this.gb_DioState.Controls.Add(this.lbl_cap_di);
            this.gb_DioState.Controls.Add(this.btn_DI0);
            this.gb_DioState.Controls.Add(this.lbl_cap_no);
            this.gb_DioState.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gb_DioState.Location = new System.Drawing.Point(231, 12);
            this.gb_DioState.Name = "gb_DioState";
            this.gb_DioState.Size = new System.Drawing.Size(446, 137);
            this.gb_DioState.TabIndex = 28;
            this.gb_DioState.TabStop = false;
            this.gb_DioState.Text = "DIO 상태";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(14, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(416, 2);
            this.label5.TabIndex = 68;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(14, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(416, 2);
            this.label4.TabIndex = 67;
            // 
            // btn_DO7
            // 
            this.btn_DO7.BackColor = System.Drawing.Color.Red;
            this.btn_DO7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DO7.Location = new System.Drawing.Point(394, 87);
            this.btn_DO7.Name = "btn_DO7";
            this.btn_DO7.Size = new System.Drawing.Size(33, 33);
            this.btn_DO7.TabIndex = 66;
            this.btn_DO7.UseVisualStyleBackColor = false;
            this.btn_DO7.Click += new System.EventHandler(this.DO_Contacts_Click);
            // 
            // btn_DO6
            // 
            this.btn_DO6.BackColor = System.Drawing.Color.Red;
            this.btn_DO6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DO6.Location = new System.Drawing.Point(346, 87);
            this.btn_DO6.Name = "btn_DO6";
            this.btn_DO6.Size = new System.Drawing.Size(33, 33);
            this.btn_DO6.TabIndex = 65;
            this.btn_DO6.UseVisualStyleBackColor = false;
            this.btn_DO6.Click += new System.EventHandler(this.DO_Contacts_Click);
            // 
            // btn_DO5
            // 
            this.btn_DO5.BackColor = System.Drawing.Color.Red;
            this.btn_DO5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DO5.Location = new System.Drawing.Point(298, 87);
            this.btn_DO5.Name = "btn_DO5";
            this.btn_DO5.Size = new System.Drawing.Size(33, 33);
            this.btn_DO5.TabIndex = 64;
            this.btn_DO5.UseVisualStyleBackColor = false;
            this.btn_DO5.Click += new System.EventHandler(this.DO_Contacts_Click);
            // 
            // btn_DO4
            // 
            this.btn_DO4.BackColor = System.Drawing.Color.Red;
            this.btn_DO4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DO4.Location = new System.Drawing.Point(250, 87);
            this.btn_DO4.Name = "btn_DO4";
            this.btn_DO4.Size = new System.Drawing.Size(33, 33);
            this.btn_DO4.TabIndex = 63;
            this.btn_DO4.UseVisualStyleBackColor = false;
            this.btn_DO4.Click += new System.EventHandler(this.DO_Contacts_Click);
            // 
            // btn_DO3
            // 
            this.btn_DO3.BackColor = System.Drawing.Color.Red;
            this.btn_DO3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DO3.Location = new System.Drawing.Point(202, 87);
            this.btn_DO3.Name = "btn_DO3";
            this.btn_DO3.Size = new System.Drawing.Size(33, 33);
            this.btn_DO3.TabIndex = 62;
            this.btn_DO3.UseVisualStyleBackColor = false;
            this.btn_DO3.Click += new System.EventHandler(this.DO_Contacts_Click);
            // 
            // btn_DO2
            // 
            this.btn_DO2.BackColor = System.Drawing.Color.Red;
            this.btn_DO2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DO2.Location = new System.Drawing.Point(154, 87);
            this.btn_DO2.Name = "btn_DO2";
            this.btn_DO2.Size = new System.Drawing.Size(33, 33);
            this.btn_DO2.TabIndex = 61;
            this.btn_DO2.UseVisualStyleBackColor = false;
            this.btn_DO2.Click += new System.EventHandler(this.DO_Contacts_Click);
            // 
            // btn_DO1
            // 
            this.btn_DO1.BackColor = System.Drawing.Color.Red;
            this.btn_DO1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DO1.Location = new System.Drawing.Point(106, 87);
            this.btn_DO1.Name = "btn_DO1";
            this.btn_DO1.Size = new System.Drawing.Size(33, 33);
            this.btn_DO1.TabIndex = 60;
            this.btn_DO1.UseVisualStyleBackColor = false;
            this.btn_DO1.Click += new System.EventHandler(this.DO_Contacts_Click);
            // 
            // btn_DO0
            // 
            this.btn_DO0.BackColor = System.Drawing.Color.Red;
            this.btn_DO0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DO0.Location = new System.Drawing.Point(57, 87);
            this.btn_DO0.Name = "btn_DO0";
            this.btn_DO0.Size = new System.Drawing.Size(33, 33);
            this.btn_DO0.TabIndex = 59;
            this.btn_DO0.UseVisualStyleBackColor = false;
            this.btn_DO0.Click += new System.EventHandler(this.DO_Contacts_Click);
            // 
            // btn_DI7
            // 
            this.btn_DI7.BackColor = System.Drawing.Color.Red;
            this.btn_DI7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DI7.Enabled = false;
            this.btn_DI7.Location = new System.Drawing.Point(394, 45);
            this.btn_DI7.Name = "btn_DI7";
            this.btn_DI7.Size = new System.Drawing.Size(33, 33);
            this.btn_DI7.TabIndex = 58;
            this.btn_DI7.UseVisualStyleBackColor = false;
            // 
            // btn_DI6
            // 
            this.btn_DI6.BackColor = System.Drawing.Color.Red;
            this.btn_DI6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DI6.Enabled = false;
            this.btn_DI6.Location = new System.Drawing.Point(346, 45);
            this.btn_DI6.Name = "btn_DI6";
            this.btn_DI6.Size = new System.Drawing.Size(33, 33);
            this.btn_DI6.TabIndex = 57;
            this.btn_DI6.UseVisualStyleBackColor = false;
            // 
            // btn_DI5
            // 
            this.btn_DI5.BackColor = System.Drawing.Color.Red;
            this.btn_DI5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DI5.Enabled = false;
            this.btn_DI5.Location = new System.Drawing.Point(298, 45);
            this.btn_DI5.Name = "btn_DI5";
            this.btn_DI5.Size = new System.Drawing.Size(33, 33);
            this.btn_DI5.TabIndex = 56;
            this.btn_DI5.UseVisualStyleBackColor = false;
            // 
            // btn_DI4
            // 
            this.btn_DI4.BackColor = System.Drawing.Color.Red;
            this.btn_DI4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DI4.Enabled = false;
            this.btn_DI4.Location = new System.Drawing.Point(250, 45);
            this.btn_DI4.Name = "btn_DI4";
            this.btn_DI4.Size = new System.Drawing.Size(33, 33);
            this.btn_DI4.TabIndex = 55;
            this.btn_DI4.UseVisualStyleBackColor = false;
            // 
            // btn_DI3
            // 
            this.btn_DI3.BackColor = System.Drawing.Color.Red;
            this.btn_DI3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DI3.Enabled = false;
            this.btn_DI3.Location = new System.Drawing.Point(202, 45);
            this.btn_DI3.Name = "btn_DI3";
            this.btn_DI3.Size = new System.Drawing.Size(33, 33);
            this.btn_DI3.TabIndex = 54;
            this.btn_DI3.UseVisualStyleBackColor = false;
            // 
            // btn_DI2
            // 
            this.btn_DI2.BackColor = System.Drawing.Color.Red;
            this.btn_DI2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DI2.Enabled = false;
            this.btn_DI2.Location = new System.Drawing.Point(154, 45);
            this.btn_DI2.Name = "btn_DI2";
            this.btn_DI2.Size = new System.Drawing.Size(33, 33);
            this.btn_DI2.TabIndex = 53;
            this.btn_DI2.UseVisualStyleBackColor = false;
            // 
            // btn_DI1
            // 
            this.btn_DI1.BackColor = System.Drawing.Color.Red;
            this.btn_DI1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DI1.Enabled = false;
            this.btn_DI1.Location = new System.Drawing.Point(106, 45);
            this.btn_DI1.Name = "btn_DI1";
            this.btn_DI1.Size = new System.Drawing.Size(33, 33);
            this.btn_DI1.TabIndex = 52;
            this.btn_DI1.UseVisualStyleBackColor = false;
            // 
            // lbl_cap_do
            // 
            this.lbl_cap_do.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cap_do.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_cap_do.ForeColor = System.Drawing.Color.Black;
            this.lbl_cap_do.Location = new System.Drawing.Point(16, 95);
            this.lbl_cap_do.Name = "lbl_cap_do";
            this.lbl_cap_do.Size = new System.Drawing.Size(35, 18);
            this.lbl_cap_do.TabIndex = 51;
            this.lbl_cap_do.Text = "DO";
            // 
            // lbl_cap_di
            // 
            this.lbl_cap_di.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cap_di.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_cap_di.ForeColor = System.Drawing.Color.Black;
            this.lbl_cap_di.Location = new System.Drawing.Point(16, 52);
            this.lbl_cap_di.Name = "lbl_cap_di";
            this.lbl_cap_di.Size = new System.Drawing.Size(35, 18);
            this.lbl_cap_di.TabIndex = 50;
            this.lbl_cap_di.Text = "DI";
            // 
            // btn_DI0
            // 
            this.btn_DI0.BackColor = System.Drawing.Color.Red;
            this.btn_DI0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DI0.Enabled = false;
            this.btn_DI0.Location = new System.Drawing.Point(57, 45);
            this.btn_DI0.Name = "btn_DI0";
            this.btn_DI0.Size = new System.Drawing.Size(33, 33);
            this.btn_DI0.TabIndex = 49;
            this.btn_DI0.UseVisualStyleBackColor = false;
            // 
            // lbl_cap_no
            // 
            this.lbl_cap_no.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cap_no.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_cap_no.Location = new System.Drawing.Point(67, 23);
            this.lbl_cap_no.Name = "lbl_cap_no";
            this.lbl_cap_no.Size = new System.Drawing.Size(369, 15);
            this.lbl_cap_no.TabIndex = 48;
            this.lbl_cap_no.Text = "0        1        2        3        4        5        6        7";
            // 
            // gb_Info
            // 
            this.gb_Info.BackColor = System.Drawing.Color.LightGray;
            this.gb_Info.Controls.Add(this.lbl_State);
            this.gb_Info.Controls.Add(this.lbl_Port);
            this.gb_Info.Controls.Add(this.lbl_IP);
            this.gb_Info.Controls.Add(this.lbl_cap_state);
            this.gb_Info.Controls.Add(this.lbl_cap_port);
            this.gb_Info.Controls.Add(this.lbl_cap_ip);
            this.gb_Info.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gb_Info.Location = new System.Drawing.Point(11, 12);
            this.gb_Info.Name = "gb_Info";
            this.gb_Info.Size = new System.Drawing.Size(211, 137);
            this.gb_Info.TabIndex = 27;
            this.gb_Info.TabStop = false;
            this.gb_Info.Text = "연결 정보";
            // 
            // lbl_State
            // 
            this.lbl_State.BackColor = System.Drawing.Color.Transparent;
            this.lbl_State.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_State.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_State.ForeColor = System.Drawing.Color.Black;
            this.lbl_State.Location = new System.Drawing.Point(65, 100);
            this.lbl_State.Name = "lbl_State";
            this.lbl_State.Size = new System.Drawing.Size(109, 19);
            this.lbl_State.TabIndex = 21;
            this.lbl_State.Text = "__________";
            this.lbl_State.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Port
            // 
            this.lbl_Port.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Port.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Port.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Port.ForeColor = System.Drawing.Color.Black;
            this.lbl_Port.Location = new System.Drawing.Point(65, 64);
            this.lbl_Port.Name = "lbl_Port";
            this.lbl_Port.Size = new System.Drawing.Size(77, 19);
            this.lbl_Port.TabIndex = 20;
            this.lbl_Port.Text = "_____";
            this.lbl_Port.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_IP
            // 
            this.lbl_IP.BackColor = System.Drawing.Color.Transparent;
            this.lbl_IP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_IP.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IP.ForeColor = System.Drawing.Color.Black;
            this.lbl_IP.Location = new System.Drawing.Point(65, 28);
            this.lbl_IP.Name = "lbl_IP";
            this.lbl_IP.Size = new System.Drawing.Size(136, 19);
            this.lbl_IP.TabIndex = 19;
            this.lbl_IP.Text = "___.___.___.___";
            this.lbl_IP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_cap_state
            // 
            this.lbl_cap_state.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cap_state.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_cap_state.ForeColor = System.Drawing.Color.Black;
            this.lbl_cap_state.Location = new System.Drawing.Point(19, 102);
            this.lbl_cap_state.Name = "lbl_cap_state";
            this.lbl_cap_state.Size = new System.Drawing.Size(47, 15);
            this.lbl_cap_state.TabIndex = 18;
            this.lbl_cap_state.Text = "상태 : ";
            this.lbl_cap_state.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_cap_port
            // 
            this.lbl_cap_port.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cap_port.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_cap_port.ForeColor = System.Drawing.Color.Black;
            this.lbl_cap_port.Location = new System.Drawing.Point(19, 66);
            this.lbl_cap_port.Name = "lbl_cap_port";
            this.lbl_cap_port.Size = new System.Drawing.Size(41, 15);
            this.lbl_cap_port.TabIndex = 17;
            this.lbl_cap_port.Text = "포트 :";
            this.lbl_cap_port.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_cap_ip
            // 
            this.lbl_cap_ip.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cap_ip.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_cap_ip.ForeColor = System.Drawing.Color.Black;
            this.lbl_cap_ip.Location = new System.Drawing.Point(19, 30);
            this.lbl_cap_ip.Name = "lbl_cap_ip";
            this.lbl_cap_ip.Size = new System.Drawing.Size(43, 15);
            this.lbl_cap_ip.TabIndex = 16;
            this.lbl_cap_ip.Text = "IP    :";
            this.lbl_cap_ip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl_Bottom
            // 
            this.pnl_Bottom.BackColor = System.Drawing.Color.LightGray;
            this.pnl_Bottom.Controls.Add(this.lbl_cap_log);
            this.pnl_Bottom.Controls.Add(this.lbl_SelLog);
            this.pnl_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Bottom.Location = new System.Drawing.Point(0, 213);
            this.pnl_Bottom.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_Bottom.Name = "pnl_Bottom";
            this.pnl_Bottom.Padding = new System.Windows.Forms.Padding(1);
            this.pnl_Bottom.Size = new System.Drawing.Size(689, 30);
            this.pnl_Bottom.TabIndex = 10;
            // 
            // lbl_cap_log
            // 
            this.lbl_cap_log.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_cap_log.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_cap_log.ForeColor = System.Drawing.Color.Black;
            this.lbl_cap_log.Location = new System.Drawing.Point(1, 1);
            this.lbl_cap_log.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_cap_log.Name = "lbl_cap_log";
            this.lbl_cap_log.Size = new System.Drawing.Size(77, 28);
            this.lbl_cap_log.TabIndex = 21;
            this.lbl_cap_log.Text = "로그정보";
            this.lbl_cap_log.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_SelLog
            // 
            this.lbl_SelLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_SelLog.BackColor = System.Drawing.Color.Black;
            this.lbl_SelLog.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_SelLog.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_SelLog.ForeColor = System.Drawing.Color.White;
            this.lbl_SelLog.Location = new System.Drawing.Point(79, 1);
            this.lbl_SelLog.Name = "lbl_SelLog";
            this.lbl_SelLog.Size = new System.Drawing.Size(609, 28);
            this.lbl_SelLog.TabIndex = 20;
            this.lbl_SelLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // E1212View
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(689, 524);
            this.ControlBox = false;
            this.Controls.Add(this.tlp_Main);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "E1212View";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DioDetailView";
            this.tlp_Main.ResumeLayout(false);
            this.pn_Top.ResumeLayout(false);
            this.pn_Top.PerformLayout();
            this.pnl_Center.ResumeLayout(false);
            this.gb_DioState.ResumeLayout(false);
            this.gb_Info.ResumeLayout(false);
            this.pnl_Bottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Center;
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.Panel pn_Top;
        private System.Windows.Forms.Button btn_WinState;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Label lbl_Tilte;
        private System.Windows.Forms.Label lbl_bar;
        private System.Windows.Forms.GroupBox gb_DioState;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_DO7;
        private System.Windows.Forms.Button btn_DO6;
        private System.Windows.Forms.Button btn_DO5;
        private System.Windows.Forms.Button btn_DO4;
        private System.Windows.Forms.Button btn_DO3;
        private System.Windows.Forms.Button btn_DO2;
        private System.Windows.Forms.Button btn_DO1;
        private System.Windows.Forms.Button btn_DO0;
        private System.Windows.Forms.Button btn_DI7;
        private System.Windows.Forms.Button btn_DI6;
        private System.Windows.Forms.Button btn_DI5;
        private System.Windows.Forms.Button btn_DI4;
        private System.Windows.Forms.Button btn_DI3;
        private System.Windows.Forms.Button btn_DI2;
        private System.Windows.Forms.Button btn_DI1;
        private System.Windows.Forms.Label lbl_cap_do;
        private System.Windows.Forms.Label lbl_cap_di;
        private System.Windows.Forms.Button btn_DI0;
        private System.Windows.Forms.Label lbl_cap_no;
        private System.Windows.Forms.GroupBox gb_Info;
        private System.Windows.Forms.Label lbl_State;
        private System.Windows.Forms.Label lbl_Port;
        private System.Windows.Forms.Label lbl_IP;
        private System.Windows.Forms.Label lbl_cap_state;
        private System.Windows.Forms.Label lbl_cap_port;
        private System.Windows.Forms.Label lbl_cap_ip;
        private System.Windows.Forms.Panel pnl_Bottom;
        private System.Windows.Forms.Label lbl_cap_log;
        private System.Windows.Forms.Label lbl_SelLog;
        private System.Windows.Forms.ListView lstLog;
        private System.Windows.Forms.ColumnHeader cIcon;
        private System.Windows.Forms.ColumnHeader cTime;
        private System.Windows.Forms.ColumnHeader cData;
        private System.Windows.Forms.ImageList imgTrans;
        private System.Windows.Forms.ColumnHeader cType;
        private System.Windows.Forms.Label lbl_MessageWaitCount;
        private System.Windows.Forms.Label label3;
    }
}