namespace SPApi.Controls.Plc.OpcServer
{ 
    partial class Opc
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
            this.slbl_Title = new SPApi.Controls.UI.SLabel();
            this.tip_Info = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // slbl_Title
            // 
            this.slbl_Title.BorderColor = System.Drawing.Color.Black;
            this.slbl_Title.BorderVisible = true;
            this.slbl_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slbl_Title.ExBorderStyle = SPApi.Controls.UI.CustomBorderStyle.None;
            this.slbl_Title.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.slbl_Title.GradientEnabled = true;
            this.slbl_Title.GradientEndColor = System.Drawing.Color.DarkGray;
            this.slbl_Title.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.slbl_Title.GradientStartColor = System.Drawing.Color.White;
            this.slbl_Title.ImageEnabled = false;
            this.slbl_Title.ImageLocation = new System.Drawing.Point(0, 0);
            this.slbl_Title.Location = new System.Drawing.Point(0, 0);
            this.slbl_Title.Margin = new System.Windows.Forms.Padding(0);
            this.slbl_Title.Name = "slbl_Title";
            this.slbl_Title.Picture = null;
            this.slbl_Title.RoundCornerRadius = 0;
            this.slbl_Title.Size = new System.Drawing.Size(55, 35);
            this.slbl_Title.TabIndex = 3;
            this.slbl_Title.Text = "OPC";
            this.slbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slbl_Title.Click += new System.EventHandler(this.slbl_Status_Click);
            // 
            // Opc
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.slbl_Title);
            this.Name = "Opc";
            this.Size = new System.Drawing.Size(55, 35);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.SLabel slbl_Title;
        private System.Windows.Forms.ToolTip tip_Info;
    }
}
