namespace SPApi.Controls.ULink
{
    partial class ULinkClient
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
            components = new System.ComponentModel.Container();
            slbl_Status = new SPApi.Controls.UI.SLabel();
            tip_Info = new System.Windows.Forms.ToolTip(components);
            SuspendLayout();
            // 
            // slbl_Status
            // 
            slbl_Status.BorderColor = System.Drawing.Color.Black;
            slbl_Status.BorderVisible = true;
            slbl_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            slbl_Status.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            slbl_Status.GradientEnabled = true;
            slbl_Status.GradientEndColor = System.Drawing.Color.DarkGray;
            slbl_Status.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            slbl_Status.GradientStartColor = System.Drawing.Color.White;
            slbl_Status.ImageEnabled = false;
            slbl_Status.ImageLocation = new System.Drawing.Point(0, 0);
            slbl_Status.Location = new System.Drawing.Point(0, 0);
            slbl_Status.Margin = new System.Windows.Forms.Padding(0);
            slbl_Status.Name = "slbl_Status";
            slbl_Status.Picture = null;
            slbl_Status.Size = new System.Drawing.Size(55, 35);
            slbl_Status.TabIndex = 4;
            slbl_Status.Text = "ULink";
            slbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            slbl_Status.Click += new System.EventHandler(slbl_Status_Click);
            // 
            // ULinkClient
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(slbl_Status);
            Name = "ULinkClient";
            Size = new System.Drawing.Size(55, 35);
            ResumeLayout(false);

        }

        #endregion

        private SPApi.Controls.UI.SLabel slbl_Status;
        private System.Windows.Forms.ToolTip tip_Info;
    }
}
