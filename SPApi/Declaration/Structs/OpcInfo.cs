using System;
using OPCAutomation;

namespace SPApi.Declaration.Structs
{

    /// <summary>
    /// OPC GROUP 구조체
    /// </summary>
    public class OPC_GROUP
    {
        public event DIOPCGroupsEvent_GlobalDataChangeEventHandler OnDataChange;

        public int Index { get; set; }
        public string Name { get; set; }
        public int UpdateRate { get; set; }
        public bool IsSubscribed { get; set; }
        public bool IsActive { get; set; }
        public bool IsDataChange { get; set; }
        public OPC_ITEM[] Items { get; set; }
        public int[] ClientHandle { get; set; }
        public string[] IDs { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public void DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            LastUpdateDate = DateTime.Now;

            if (OnDataChange != null)
                OnDataChange(TransactionID, Index, NumItems, ref ClientHandles, ref ItemValues, ref Qualities, ref TimeStamps);
        }
    }

    /// <summary>
    /// OPC ITEM 구조체
    /// </summary>
    [Serializable]
    public struct OPC_ITEM
    {
        public int ClientHandle { get; set; }
        public string OPCItemId { get; set; }
        public string OPCItemName { get; set; }
        public string OPCItemValue { get; set; }
        public string OPCItemUnit { get; set; }
        public string OPCQuality { get; set; }
        public string TimeStamp { get; set; }
    }


}
