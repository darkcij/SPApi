using SPApi.Commons;
using System;
using System.Windows.Forms;

namespace SPApi.Controls.Dialog
{
    public partial class InputBox : Form
    {
        #region Members
        
        private int _number = 0;

        private int _maxLength = 0;

        #endregion

        #region Properties

        public string Title
        {
            set { lbl_Tilte.Text = value; }
        }

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public int MaxLength
        {            
            set { _maxLength = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// 생성자
        /// </summary>
        public InputBox()
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

        protected override void OnLoad(EventArgs e)
        {
            //기본 설정
            DefaultSetting();

            base.OnLoad(e);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 기본 설정
        /// </summary>
        private void DefaultSetting()
        {
            //키패드 초기 설정
            KeypadInitialization();
            
        }

        //public void ShowKeyPad(string title, int maxLength)
        //{
        //    lbl_Tilte.Text = title;
        //    _txtLength = maxLength;
        //    txt_number.Clear();
        //    ShowDialog();
        //}

        #endregion

        #region KeyPad

        //키패드 초기 설정
        private void KeypadInitialization()
        {
            //키패드 설정
            kPad.OnKeyClick += kPad_OnKeyClick;
        }

        //키패드 이벤트 처리
        private void kPad_OnKeyClick(string key)
        {
            SetTextbox(key);
        }

        /// <summary>
        /// 텍스트박스 포커스 자동 이동 및 텍스트 입력
        /// </summary>
        private void SetTextbox(string key)
        {
            //키입력
            switch (key)
            {
                case "CLR":
                    txt_number.Text = string.Empty;
                    break;
                case "←":
                    txt_number.Text = kPad.GetBackSpaceValue(txt_number.Text);
                    break;
                default:
                    if (!txt_number.Text.Length.Equals(_maxLength))
                        txt_number.AppendText(key);
                    break;
            }
        }

        #endregion

        #region 확인 및 닫기

        /// <summary>
        /// 확인 및 닫기 버튼 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            _number = txt_number.Text.ToInt();
            
            DialogResult = DialogResult.OK;
        }


        #endregion

        
    }
}
