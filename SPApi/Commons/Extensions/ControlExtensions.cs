using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace SPApi.Commons
{
    public static class ControlExtensions
    {
        #region INVOKE

        /// <summary>
        /// Simple Control Property Invoke 처리
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);
        private delegate void ThreadSafeDelegate(Control control, Action action);

        public static void SimpleInvoke(this Control control, string propertyName, object propertyValue)
        {
            if (control == null || control.IsDisposed) return;

            if (control.InvokeRequired)
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SimpleInvoke), control, propertyName, propertyValue);
            else
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
        }

        /// <summary>
        /// Action Control Invoke 처리
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        public static void SimpleInvoke(Control control, Action action)
        {
            if (control == null || control.IsDisposed) return;

            if (control.InvokeRequired)
                control.Invoke(new ThreadSafeDelegate(SimpleInvoke), control, action);
            else
                action();
        }

        /// <summary>
        /// Action Control Invoke 처리
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        public static void InvokeToAction(this Control control, Action action)
        {
            if (control == null || control.IsDisposed) return;

            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action();
        }

        public static void InvokeToAction<T>(this Control control, Action<T> action, T arg)
        {
            if (control == null || control.IsDisposed) return;

            if (control.InvokeRequired)
                control.Invoke(action, arg);
            else
                action(arg);
        }
        public static void InvokeToAction<T1, T2>(this Control control, Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (control == null || control.IsDisposed) return;

            if (control.InvokeRequired)
                control.Invoke(action, arg1, arg2);
            else
                action(arg1, arg2);
        }
        public static void InvokeToAction<T1, T2, T3>(this Control control, Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            if (control == null || control.IsDisposed) return;

            if (control.InvokeRequired)
                control.Invoke(action, arg1, arg2, arg3);
            else
                action(arg1, arg2, arg3);
        }
        public static void InvokeToAction<T1, T2, T3, T4>(this Control control, Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (control == null || control.IsDisposed) return;

            if (control.InvokeRequired)
                control.Invoke(action, arg1, arg2, arg3, arg4);
            else
                action(arg1, arg2, arg3, arg4);
        }

        #endregion

        #region Control / Form

        public static void DoubleBuffered(this Control ctl, bool setting)
        {
            Type t = ctl.GetType();
            PropertyInfo p = t.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            p.SetValue(ctl, setting, null);
        }

        #endregion

        #region DataGridView

        /// <summary>
        /// 컬럼의 DataPropertyName을 기준으로 데이터를 세팅합니다.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="objData">serializable가능한 object ( ex: DataTable, List[HashTable], List[my_class], List[my_struct] ), null시 내용초기화</param>
        /// <param name="resetSelection">false:선택행을 유지한다. true:선택행을 초기화하며, 스크롤바가 제일 상단으로 이동합니다</param>
        public static void SetDataSource(this DataGridView grid, object dataSource, bool resetSelection)
        {
            List<Hashtable> map2 = null;

            try
            {
                map2 = JsonSerializer.Deserialize<List<Hashtable>>(JsonSerializer.Serialize(dataSource));
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Serialize 또는 Deserialize에 실패했습니다. msg: {0}", e.Message));
            }

            DataTable dt = new DataTable();
            for (int j = 0; j < grid.Columns.Count; j++)
            {
                dt.Columns.Add(grid.Columns[j].DataPropertyName);
            }
            if (map2 != null)
            {
                for (int i = 0; i < map2.Count; i++)
                {
                    DataRow _ravi = dt.NewRow();
                    for (int j = 0; j < grid.Columns.Count; j++)
                    {
                        _ravi[grid.Columns[j].DataPropertyName] = map2[i][grid.Columns[j].DataPropertyName];
                    }
                    dt.Rows.Add(_ravi);
                }
            }
            if (resetSelection)
            {
                grid.DataSource = dt;
                grid.ClearSelection();
            }
            else
            {
                int selCount = grid.SelectedRows.Count;
                int[] selidx = ControlCommon.CheckGridSelection(grid);
                grid.DataSource = dt;
                ControlCommon.SetGridSelection(grid, selidx);
                if (selCount == 0) grid.ClearSelection();
            }
        }

        #endregion
    }
}
