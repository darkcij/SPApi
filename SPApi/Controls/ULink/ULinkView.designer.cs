namespace SPApi.Controls.ULink
{
    partial class ULinkView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ULinkView));
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_bar = new System.Windows.Forms.Label();
            this.pn_Top = new System.Windows.Forms.Panel();
            this.btn_WinState = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.lbl_Tilte = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListView();
            this.cIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgTrans = new System.Windows.Forms.ImageList(this.components);
            this.lbl_MessageWaitCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tlp_Main.SuspendLayout();
            this.pn_Top.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.lbl_bar, 0, 1);
            this.tlp_Main.Controls.Add(this.pn_Top, 0, 0);
            this.tlp_Main.Controls.Add(this.lstLog, 0, 2);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 3;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_Main.Size = new System.Drawing.Size(693, 523);
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
            this.lbl_bar.Size = new System.Drawing.Size(691, 3);
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
            this.pn_Top.Size = new System.Drawing.Size(691, 49);
            this.pn_Top.TabIndex = 6;
            this.pn_Top.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Top_MouseMove);
            // 
            // btn_WinState
            // 
            this.btn_WinState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_WinState.BackColor = System.Drawing.SystemColors.Control;
            this.btn_WinState.BackgroundImage = global::SPApi.Properties.Resources.btn_FullScreen;
            this.btn_WinState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_WinState.Location = new System.Drawing.Point(612, 9);
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
            this.btnHide.Location = new System.Drawing.Point(647, 9);
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
            this.lbl_Tilte.Text = "ULink Viewer";
            this.lbl_Tilte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Tilte.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Top_MouseMove);
            // 
            // lstLog
            // 
            this.lstLog.BackColor = System.Drawing.SystemColors.Window;
            this.lstLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cIcon,
            this.cTime,
            this.cData});
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lstLog.FullRowSelect = true;
            this.lstLog.GridLines = true;
            this.lstLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstLog.Location = new System.Drawing.Point(0, 53);
            this.lstLog.Margin = new System.Windows.Forms.Padding(0);
            this.lstLog.MultiSelect = false;
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(693, 470);
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
            this.cTime.Width = 150;
            // 
            // cData
            // 
            this.cData.Text = "Data";
            this.cData.Width = 508;
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
            this.lbl_MessageWaitCount.Location = new System.Drawing.Point(446, 27);
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
            this.label3.Location = new System.Drawing.Point(302, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "MessageWaitCount : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ULinkView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(693, 523);
            this.ControlBox = false;
            this.Controls.Add(this.tlp_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ULinkView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Serial Viewer";
            this.tlp_Main.ResumeLayout(false);
            this.pn_Top.ResumeLayout(false);
            this.pn_Top.PerformLayout();
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
        private System.Windows.Forms.Label lbl_MessageWaitCount;
        private System.Windows.Forms.Label label3;
    }
}