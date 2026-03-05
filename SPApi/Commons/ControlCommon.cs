using System;
using System.Windows.Forms;


namespace SPApi.Commons
{
    public class ControlCommon
    {
        #region Commons

        /// <summary>
        /// 폼 Null / Disposed 체크
        /// </summary>
        /// <returns></returns>
        public static bool FormNullCheck(Form viewer)
        {
            return (viewer == null || viewer.IsDisposed);
        }

        #endregion

        #region DataGridView

        /// <summary>
        /// 그리드 현재 선택된 Row 인덱스 저장
        /// </summary>
        public static int[] CheckGridSelection(DataGridView dgv)
        {
            int _gridScrollIndex = 0;
            int _gridSelectRow = 0;

            _gridScrollIndex = dgv.FirstDisplayedScrollingRowIndex;
            if (dgv.SelectedRows.Count > 0)
                _gridSelectRow = dgv.SelectedRows[0].Index;

            return new int[] { _gridScrollIndex, _gridSelectRow };
        }

        /// <summary>
        /// 그리드에 마지막 선택된 Row 인덱스 설정
        /// </summary>
        public static void SetGridSelection(DataGridView dgv, int[] gridindex)
        {
            if ((gridindex[0] >= 0) && ((dgv.Rows.Count - 1) >= gridindex[0]))
                dgv.FirstDisplayedScrollingRowIndex = gridindex[0];

            if ((dgv.Rows.Count - 1) >= gridindex[1])
                dgv.Rows[gridindex[1]].Selected = true;

        }

        /// <summary>
        /// 그리드의 Row Header에 Row 순번 설정
        /// </summary>
        public static void SetGridRowHeaderNo(DataGridView dgv, int padding = 3)
        {
            if (dgv.Rows.Count < 1) return;

            for (int i = 0; i < dgv.Rows.Count; i++)
                dgv.Rows[i].HeaderCell.Value = (i + 1).ToString().PadLeft(padding);
        }
        
        #endregion

        #region Handle

        public static void MouseMove(IntPtr handle)
        {
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage(handle, NativeMethods.WM_NLBUTTONDOWN, NativeMethods.HT_CAPTION, (IntPtr)0);
        }

        #endregion
    }
}
