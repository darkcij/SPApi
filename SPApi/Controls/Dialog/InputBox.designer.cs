namespace SPApi.Controls.Dialog
{
    partial class InputBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBox));
            this.lbl_Tilte = new System.Windows.Forms.Label();
            this.lbl_bar = new System.Windows.Forms.Label();
            this.pn_Top = new System.Windows.Forms.Panel();
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_Center = new System.Windows.Forms.Panel();
            this.txt_number = new System.Windows.Forms.TextBox();
            this.btn_Ok = new SPApi.Controls.UI.SButton();
            this.kPad = new SPApi.Controls.UI.TenKey();
            this.pn_Top.SuspendLayout();
            this.tlp_Main.SuspendLayout();
            this.pnl_Center.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Tilte
            // 
            this.lbl_Tilte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Tilte.BackColor = System.Drawing.Color.White;
            this.lbl_Tilte.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Tilte.Location = new System.Drawing.Point(7, 7);
            this.lbl_Tilte.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.lbl_Tilte.Name = "lbl_Tilte";
            this.lbl_Tilte.Size = new System.Drawing.Size(387, 35);
            this.lbl_Tilte.TabIndex = 5;
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
            this.lbl_bar.Size = new System.Drawing.Size(400, 3);
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
            this.pn_Top.Size = new System.Drawing.Size(400, 49);
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
            this.tlp_Main.Size = new System.Drawing.Size(402, 602);
            this.tlp_Main.TabIndex = 24;
            // 
            // pnl_Center
            // 
            this.pnl_Center.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl_Center.Controls.Add(this.txt_number);
            this.pnl_Center.Controls.Add(this.btn_Ok);
            this.pnl_Center.Controls.Add(this.kPad);
            this.pnl_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Center.Location = new System.Drawing.Point(1, 53);
            this.pnl_Center.Margin = new System.Windows.Forms.Padding(1, 0, 1, 1);
            this.pnl_Center.Name = "pnl_Center";
            this.pnl_Center.Size = new System.Drawing.Size(400, 548);
            this.pnl_Center.TabIndex = 23;
            // 
            // txt_number
            // 
            this.txt_number.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_number.BackColor = System.Drawing.Color.Yellow;
            this.txt_number.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_number.Font = new System.Drawing.Font("굴림체", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_number.Location = new System.Drawing.Point(1, 0);
            this.txt_number.Name = "txt_number";
            this.txt_number.Size = new System.Drawing.Size(398, 74);
            this.txt_number.TabIndex = 24;
            this.txt_number.Tag = "0";
            this.txt_number.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_number.WordWrap = false;
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Ok.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Ok.BorderColor = System.Drawing.Color.Black;
            this.btn_Ok.BorderStyle = SPApi.Controls.UI.CustomBorderStyle.None;
            this.btn_Ok.CornerRadius = 10;
            this.btn_Ok.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Ok.Location = new System.Drawing.Point(1, 470);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(398, 77);
            this.btn_Ok.TabIndex = 19;
            this.btn_Ok.Text = "확인 및 닫기";
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // kPad
            // 
            this.kPad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kPad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(232)))));
            this.kPad.Location = new System.Drawing.Point(1, 76);
            this.kPad.Name = "kPad";
            this.kPad.Size = new System.Drawing.Size(398, 392);
            this.kPad.TabIndex = 22;
            this.kPad.TabStop = false;
            // 
            // InputBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(402, 602);
            this.Controls.Add(this.tlp_Main);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputBox";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INPUT BOX";
            this.pn_Top.ResumeLayout(false);
            this.tlp_Main.ResumeLayout(false);
            this.pnl_Center.ResumeLayout(false);
            this.pnl_Center.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Tilte;
        private System.Windows.Forms.Label lbl_bar;
        private SPApi.Controls.UI.SButton btn_Ok;
        private SPApi.Controls.UI.TenKey kPad;
        private System.Windows.Forms.Panel pn_Top;
        private System.Windows.Forms.Panel pnl_Center;
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.TextBox txt_number;
    }
}
