namespace SPApi.Controls.Databases
{
    partial class QueryView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryView));
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_bar = new System.Windows.Forms.Label();
            this.pn_Top = new System.Windows.Forms.Panel();
            this.btnHide = new System.Windows.Forms.Button();
            this.lbl_Tilte = new System.Windows.Forms.Label();
            this.pnl_Center = new System.Windows.Forms.Panel();
            this.gb_Info = new System.Windows.Forms.GroupBox();
            this.picState = new System.Windows.Forms.PictureBox();
            this.lbl_cap_ip = new System.Windows.Forms.Label();
            this.lblRunTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtQuery = new System.Windows.Forms.RichTextBox();
            this.tlp_Main.SuspendLayout();
            this.pn_Top.SuspendLayout();
            this.pnl_Center.SuspendLayout();
            this.gb_Info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picState)).BeginInit();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.lbl_bar, 0, 1);
            this.tlp_Main.Controls.Add(this.pn_Top, 0, 0);
            this.tlp_Main.Controls.Add(this.pnl_Center, 0, 2);
            this.tlp_Main.Controls.Add(this.txtQuery, 0, 3);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 4;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_Main.Size = new System.Drawing.Size(572, 483);
            this.tlp_Main.TabIndex = 2;
            // 
            // lbl_bar
            // 
            this.lbl_bar.BackColor = System.Drawing.Color.Sienna;
            this.lbl_bar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_bar.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_bar.Location = new System.Drawing.Point(1, 50);
            this.lbl_bar.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lbl_bar.Name = "lbl_bar";
            this.lbl_bar.Size = new System.Drawing.Size(570, 3);
            this.lbl_bar.TabIndex = 5;
            this.lbl_bar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pn_Top
            // 
            this.pn_Top.BackColor = System.Drawing.Color.White;
            this.pn_Top.Controls.Add(this.btnHide);
            this.pn_Top.Controls.Add(this.lbl_Tilte);
            this.pn_Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_Top.Location = new System.Drawing.Point(1, 1);
            this.pn_Top.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.pn_Top.Name = "pn_Top";
            this.pn_Top.Size = new System.Drawing.Size(570, 49);
            this.pn_Top.TabIndex = 6;
            this.pn_Top.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTilte_MouseMove);
            // 
            // btnHide
            // 
            this.btnHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHide.BackColor = System.Drawing.SystemColors.Control;
            this.btnHide.BackgroundImage = global::SPApi.Properties.Resources.btn_Min;
            this.btnHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHide.Location = new System.Drawing.Point(526, 9);
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
            this.lbl_Tilte.Size = new System.Drawing.Size(191, 35);
            this.lbl_Tilte.TabIndex = 5;
            this.lbl_Tilte.Text = "Query Viewer";
            this.lbl_Tilte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Tilte.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTilte_MouseMove);
            // 
            // pnl_Center
            // 
            this.pnl_Center.BackColor = System.Drawing.Color.LightGray;
            this.pnl_Center.Controls.Add(this.gb_Info);
            this.pnl_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Center.Location = new System.Drawing.Point(0, 53);
            this.pnl_Center.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_Center.Name = "pnl_Center";
            this.pnl_Center.Size = new System.Drawing.Size(572, 80);
            this.pnl_Center.TabIndex = 7;
            // 
            // gb_Info
            // 
            this.gb_Info.BackColor = System.Drawing.Color.LightGray;
            this.gb_Info.Controls.Add(this.picState);
            this.gb_Info.Controls.Add(this.lbl_cap_ip);
            this.gb_Info.Controls.Add(this.lblRunTime);
            this.gb_Info.Controls.Add(this.label10);
            this.gb_Info.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gb_Info.Location = new System.Drawing.Point(4, 7);
            this.gb_Info.Name = "gb_Info";
            this.gb_Info.Size = new System.Drawing.Size(564, 67);
            this.gb_Info.TabIndex = 29;
            this.gb_Info.TabStop = false;
            this.gb_Info.Text = "실행 정보";
            // 
            // picState
            // 
            this.picState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picState.Location = new System.Drawing.Point(91, 25);
            this.picState.Name = "picState";
            this.picState.Size = new System.Drawing.Size(24, 24);
            this.picState.TabIndex = 20;
            this.picState.TabStop = false;
            // 
            // lbl_cap_ip
            // 
            this.lbl_cap_ip.AutoSize = true;
            this.lbl_cap_ip.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cap_ip.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_cap_ip.ForeColor = System.Drawing.Color.Black;
            this.lbl_cap_ip.Location = new System.Drawing.Point(37, 30);
            this.lbl_cap_ip.Name = "lbl_cap_ip";
            this.lbl_cap_ip.Size = new System.Drawing.Size(41, 13);
            this.lbl_cap_ip.TabIndex = 16;
            this.lbl_cap_ip.Text = "상태 :";
            this.lbl_cap_ip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRunTime
            // 
            this.lblRunTime.BackColor = System.Drawing.Color.Transparent;
            this.lblRunTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRunTime.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblRunTime.ForeColor = System.Drawing.Color.Black;
            this.lblRunTime.Location = new System.Drawing.Point(237, 28);
            this.lblRunTime.Name = "lblRunTime";
            this.lblRunTime.Size = new System.Drawing.Size(169, 19);
            this.lblRunTime.TabIndex = 19;
            this.lblRunTime.Text = "_________";
            this.lblRunTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(168, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "실행시간 :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtQuery
            // 
            this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuery.Location = new System.Drawing.Point(1, 133);
            this.txtQuery.Margin = new System.Windows.Forms.Padding(1, 0, 1, 1);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ReadOnly = true;
            this.txtQuery.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtQuery.Size = new System.Drawing.Size(570, 349);
            this.txtQuery.TabIndex = 8;
            this.txtQuery.Text = "";
            // 
            // QueryView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(572, 483);
            this.ControlBox = false;
            this.Controls.Add(this.tlp_Main);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QueryView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Viewer";
            this.tlp_Main.ResumeLayout(false);
            this.pn_Top.ResumeLayout(false);
            this.pnl_Center.ResumeLayout(false);
            this.gb_Info.ResumeLayout(false);
            this.gb_Info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.Label lbl_bar;
        private System.Windows.Forms.Panel pn_Top;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Label lbl_Tilte;
        private System.Windows.Forms.Panel pnl_Center;
        private System.Windows.Forms.RichTextBox txtQuery;
        private System.Windows.Forms.GroupBox gb_Info;
        private System.Windows.Forms.PictureBox picState;
        private System.Windows.Forms.Label lbl_cap_ip;
        private System.Windows.Forms.Label lblRunTime;
        private System.Windows.Forms.Label label10;
    }
}