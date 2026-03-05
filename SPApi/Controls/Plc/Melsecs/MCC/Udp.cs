using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPApi.Controls.Plc.Melsecs.MCC
{
    public class Udp:iMCC
    {
        public bool IsRun
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler<McpEventArgs> DataReceived;
        public event EventHandler<McpEventArgs> DataSent;
        public event EventHandler<McpEventArgs> ErrorReceived;

        public void Start()
        {

        }

        public void Stop()
        {

        }

        public void Send(string sdata)
        {

        }
        public void Setting(McSetting set)
        {
        }
    }
}
