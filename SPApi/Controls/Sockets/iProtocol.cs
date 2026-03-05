using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPApi.Controls.Sockets
{
    public interface iProtocol
    {
        string MakeSendData(SockDataObject data);

        bool CheckReceiveData(string recv);

        SockDataObject ReceiveDataProcess(string recv);
    }
}
