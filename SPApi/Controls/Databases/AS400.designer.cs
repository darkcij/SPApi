namespace SPApi.Controls.Databases
{
    partial class AS400
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
            this.components = new System.ComponentModel.Container();
            this.tip_Info = new System.Windows.Forms.ToolTip(this.components);
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.slbl_Status = new SPApi.Controls.UI.SLabel();
            this.slbl_Status2 = new SPApi.Controls.UI.SLabel();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.Black;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.slbl_Status, 0, 0);
            this.tlpMain.Controls.Add(this.slbl_Status2, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Size = new System.Drawing.Size(60, 35);
            this.tlpMain.TabIndex = 3;
            // 
            // slbl_Status
            // 
            this.slbl_Status.BorderColor = System.Drawing.Color.Black;
            this.slbl_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slbl_Status.ExBorderStyle = SPApi.Controls.UI.CustomBorderStyle.None;
            this.slbl_Status.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.slbl_Status.GradientEnabled = true;
            this.slbl_Status.GradientEndColor = System.Drawing.Color.DarkGray;
            this.slbl_Status.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.slbl_Status.GradientStartColor = System.Drawing.Color.White;
            this.slbl_Status.ImageEnabled = false;
            this.slbl_Status.ImageLocation = new System.Drawing.Point(0, 0);
            this.slbl_Status.Location = new System.Drawing.Point(0, 0);
            this.slbl_Status.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.slbl_Status.Name = "slbl_Status";
            this.slbl_Status.Picture = null;
            this.slbl_Status.RoundCornerRadius = 0;
            this.slbl_Status.ShadowOffSet = 0;
            this.slbl_Status.Size = new System.Drawing.Size(59, 17);
            this.slbl_Status.TabIndex = 2;
            this.slbl_Status.Text = "HOST[D]";
            this.slbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slbl_Status.Click += new System.EventHandler(this.slblStatus_Click);
            // 
            // slbl_Status2
            // 
            this.slbl_Status2.BorderColor = System.Drawing.Color.Black;
            this.slbl_Status2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slbl_Status2.ExBorderStyle = SPApi.Controls.UI.CustomBorderStyle.None;
            this.slbl_Status2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.slbl_Status2.GradientEnabled = true;
            this.slbl_Status2.GradientEndColor = System.Drawing.Color.DarkGray;
            this.slbl_Status2.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.slbl_Status2.GradientStartColor = System.Drawing.Color.White;
            this.slbl_Status2.ImageEnabled = false;
            this.slbl_Status2.ImageLocation = new System.Drawing.Point(0, 0);
            this.slbl_Status2.Location = new System.Drawing.Point(0, 17);
            this.slbl_Status2.Margin = new System.Windows.Forms.Padding(0, 0, 1, 1);
            this.slbl_Status2.Name = "slbl_Status2";
            this.slbl_Status2.Picture = null;
            this.slbl_Status2.RoundCornerRadius = 0;
            this.slbl_Status2.ShadowOffSet = 0;
            this.slbl_Status2.Size = new System.Drawing.Size(59, 17);
            this.slbl_Status2.TabIndex = 4;
            this.slbl_Status2.Text = "HOST[U]";
            this.slbl_Status2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slbl_Status2.Click += new System.EventHandler(this.slbl_Status2_Click);
            // 
            // AS400
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tlpMain);
            this.DoubleBuffered = true;
            this.Name = "AS400";
            this.Size = new System.Drawing.Size(60, 35);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip tip_Info;
        private SPApi.Controls.UI.SLabel slbl_Status;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private SPApi.Controls.UI.SLabel slbl_Status2;
    }
}
