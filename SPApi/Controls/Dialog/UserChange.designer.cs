namespace SPApi.Controls.Dialog
{
    partial class UserChange
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserChange));
            this.lbl_Tilte = new System.Windows.Forms.Label();
            this.lbl_bar = new System.Windows.Forms.Label();
            this.pn_Top = new System.Windows.Forms.Panel();
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_Center = new System.Windows.Forms.Panel();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.txt_Id = new System.Windows.Forms.TextBox();
            this.dgv_User = new System.Windows.Forms.DataGridView();
            this.btn_Ok = new SPApi.Controls.UI.SButton();
            this.btn_Delete = new SPApi.Controls.UI.SButton();
            this.btn_Append = new SPApi.Controls.UI.SButton();
            this.btn_Find = new SPApi.Controls.UI.SButton();
            this.kPad = new SPApi.Controls.UI.TenKey();
            this.pn_Top.SuspendLayout();
            this.tlp_Main.SuspendLayout();
            this.pnl_Center.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_User)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Tilte
            // 
            this.lbl_Tilte.BackColor = System.Drawing.Color.White;
            this.lbl_Tilte.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Tilte.Location = new System.Drawing.Point(7, 7);
            this.lbl_Tilte.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.lbl_Tilte.Name = "lbl_Tilte";
            this.lbl_Tilte.Size = new System.Drawing.Size(176, 35);
            this.lbl_Tilte.TabIndex = 5;
            this.lbl_Tilte.Text = "작업자 변경";
            this.lbl_Tilte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_bar
            // 
            this.lbl_bar.BackColor = System.Drawing.Color.DarkBlue;
            this.lbl_bar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_bar.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_bar.Location = new System.Drawing.Point(1, 50);
            this.lbl_bar.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lbl_bar.Name = "lbl_bar";
            this.lbl_bar.Size = new System.Drawing.Size(780, 3);
            this.lbl_bar.TabIndex = 8;
            this.lbl_bar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pn_Top
            // 
            this.pn_Top.BackColor = System.Drawing.Color.White;
            this.pn_Top.Controls.Add(this.lbl_Tilte);
            this.pn_Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_Top.Location = new System.Drawing.Point(1, 1);
            this.pn_Top.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.pn_Top.Name = "pn_Top";
            this.pn_Top.Size = new System.Drawing.Size(780, 49);
            this.pn_Top.TabIndex = 7;
            // 
            // tlp_Main
            // 
            this.tlp_Main.BackColor = System.Drawing.Color.Gray;
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.pnl_Center, 0, 2);
            this.tlp_Main.Controls.Add(this.lbl_bar, 0, 1);
            this.tlp_Main.Controls.Add(this.pn_Top, 0, 0);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 3;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlp_Main.Size = new System.Drawing.Size(782, 711);
            this.tlp_Main.TabIndex = 24;
            // 
            // pnl_Center
            // 
            this.pnl_Center.BackgroundImage = global::SPApi.Properties.Resources.UserChange;
            this.pnl_Center.Controls.Add(this.lbl_Name);
            this.pnl_Center.Controls.Add(this.txt_Id);
            this.pnl_Center.Controls.Add(this.dgv_User);
            this.pnl_Center.Controls.Add(this.btn_Ok);
            this.pnl_Center.Controls.Add(this.btn_Delete);
            this.pnl_Center.Controls.Add(this.btn_Append);
            this.pnl_Center.Controls.Add(this.btn_Find);
            this.pnl_Center.Controls.Add(this.kPad);
            this.pnl_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Center.Location = new System.Drawing.Point(1, 53);
            this.pnl_Center.Margin = new System.Windows.Forms.Padding(1, 0, 1, 1);
            this.pnl_Center.Name = "pnl_Center";
            this.pnl_Center.Size = new System.Drawing.Size(780, 657);
            this.pnl_Center.TabIndex = 23;
            // 
            // lbl_Name
            // 
            this.lbl_Name.BackColor = System.Drawing.Color.White;
            this.lbl_Name.Font = new System.Drawing.Font("굴림", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Name.ForeColor = System.Drawing.Color.Black;
            this.lbl_Name.Location = new System.Drawing.Point(394, 214);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(368, 74);
            this.lbl_Name.TabIndex = 37;
            this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Id
            // 
            this.txt_Id.BackColor = System.Drawing.Color.White;
            this.txt_Id.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Id.Font = new System.Drawing.Font("굴림체", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_Id.Location = new System.Drawing.Point(394, 89);
            this.txt_Id.MaxLength = 6;
            this.txt_Id.Name = "txt_Id";
            this.txt_Id.Size = new System.Drawing.Size(368, 74);
            this.txt_Id.TabIndex = 24;
            this.txt_Id.Tag = "0";
            this.txt_Id.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Id.WordWrap = false;
            this.txt_Id.TextChanged += new System.EventHandler(this.txt_Id_TextChanged);
            this.txt_Id.Enter += new System.EventHandler(this.txt_Id_Enter);
            // 
            // dgv_User
            // 
            this.dgv_User.AllowUserToAddRows = false;
            this.dgv_User.AllowUserToDeleteRows = false;
            this.dgv_User.AllowUserToResizeColumns = false;
            this.dgv_User.AllowUserToResizeRows = false;
            this.dgv_User.ColumnHeadersHeight = 60;
            this.dgv_User.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_User.Location = new System.Drawing.Point(6, 38);
            this.dgv_User.MultiSelect = false;
            this.dgv_User.Name = "dgv_User";
            this.dgv_User.ReadOnly = true;
            this.dgv_User.RowHeadersVisible = false;
            this.dgv_User.RowTemplate.Height = 60;
            this.dgv_User.RowTemplate.ReadOnly = true;
            this.dgv_User.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_User.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_User.ShowCellErrors = false;
            this.dgv_User.ShowCellToolTips = false;
            this.dgv_User.ShowEditingIcon = false;
            this.dgv_User.ShowRowErrors = false;
            this.dgv_User.Size = new System.Drawing.Size(363, 523);
            this.dgv_User.TabIndex = 23;
            this.dgv_User.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_User_CellMouseClick);
            // 
            // btn_Ok
            // 
            this.btn_Ok.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Ok.BorderColor = System.Drawing.Color.Black;
            this.btn_Ok.BorderStyle = SPApi.Controls.UI.CustomBorderStyle.Raised;
            this.btn_Ok.CornerRadius = 10;
            this.btn_Ok.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Ok.Location = new System.Drawing.Point(11, 569);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.RoundCorners = ((SPApi.Controls.UI.Corners)((((SPApi.Controls.UI.Corners.TopLeft | SPApi.Controls.UI.Corners.TopRight) 
            | SPApi.Controls.UI.Corners.BottomLeft) 
            | SPApi.Controls.UI.Corners.BottomRight)));
            this.btn_Ok.Size = new System.Drawing.Size(353, 79);
            this.btn_Ok.TabIndex = 19;
            this.btn_Ok.Text = "확인 및 닫기";
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btn_Delete.BorderColor = System.Drawing.Color.Black;
            this.btn_Delete.BorderStyle = SPApi.Controls.UI.CustomBorderStyle.None;
            this.btn_Delete.CornerRadius = 10;
            this.btn_Delete.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Delete.Location = new System.Drawing.Point(657, 537);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.RoundCorners = ((SPApi.Controls.UI.Corners)((((SPApi.Controls.UI.Corners.TopLeft | SPApi.Controls.UI.Corners.TopRight) 
            | SPApi.Controls.UI.Corners.BottomLeft) 
            | SPApi.Controls.UI.Corners.BottomRight)));
            this.btn_Delete.Size = new System.Drawing.Size(111, 108);
            this.btn_Delete.TabIndex = 18;
            this.btn_Delete.Text = "삭제";
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Append
            // 
            this.btn_Append.BackColor = System.Drawing.Color.LightGreen;
            this.btn_Append.BorderColor = System.Drawing.Color.Black;
            this.btn_Append.BorderStyle = SPApi.Controls.UI.CustomBorderStyle.None;
            this.btn_Append.CornerRadius = 10;
            this.btn_Append.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Append.Location = new System.Drawing.Point(657, 424);
            this.btn_Append.Name = "btn_Append";
            this.btn_Append.RoundCorners = ((SPApi.Controls.UI.Corners)((((SPApi.Controls.UI.Corners.TopLeft | SPApi.Controls.UI.Corners.TopRight) 
            | SPApi.Controls.UI.Corners.BottomLeft) 
            | SPApi.Controls.UI.Corners.BottomRight)));
            this.btn_Append.Size = new System.Drawing.Size(111, 108);
            this.btn_Append.TabIndex = 18;
            this.btn_Append.Text = "추가";
            this.btn_Append.Click += new System.EventHandler(this.btn_Append_Click);
            // 
            // btn_Find
            // 
            this.btn_Find.BackColor = System.Drawing.Color.LightBlue;
            this.btn_Find.BorderColor = System.Drawing.Color.Black;
            this.btn_Find.BorderStyle = SPApi.Controls.UI.CustomBorderStyle.None;
            this.btn_Find.CornerRadius = 10;
            this.btn_Find.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Find.Location = new System.Drawing.Point(657, 311);
            this.btn_Find.Name = "btn_Find";
            this.btn_Find.RoundCorners = ((SPApi.Controls.UI.Corners)((((SPApi.Controls.UI.Corners.TopLeft | SPApi.Controls.UI.Corners.TopRight) 
            | SPApi.Controls.UI.Corners.BottomLeft) 
            | SPApi.Controls.UI.Corners.BottomRight)));
            this.btn_Find.Size = new System.Drawing.Size(111, 108);
            this.btn_Find.TabIndex = 18;
            this.btn_Find.Text = "검색";
            this.btn_Find.Click += new System.EventHandler(this.btn_Find_Click);
            // 
            // kPad
            // 
            this.kPad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(232)))));
            this.kPad.Location = new System.Drawing.Point(386, 308);
            this.kPad.Name = "kPad";
            this.kPad.Size = new System.Drawing.Size(258, 342);
            this.kPad.TabIndex = 22;
            this.kPad.TabStop = false;
            // 
            // UserChange
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(782, 711);
            this.Controls.Add(this.tlp_Main);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UserChange";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.UserChange_Load);
            this.VisibleChanged += new System.EventHandler(this.UserChange_VisibleChanged);
            this.pn_Top.ResumeLayout(false);
            this.tlp_Main.ResumeLayout(false);
            this.pnl_Center.ResumeLayout(false);
            this.pnl_Center.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_User)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Tilte;
        private System.Windows.Forms.Label lbl_bar;
        private UI.SButton btn_Ok;
        private UI.SButton btn_Find;
        private UI.TenKey kPad;
        private System.Windows.Forms.Panel pn_Top;
        private System.Windows.Forms.Panel pnl_Center;
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private UI.SButton btn_Delete;
        private UI.SButton btn_Append;
        private System.Windows.Forms.DataGridView dgv_User;
        private System.Windows.Forms.TextBox txt_Id;
        private System.Windows.Forms.Label lbl_Name;
    }
}
