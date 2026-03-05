namespace SPApi.Controls.Plc.OpcServer
{
    partial class OpcView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpcView));
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_bar = new System.Windows.Forms.Label();
            this.dgv_Center = new System.Windows.Forms.DataGridView();
            this.pn_Menu = new System.Windows.Forms.Panel();
            this.cb_Type = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pn_Top = new System.Windows.Forms.Panel();
            this.btn_WinState = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.lbl_Tilte = new System.Windows.Forms.Label();
            this.tlp_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Center)).BeginInit();
            this.pn_Menu.SuspendLayout();
            this.pn_Top.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.lbl_bar, 0, 1);
            this.tlp_Main.Controls.Add(this.dgv_Center, 0, 3);
            this.tlp_Main.Controls.Add(this.pn_Menu, 0, 2);
            this.tlp_Main.Controls.Add(this.pn_Top, 0, 0);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 4;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(800, 600);
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
            this.lbl_bar.Size = new System.Drawing.Size(798, 3);
            this.lbl_bar.TabIndex = 5;
            this.lbl_bar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgv_Center
            // 
            this.dgv_Center.AllowUserToAddRows = false;
            this.dgv_Center.AllowUserToDeleteRows = false;
            this.dgv_Center.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Center.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Center.Location = new System.Drawing.Point(0, 98);
            this.dgv_Center.Margin = new System.Windows.Forms.Padding(0);
            this.dgv_Center.MultiSelect = false;
            this.dgv_Center.Name = "dgv_Center";
            this.dgv_Center.ReadOnly = true;
            this.dgv_Center.RowHeadersVisible = false;
            this.dgv_Center.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_Center.RowTemplate.Height = 23;
            this.dgv_Center.RowTemplate.ReadOnly = true;
            this.dgv_Center.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Center.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_Center.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Center.ShowCellErrors = false;
            this.dgv_Center.ShowCellToolTips = false;
            this.dgv_Center.ShowEditingIcon = false;
            this.dgv_Center.ShowRowErrors = false;
            this.dgv_Center.Size = new System.Drawing.Size(800, 502);
            this.dgv_Center.TabIndex = 2;
            // 
            // pn_Menu
            // 
            this.pn_Menu.BackColor = System.Drawing.SystemColors.Control;
            this.pn_Menu.Controls.Add(this.cb_Type);
            this.pn_Menu.Controls.Add(this.label1);
            this.pn_Menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_Menu.Location = new System.Drawing.Point(1, 53);
            this.pn_Menu.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.pn_Menu.Name = "pn_Menu";
            this.pn_Menu.Size = new System.Drawing.Size(798, 45);
            this.pn_Menu.TabIndex = 3;
            // 
            // cb_Type
            // 
            this.cb_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Type.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_Type.FormattingEnabled = true;
            this.cb_Type.Location = new System.Drawing.Point(93, 8);
            this.cb_Type.Name = "cb_Type";
            this.cb_Type.Size = new System.Drawing.Size(232, 29);
            this.cb_Type.TabIndex = 8;
            this.cb_Type.SelectionChangeCommitted += new System.EventHandler(this.cb_Type_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(17, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 27);
            this.label1.TabIndex = 7;
            this.label1.Text = "DB 선택";
            // 
            // pn_Top
            // 
            this.pn_Top.BackColor = System.Drawing.Color.White;
            this.pn_Top.Controls.Add(this.btn_WinState);
            this.pn_Top.Controls.Add(this.btnHide);
            this.pn_Top.Controls.Add(this.lbl_Tilte);
            this.pn_Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_Top.Location = new System.Drawing.Point(1, 1);
            this.pn_Top.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.pn_Top.Name = "pn_Top";
            this.pn_Top.Size = new System.Drawing.Size(798, 49);
            this.pn_Top.TabIndex = 6;
            // 
            // btn_WinState
            // 
            this.btn_WinState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_WinState.BackColor = System.Drawing.SystemColors.Control;
            this.btn_WinState.BackgroundImage = global::SPApi.Properties.Resources.btn_FullScreen;
            this.btn_WinState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_WinState.Location = new System.Drawing.Point(719, 9);
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
            this.btnHide.Location = new System.Drawing.Point(754, 9);
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
            this.lbl_Tilte.Size = new System.Drawing.Size(605, 35);
            this.lbl_Tilte.TabIndex = 5;
            this.lbl_Tilte.Text = "Opc Viewer";
            this.lbl_Tilte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OpcView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.ControlBox = false;
            this.Controls.Add(this.tlp_Main);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "OpcView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opc Viewer";
            this.tlp_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Center)).EndInit();
            this.pn_Menu.ResumeLayout(false);
            this.pn_Top.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.Label lbl_bar;
        private System.Windows.Forms.DataGridView dgv_Center;
        private System.Windows.Forms.Panel pn_Menu;
        private System.Windows.Forms.ComboBox cb_Type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pn_Top;
        private System.Windows.Forms.Button btn_WinState;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Label lbl_Tilte;
    }
}