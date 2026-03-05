using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPApi.Commons;
using SPApi.Controls.Databases;
using SPApi.Declaration.Structs;

namespace SPApi.Controls.Dialog
{
    public partial class UserChange : Form
    {
        #region Members

        private AS400 host = null;
        private SQLite localDB = null;

        public AS400 Host
        {
            set { host = value; }
        }

        public SQLite LocalDB
        {
            set { localDB = value; }
        }

        private static UserChange Instance;
        public static UserChange GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new UserChange();
                }
                return Instance;
            }
        }

        #endregion

        #region Properties

        public string W_NAME { get; set; }
        public string W_ID { get; set; }

        #endregion

        #region UserChange Control

        /// <summary>
        /// 생성자
        /// </summary>
        public UserChange()
        {
            Instance = this;
            
            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitializeComponent();

            //기본 설정
            DefaultSetting();
        }

        /// <summary>
        /// 폼 로드 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserChange_Load(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region ID TextBox 이벤트

        /// <summary>
        /// 사번 텍스트박스 Focus 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Id_Enter(object sender, EventArgs e)
        {
            txt_Id.BackColor = Color.Yellow;
        }

        /// <summary>
        /// 사번 텍스트박스 TextChanged 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Id_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lbl_Name.Text))
                lbl_Name.Text = string.Empty;
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

        private void UserChange_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                //로컬DB의 작업자 정보 로드
                SetGridData();
            }
        }

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
                    txt_Id.Text = string.Empty;
                    break;
                case "←":
                    txt_Id.Text = kPad.GetBackSpaceValue(txt_Id.Text);
                    break;
                default:
                    if (!txt_Id.Text.Length.Equals(6))
                        txt_Id.AppendText(key);
                    break;
            }
        }

        #endregion

        #region Grid

        /// <summary>
        /// 그리드 데이터 설정
        /// </summary>
        private void SetGridData()
        {
            try
            {
                DataTable dt;
                
                StringBuilder query = new StringBuilder();
                query.Append("SELECT ");
                query.Append("WRKID 사번,");
                query.Append("WRKNAM 이름 ");
                query.Append("FROM USER ");
                query.Append("ORDER BY WRKID");

                PARAM_QUERY pQry = new PARAM_QUERY()
                {
                    Query = query.ToString(),
                    Param = null
                };

                if (localDB.SelectCmd(pQry, out dt))
                {
                    dgv_User.SuspendLayout();

                    int[] rowIndex = CheckGridSelection();
                    dgv_User.DataSource = dt;
                    SetGridSelection(rowIndex);
                    SetGridStyle();

                    dgv_User.ClearSelection();

                    dgv_User.ResumeLayout();
                }
                else
                    dgv_User.DataSource = null;

                dt.Dispose();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 그리드 스타일 설정
        /// </summary>
        private void SetGridStyle()
        {
            int[] columnSizeArray = new int[] { 170, 170 };

            //Grid DefaultColumn Font 설정
            dgv_User.ColumnHeadersDefaultCellStyle.Font = new Font("굴림체", 28f, FontStyle.Bold);

            //Grid Column Font 설정
            dgv_User.DefaultCellStyle.Font = new Font("굴림체", 28f, FontStyle.Bold);

            //Grid Column Size 설정
            for (int i = 0; i < columnSizeArray.Length; i++)
            {
                dgv_User.Columns[i].Width = columnSizeArray[i];
                dgv_User.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 1)
                    dgv_User.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                else
                    dgv_User.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        /// <summary>
        /// 그리드 현재 선택된 Row 인덱스 저장
        /// </summary>
        private int[] CheckGridSelection()
        {
            int _gridScrollIndex = 0;
            int _gridSelectRow = 0;

            _gridScrollIndex = dgv_User.FirstDisplayedScrollingRowIndex;
            if (dgv_User.SelectedRows.Count > 0)
                _gridSelectRow = dgv_User.SelectedRows[0].Index;

            return new int[] { _gridScrollIndex, _gridSelectRow };
        }

        /// <summary>
        /// 그리드에 마지막 선택된 Row 인덱스 설정
        /// </summary>
        private void SetGridSelection(int[] gridindex)
        {
            if ((gridindex[0] >= 0) && ((dgv_User.Rows.Count - 1) >= gridindex[0]))
                dgv_User.FirstDisplayedScrollingRowIndex = gridindex[0];

            if ((dgv_User.Rows.Count - 1) >= gridindex[1])
                dgv_User.Rows[gridindex[1]].Selected = true;

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
            W_ID = txt_Id.Text;
            W_NAME = lbl_Name.Text;

            txt_Id.Text = string.Empty;
            lbl_Name.Text = string.Empty;

            Hide();
        }

        #endregion

        #region 찾기

        /// <summary>
        /// 찾기 버튼 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Find_Click(object sender, EventArgs e)
        {
            //호스트에서 작업자 정보 조회
            FindUserFromAS400();
        }

        /// <summary>
        /// 호스트에서 작업자 정보 조회
        /// </summary>
        private void FindUserFromAS400()
        {
            if(!txt_Id.TextLength.Equals(6))
            {
                MessageBox.Show("사번을 입력하세요.");
                return;
            }

            int sabun;

            if (!int.TryParse(txt_Id.Text.Trim(), out sabun))
            {
                MessageBox.Show("사번을 정확히 입력하세요.");
                return;
            }

            DataTable dt;

            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append("PNAME,");
            query.Append("PIDNO ");
            query.Append("FROM KPI.PIAMAS0 ");
            query.Append("WHERE PIDNO=? ");

            PARAM_QUERY pQry = new PARAM_QUERY()
            {
                Query = query.ToString(),
                Param = new ArrayList()
                {
                    txt_Id.Text
                }
            };
            

            if (host.SelectCmd(pQry, out dt))
            {
                DataRow dr = dt.Rows[0];

                lbl_Name.Text = dr[0].ToString().Trim();

                dt.Dispose();
            }
            else
            {
                MessageBox.Show("작업자 정보를 찾을 수 없습니다.");
            }
        }
        
        #endregion

        #region 추가

        /// <summary>
        /// 추가 버튼 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Append_Click(object sender, EventArgs e)
        {
            //로컬 DB에 작업자 정보 저장
            AddUserToLocalDB();
        }

        /// <summary>
        /// 로컬 DB에 작업자 정보 저장
        /// </summary>
        private void AddUserToLocalDB()
        {
            if (txt_Id.Text.NoE() || lbl_Name.Text.NoE())
            {
                MessageBox.Show("작업자 정보가 정확하지 않습니다.", "알림", MessageBoxButtons.OK);
                return;
            }
            
            foreach (DataGridViewRow row in dgv_User.Rows)
            {
                if (row.Cells["사번"].Value.ToString().Equals(txt_Id.Text))
                {
                    MessageBox.Show("이미 등록된 작업자 입니다.");
                    return;
                }
            }

            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO USER (");
            query.Append("WRKID,");
            query.Append("WRKNAM ) ");
            query.Append("VALUES ( ?, ? )");


            PARAM_QUERY pQry = new PARAM_QUERY()
            {
                Query = query.ToString(),
                Param = new ArrayList()
                {
                    txt_Id.Text,
                    lbl_Name.Text
                }
            };
                
            if (localDB.ExecuteCmd(pQry) >= 0)
            {
                //데이터 그리드 리플레쉬
                SetGridData();
            }
            else
                MessageBox.Show("로컬 DB에 작업자 정보를 추가하지 못했습니다.");
        }

        #endregion

        #region 삭제

        /// <summary>
        /// 삭제 버튼 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder();
            query.Append("DELETE FROM USER ");
            query.Append("WHERE WRKID = ?");

            PARAM_QUERY pQry = new PARAM_QUERY()
            {
                Query = query.ToString(),
                Param = new ArrayList()
                {
                    txt_Id.Text
                }
            };
                
            if (localDB.ExecuteCmd(pQry) >= 0)
            {
                //데이터 그리드 리플레쉬
                SetGridData();

                txt_Id.Text = string.Empty;
                lbl_Name.Text = string.Empty;
            }
            else
                MsgBox.GetInstance.ShowMessage("로컬 DB에 작업자 정보를 삭제하지 못했습니다.", "알림", true);
        }

        #endregion

        private void dgv_User_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txt_Id.Text = dgv_User.Rows[e.RowIndex].Cells["사번"].Value.ToString();
            lbl_Name.Text = dgv_User.Rows[e.RowIndex].Cells["이름"].Value.ToString();

        }
    }
}
