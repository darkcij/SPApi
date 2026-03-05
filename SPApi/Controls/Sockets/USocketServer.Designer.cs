namespace SPApi.Controls.Sockets
{
    partial class USocketServer
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
            this.slbl_Status = new SPApi.Controls.UI.SLabel();
            this.SuspendLayout();
            // 
            // slbl_Status
            // 
            this.slbl_Status.BorderColor = System.Drawing.Color.Black;
            this.slbl_Status.BorderVisible = true;
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
            this.slbl_Status.Margin = new System.Windows.Forms.Padding(0);
            this.slbl_Status.Name = "slbl_Status";
            this.slbl_Status.Picture = null;
            this.slbl_Status.Size = new System.Drawing.Size(55, 35);
            this.slbl_Status.TabIndex = 2;
            this.slbl_Status.Text = "UServer";
            this.slbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slbl_Status.Click += new System.EventHandler(this.slbl_Status_Click);
            // 
            // USocketServer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.slbl_Status);
            this.Name = "USocketServer";
            this.Size = new System.Drawing.Size(55, 35);
            this.Load += new System.EventHandler(this.SocketServer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip tip_Info;
        private SPApi.Controls.UI.SLabel slbl_Status;
    }
}
