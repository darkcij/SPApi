namespace SPApi.Controls.Databases
{
    partial class OracleDatabaseControl
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
            this.lbl_Status = new SPApi.Controls.UI.SLabel();
            this.SuspendLayout();
            // 
            // tip_Info
            // 
            this.tip_Info.AutomaticDelay = 200;
            this.tip_Info.AutoPopDelay = 5000;
            this.tip_Info.InitialDelay = 200;
            this.tip_Info.ReshowDelay = 40;
            this.tip_Info.ShowAlways = true;
            this.tip_Info.ToolTipTitle = "Database 정보";
            // 
            // lbl_Status
            // 
            this.lbl_Status.BackColor = System.Drawing.Color.Black;
            this.lbl_Status.BorderColor = System.Drawing.Color.Black;
            this.lbl_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Status.ExBorderStyle = SPApi.Controls.UI.CustomBorderStyle.None;
            this.lbl_Status.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_Status.GradientEnabled = true;
            this.lbl_Status.GradientEndColor = System.Drawing.Color.DarkGray;
            this.lbl_Status.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.lbl_Status.GradientStartColor = System.Drawing.Color.White;
            this.lbl_Status.ImageEnabled = false;
            this.lbl_Status.ImageLocation = new System.Drawing.Point(4, 4);
            this.lbl_Status.Location = new System.Drawing.Point(0, 0);
            this.lbl_Status.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Picture = null;
            this.lbl_Status.RoundCornerRadius = 0;
            this.lbl_Status.Size = new System.Drawing.Size(58, 35);
            this.lbl_Status.TabIndex = 130;
            this.lbl_Status.Text = "ORACLE";
            this.lbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OracleDatabaseControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lbl_Status);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "OracleDatabaseControl";
            this.Size = new System.Drawing.Size(58, 35);
            this.Load += new System.EventHandler(this.OracleDatabaseControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private SPApi.Controls.UI.SLabel lbl_Status;
        private System.Windows.Forms.ToolTip tip_Info;

    }
}
