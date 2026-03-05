namespace SPApi.Controls.Dialog
{
    partial class MsgBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgBox));
            this.btn_Yes = new System.Windows.Forms.Button();
            this.btn_No = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.tmrHide = new System.Windows.Forms.Timer(this.components);
            this.lbl_Msg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Yes
            // 
            this.btn_Yes.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Yes.Location = new System.Drawing.Point(111, 161);
            this.btn_Yes.Name = "btn_Yes";
            this.btn_Yes.Size = new System.Drawing.Size(79, 36);
            this.btn_Yes.TabIndex = 1;
            this.btn_Yes.Text = "예(&Y)";
            this.btn_Yes.UseVisualStyleBackColor = true;
            this.btn_Yes.Click += new System.EventHandler(this.btn_Yes_Click);
            // 
            // btn_No
            // 
            this.btn_No.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_No.Location = new System.Drawing.Point(211, 161);
            this.btn_No.Name = "btn_No";
            this.btn_No.Size = new System.Drawing.Size(79, 36);
            this.btn_No.TabIndex = 2;
            this.btn_No.Text = "아니오(&N)";
            this.btn_No.UseVisualStyleBackColor = true;
            this.btn_No.Click += new System.EventHandler(this.btn_No_Click);
            // 
            // btn_Ok
            // 
            this.btn_Ok.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Ok.Location = new System.Drawing.Point(161, 161);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(79, 36);
            this.btn_Ok.TabIndex = 3;
            this.btn_Ok.Text = "확인(&O)";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // tmrHide
            // 
            this.tmrHide.Interval = 10000;
            this.tmrHide.Tick += new System.EventHandler(this.tmrHide_Tick);
            // 
            // lbl_Msg
            // 
            this.lbl_Msg.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Msg.ForeColor = System.Drawing.Color.Red;
            this.lbl_Msg.Location = new System.Drawing.Point(6, 5);
            this.lbl_Msg.Name = "lbl_Msg";
            this.lbl_Msg.Size = new System.Drawing.Size(388, 155);
            this.lbl_Msg.TabIndex = 4;
            this.lbl_Msg.Text = "MESSAGE";
            this.lbl_Msg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MsgBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 203);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_Msg);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.btn_No);
            this.Controls.Add(this.btn_Yes);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MsgBox";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "알림";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMsgBox_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Yes;
        private System.Windows.Forms.Button btn_No;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Timer tmrHide;
        private System.Windows.Forms.Label lbl_Msg;
    }
}