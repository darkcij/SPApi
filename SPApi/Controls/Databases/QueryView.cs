using System;
using System.Drawing;
using System.Windows.Forms;
using SPApi.Commons;

namespace SPApi.Controls.Databases
{
    public partial class QueryView : Form
    {
        #region Properties

        public string Title
        {
            get { return lbl_Tilte.Text; }
            set { lbl_Tilte.SimpleInvoke("Text", value); }
        }
        
        #endregion

        #region Constructor

        public QueryView()
        {
            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// 폼 이동 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTilte_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(Handle, NativeMethods.WM_NLBUTTONDOWN, NativeMethods.HT_CAPTION, (IntPtr)0);
            }
            base.OnMouseDown(e);
        }
        
        //폼 Hide
        private void btnHide_Click(object sender, EventArgs e)
        {
            Hide();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 기본 설정
        /// </summary>
        private void DefaultSetting()
        {
        }

        public void SetQueryInfo(Image fin, string dtm, string qry)
        {
            picState.SimpleInvoke("BackgroundImage", fin);
            lblRunTime.SimpleInvoke("Text", dtm);
            txtQuery.SimpleInvoke("Text", qry);    
        }

        #endregion

        
    }
}
