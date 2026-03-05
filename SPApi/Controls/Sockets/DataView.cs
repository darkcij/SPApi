using System;
using System.Drawing;
using System.Windows.Forms;
using SPApi.Commons;

namespace SPApi.Controls.Sockets
{
    public partial class DataView : Form
    {
        #region Properties

        public string Title
        {
            get { return lbl_Tilte.Text; }
            set { lbl_Tilte.SimpleInvoke("Text", value); }
        }
        
        #endregion

        #region Constructor

        public DataView()
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
            Close();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 기본 설정
        /// </summary>
        private void DefaultSetting()
        {
        }

        public void SetDataInfo(Image fin, string dtm, string laddr, string raddr, string qry)
        {
            picState.SimpleInvoke("BackgroundImage", fin);
            lblRunTime.SimpleInvoke("Text", dtm);
            lblLocalAddr.SimpleInvoke("Text", laddr);
            lblRemoteAddr.SimpleInvoke("Text", raddr);
            txtData.SimpleInvoke("Text", qry);    
        }

        #endregion

        
    }
}
