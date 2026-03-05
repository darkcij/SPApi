namespace SPApi.Controls.Serials
{
    partial class SerialView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SerialView));
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_bar = new System.Windows.Forms.Label();
            this.pn_Top = new System.Windows.Forms.Panel();
            this.btn_WinState = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.lbl_Tilte = new System.Windows.Forms.Label();
            this.pnl_Center = new System.Windows.Forms.Panel();
            this.gb_Info = new System.Windows.Forms.GroupBox();
            this.lblState = new SPApi.Controls.UI.SLabel();
            this.lbl_cap_port = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_cap_ip = new System.Windows.Forms.Label();
            this.lblParitybit = new System.Windows.Forms.Label();
            this.lblDatabit = new System.Windows.Forms.Label();
            this.lblBaudrate = new System.Windows.Forms.Label();
            this.lblStopbit = new System.Windows.Forms.Label();
            this.lblWriteTimeOut = new System.Windows.Forms.Label();
            this.lblReadTimeOut = new System.Windows.Forms.Label();
            this.lblDelimiter = new System.Windows.Forms.Label();
            this.lblPortname = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_cap_state = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListView();
            this.cIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgTrans = new System.Windows.Forms.ImageList(this.components);
            this.lbl_MessageWaitCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tlp_Main.SuspendLayout();
            this.pn_Top.SuspendLayout();
            this.pnl_Center.SuspendLayout();
            this.gb_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.lbl_bar, 0, 1);
            this.tlp_Main.Controls.Add(this.pn_Top, 0, 0);
            this.tlp_Main.Controls.Add(this.pnl_Center, 0, 2);
            this.tlp_Main.Controls.Add(this.lstLog, 0, 3);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 4;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_Main.Size = new System.Drawing.Size(697, 523);
            this.tlp_Main.TabIndex = 1;
            // 
            // lbl_bar
            // 
            this.lbl_bar.BackColor = System.Drawing.Color.DarkBlue;
            this.lbl_bar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_bar.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_bar.Location = new System.Drawing.Point(1, 50);
            this.lbl_bar.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lbl_bar.Name = "lbl_bar";
            this.lbl_bar.Size = new System.Drawing.Size(695, 3);
            this.lbl_bar.TabIndex = 5;
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
            this.pn_Top.Size = new System.Drawing.Size(695, 49);
            this.pn_Top.TabIndex = 6;
            this.pn_Top.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Top_MouseMove);
            // 
            // btn_WinState
            // 
            this.btn_WinState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_WinState.BackColor = System.Drawing.SystemColors.Control;
            this.btn_WinState.BackgroundImage = global::SPApi.Properties.Resources.btn_FullScreen;
            this.btn_WinState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_WinState.Location = new System.Drawing.Point(616, 9);
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
            this.btnHide.Location = new System.Drawing.Point(651, 9);
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
            this.lbl_Tilte.Size = new System.Drawing.Size(195, 35);
            this.lbl_Tilte.TabIndex = 5;
            this.lbl_Tilte.Text = "Serial Viewer";
            this.lbl_Tilte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Tilte.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Top_MouseMove);
            // 
            // pnl_Center
            // 
            this.pnl_Center.BackColor = System.Drawing.Color.LightGray;
            this.pnl_Center.Controls.Add(this.gb_Info);
            this.pnl_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Center.Location = new System.Drawing.Point(0, 53);
            this.pnl_Center.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_Center.Name = "pnl_Center";
            this.pnl_Center.Size = new System.Drawing.Size(697, 112);
            this.pnl_Center.TabIndex = 7;
            // 
            // gb_Info
            // 
            this.gb_Info.BackColor = System.Drawing.Color.LightGray;
            this.gb_Info.Controls.Add(this.lblState);
            this.gb_Info.Controls.Add(this.lbl_cap_port);
            this.gb_Info.Controls.Add(this.label15);
            this.gb_Info.Controls.Add(this.label13);
            this.gb_Info.Controls.Add(this.label9);
            this.gb_Info.Controls.Add(this.lbl_cap_ip);
            this.gb_Info.Controls.Add(this.lblParitybit);
            this.gb_Info.Controls.Add(this.lblDatabit);
            this.gb_Info.Controls.Add(this.lblBaudrate);
            this.gb_Info.Controls.Add(this.lblStopbit);
            this.gb_Info.Controls.Add(this.lblWriteTimeOut);
            this.gb_Info.Controls.Add(this.lblReadTimeOut);
            this.gb_Info.Controls.Add(this.lblDelimiter);
            this.gb_Info.Controls.Add(this.lblPortname);
            this.gb_Info.Controls.Add(this.label11);
            this.gb_Info.Controls.Add(this.label10);
            this.gb_Info.Controls.Add(this.lbl_cap_state);
            this.gb_Info.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gb_Info.Location = new System.Drawing.Point(4, 7);
            this.gb_Info.Name = "gb_Info";
            this.gb_Info.Size = new System.Drawing.Size(689, 98);
            this.gb_Info.TabIndex = 29;
            this.gb_Info.TabStop = false;
            this.gb_Info.Text = "연결 정보";
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
            this.lblState.Location = new System.Drawing.Point(571, 58);
            this.lblState.Name = "lblState";
            this.lblState.Picture = null;
            this.lblState.Size = new System.Drawing.Size(94, 24);
            this.lblState.TabIndex = 21;
            this.lblState.Text = "CLOSE";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_cap_port
            // 
            this.lbl_cap_port.AutoSize = true;
            this.lbl_cap_port.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cap_port.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_cap_port.ForeColor = System.Drawing.Color.Black;
            this.lbl_cap_port.Location = new System.Drawing.Point(568, 30);
            this.lbl_cap_port.Name = "lbl_cap_port";
            this.lbl_cap_port.Size = new System.Drawing.Size(43, 13);
            this.lbl_cap_port.TabIndex = 17;
            this.lbl_cap_port.Text = "Stop :";
            this.lbl_cap_port.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(426, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 13);
            this.label15.TabIndex = 16;
            this.label15.Text = "Parity :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(291, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "Data :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(151, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Baud :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_cap_ip
            // 
            this.lbl_cap_ip.AutoSize = true;
            this.lbl_cap_ip.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cap_ip.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_cap_ip.ForeColor = System.Drawing.Color.Black;
            this.lbl_cap_ip.Location = new System.Drawing.Point(19, 30);
            this.lbl_cap_ip.Name = "lbl_cap_ip";
            this.lbl_cap_ip.Size = new System.Drawing.Size(40, 13);
            this.lbl_cap_ip.TabIndex = 16;
            this.lbl_cap_ip.Text = "Port :";
            this.lbl_cap_ip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblParitybit
            // 
            this.lblParitybit.BackColor = System.Drawing.Color.Transparent;
            this.lblParitybit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblParitybit.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblParitybit.ForeColor = System.Drawing.Color.Black;
            this.lblParitybit.Location = new System.Drawing.Point(482, 30);
            this.lblParitybit.Name = "lblParitybit";
            this.lblParitybit.Size = new System.Drawing.Size(48, 15);
            this.lblParitybit.TabIndex = 20;
            this.lblParitybit.Text = "_____";
            this.lblParitybit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDatabit
            // 
            this.lblDatabit.BackColor = System.Drawing.Color.Transparent;
            this.lblDatabit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDatabit.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDatabit.ForeColor = System.Drawing.Color.Black;
            this.lblDatabit.Location = new System.Drawing.Point(340, 30);
            this.lblDatabit.Name = "lblDatabit";
            this.lblDatabit.Size = new System.Drawing.Size(48, 15);
            this.lblDatabit.TabIndex = 20;
            this.lblDatabit.Text = "_____";
            this.lblDatabit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBaudrate
            // 
            this.lblBaudrate.BackColor = System.Drawing.Color.Transparent;
            this.lblBaudrate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBaudrate.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblBaudrate.ForeColor = System.Drawing.Color.Black;
            this.lblBaudrate.Location = new System.Drawing.Point(205, 30);
            this.lblBaudrate.Name = "lblBaudrate";
            this.lblBaudrate.Size = new System.Drawing.Size(48, 15);
            this.lblBaudrate.TabIndex = 20;
            this.lblBaudrate.Text = "_____";
            this.lblBaudrate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStopbit
            // 
            this.lblStopbit.BackColor = System.Drawing.Color.Transparent;
            this.lblStopbit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStopbit.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStopbit.ForeColor = System.Drawing.Color.Black;
            this.lblStopbit.Location = new System.Drawing.Point(617, 29);
            this.lblStopbit.Name = "lblStopbit";
            this.lblStopbit.Size = new System.Drawing.Size(48, 15);
            this.lblStopbit.TabIndex = 20;
            this.lblStopbit.Text = "_____";
            this.lblStopbit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWriteTimeOut
            // 
            this.lblWriteTimeOut.BackColor = System.Drawing.Color.Transparent;
            this.lblWriteTimeOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWriteTimeOut.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblWriteTimeOut.ForeColor = System.Drawing.Color.Black;
            this.lblWriteTimeOut.Location = new System.Drawing.Point(483, 64);
            this.lblWriteTimeOut.Name = "lblWriteTimeOut";
            this.lblWriteTimeOut.Size = new System.Drawing.Size(62, 15);
            this.lblWriteTimeOut.TabIndex = 19;
            this.lblWriteTimeOut.Text = "_____";
            this.lblWriteTimeOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblReadTimeOut
            // 
            this.lblReadTimeOut.BackColor = System.Drawing.Color.Transparent;
            this.lblReadTimeOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblReadTimeOut.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblReadTimeOut.ForeColor = System.Drawing.Color.Black;
            this.lblReadTimeOut.Location = new System.Drawing.Point(291, 64);
            this.lblReadTimeOut.Name = "lblReadTimeOut";
            this.lblReadTimeOut.Size = new System.Drawing.Size(62, 15);
            this.lblReadTimeOut.TabIndex = 19;
            this.lblReadTimeOut.Text = "_____";
            this.lblReadTimeOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDelimiter
            // 
            this.lblDelimiter.BackColor = System.Drawing.Color.Transparent;
            this.lblDelimiter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDelimiter.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDelimiter.ForeColor = System.Drawing.Color.Black;
            this.lblDelimiter.Location = new System.Drawing.Point(97, 64);
            this.lblDelimiter.Name = "lblDelimiter";
            this.lblDelimiter.Size = new System.Drawing.Size(62, 15);
            this.lblDelimiter.TabIndex = 19;
            this.lblDelimiter.Text = "_____";
            this.lblDelimiter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPortname
            // 
            this.lblPortname.BackColor = System.Drawing.Color.Transparent;
            this.lblPortname.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPortname.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPortname.ForeColor = System.Drawing.Color.Black;
            this.lblPortname.Location = new System.Drawing.Point(65, 30);
            this.lblPortname.Name = "lblPortname";
            this.lblPortname.Size = new System.Drawing.Size(62, 15);
            this.lblPortname.TabIndex = 19;
            this.lblPortname.Text = "_____";
            this.lblPortname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(380, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "WriteTimeOut :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(186, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "ReadTimeOut :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_cap_state
            // 
            this.lbl_cap_state.AutoSize = true;
            this.lbl_cap_state.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cap_state.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_cap_state.ForeColor = System.Drawing.Color.Black;
            this.lbl_cap_state.Location = new System.Drawing.Point(19, 65);
            this.lbl_cap_state.Name = "lbl_cap_state";
            this.lbl_cap_state.Size = new System.Drawing.Size(72, 13);
            this.lbl_cap_state.TabIndex = 18;
            this.lbl_cap_state.Text = "Delimiter : ";
            this.lbl_cap_state.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstLog
            // 
            this.lstLog.BackColor = System.Drawing.SystemColors.Window;
            this.lstLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cIcon,
            this.cTime,
            this.cData});
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lstLog.FullRowSelect = true;
            this.lstLog.GridLines = true;
            this.lstLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstLog.Location = new System.Drawing.Point(0, 165);
            this.lstLog.Margin = new System.Windows.Forms.Padding(0);
            this.lstLog.MultiSelect = false;
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(697, 358);
            this.lstLog.SmallImageList = this.imgTrans;
            this.lstLog.TabIndex = 7;
            this.lstLog.UseCompatibleStateImageBehavior = false;
            this.lstLog.View = System.Windows.Forms.View.Details;
            // 
            // cIcon
            // 
            this.cIcon.Text = "";
            this.cIcon.Width = 30;
            // 
            // cTime
            // 
            this.cTime.Text = "Time";
            this.cTime.Width = 180;
            // 
            // cData
            // 
            this.cData.Text = "Data";
            this.cData.Width = 483;
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
            // lbl_MessageWaitCount
            // 
            this.lbl_MessageWaitCount.BackColor = System.Drawing.Color.Transparent;
            this.lbl_MessageWaitCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_MessageWaitCount.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_MessageWaitCount.ForeColor = System.Drawing.Color.Black;
            this.lbl_MessageWaitCount.Location = new System.Drawing.Point(451, 27);
            this.lbl_MessageWaitCount.Name = "lbl_MessageWaitCount";
            this.lbl_MessageWaitCount.Size = new System.Drawing.Size(159, 15);
            this.lbl_MessageWaitCount.TabIndex = 31;
            this.lbl_MessageWaitCount.Text = "_____";
            this.lbl_MessageWaitCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(307, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "MessageWaitCount : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SerialView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(697, 523);
            this.ControlBox = false;
            this.Controls.Add(this.tlp_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SerialView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Serial Viewer";
            this.tlp_Main.ResumeLayout(false);
            this.pn_Top.ResumeLayout(false);
            this.pn_Top.PerformLayout();
            this.pnl_Center.ResumeLayout(false);
            this.gb_Info.ResumeLayout(false);
            this.gb_Info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.Label lbl_bar;
        private System.Windows.Forms.Panel pn_Top;
        private System.Windows.Forms.Button btn_WinState;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Label lbl_Tilte;
        private System.Windows.Forms.ListView lstLog;
        private System.Windows.Forms.ColumnHeader cIcon;
        private System.Windows.Forms.ColumnHeader cTime;
        private System.Windows.Forms.ColumnHeader cData;
        private System.Windows.Forms.ImageList imgTrans;
        private System.Windows.Forms.Panel pnl_Center;
        private System.Windows.Forms.GroupBox gb_Info;
        private UI.SLabel lblState;
        private System.Windows.Forms.Label lbl_cap_port;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_cap_ip;
        private System.Windows.Forms.Label lblParitybit;
        private System.Windows.Forms.Label lblDatabit;
        private System.Windows.Forms.Label lblBaudrate;
        private System.Windows.Forms.Label lblStopbit;
        private System.Windows.Forms.Label lblWriteTimeOut;
        private System.Windows.Forms.Label lblReadTimeOut;
        private System.Windows.Forms.Label lblDelimiter;
        private System.Windows.Forms.Label lblPortname;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_cap_state;
        private System.Windows.Forms.Label lbl_MessageWaitCount;
        private System.Windows.Forms.Label label3;
    }
}