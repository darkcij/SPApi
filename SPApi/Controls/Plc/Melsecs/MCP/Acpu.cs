using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPApi.Commons;

namespace SPApi.Controls.Plc.Melsecs.MCP
{
    class Acpu: iMCP
    {
        //요청 데이터 생성
        public string MakeRequestData(CommObject obj)
        {
            //05 30 30 46 46 [ WR ] (XX XX 체크섬) 
            //06 30 30 46 46 정상
            //15 30 30 46 46 이상

            /*
            <05>00FFWR3D00003231
            <05>00FFWR3D00503236
            <05>00FFWR3D01003232
            <05>00FFWR3D01503237
            <05>00FFWR3B0000202C
            <05>00FFWR3B0200102D
            */

            /* BIT 전송
            <05>00FFBW0B000001149
            <05>00FFBW0B000001048
            */

            /*  Word 전송
            <05>00FFWW0D010101000B03
            */

            string command = GetCommand(obj.Command);
            string fdevice = obj.DeviceType;

            if (command.Left(1).Equals("B"))
                fdevice += obj.DeviceNo.ToString("X").PadLeft(4, '0');
            else
                fdevice += obj.DeviceNo.ToString().PadLeft(4, '0');

            StringBuilder tmp = new StringBuilder();
            tmp.Append(ASCII_SIGNAL.ENQ);                   //ENQ 0x05
            tmp.Append("00");                               //국번호
            tmp.Append("FF");                               //PLC번호
            tmp.Append(command);                            //커맨드
            tmp.Append(obj.WaitTime);                       //전문대기
            tmp.Append(fdevice);                            //시작 디바이스
            tmp.Append(obj.DeviceCount.ToString("X").PadLeft(2,'0'));      //요청 갯수(Hex)

            if (obj.Command == COMMAND_TYPE.Bit_Write ||
                obj.Command == COMMAND_TYPE.Word_Write)
                tmp.Append(GetData(obj.Command, obj.Values));   //전송 데이터

            tmp.Append(GetCheckSum(tmp.ToString(1, tmp.Length - 1)));

            return tmp.ToString();
        }

        //회신 데이터 생성
        public string MakeResponseData(CommObject obj)
        {
            //02 30 30 46 46 ~ 03 (XX XX 체크섬)
            //15 30 30 46 46 (30 35 에러코드)

            StringBuilder tmp = new StringBuilder();
            tmp.Append("00");
            tmp.Append("FF");

            switch (obj.State)
            {
                case 2:     //통신 정상 
                    tmp.Insert(0, ASCII_SIGNAL.STX);
                    tmp.Append(GetData(obj.Command, obj.Values));   //전송 데이터
                    tmp.Append(ASCII_SIGNAL.ETX);
                    tmp.Append(GetCheckSum(tmp.ToString(1, tmp.Length - 1)));
                    break;

                case 6:     //명령 성공
                    tmp.Insert(0, ASCII_SIGNAL.ACK);
                    break;

                case 15:    //통신 이상, 명령 실패
                    tmp.Insert(0, ASCII_SIGNAL.NAK);

                    //응답전문은 에러코드를 포함
                    if (obj.Command == COMMAND_TYPE.Bit_Read ||
                        obj.Command == COMMAND_TYPE.Word_Read)
                        tmp.Append("05");
                    break;
            }

            return tmp.ToString();
        }

        //수신 데이터 파싱
        public bool RecvParseRequestData(string message, out CommObject obj)
        {
            bool state = false;
            obj = new CommObject();

            //데이터체크
            if (message.Left(1) == ASCII_SIGNAL.ACK && message.Length > 5)
            {
                int len = message.IndexOf(ASCII_SIGNAL.ENQ);
                message = message.Mid(len, message.Length - len);
            }

            //타입확인
            if (message.Left(1) != ASCII_SIGNAL.ENQ)
                return state;

            //섬체크
            string checksum = GetCheckSum(message.Mid(1, message.Length - 3));
            if (message.Right(2) != checksum)
                return state;

            //커맨드 확인
            obj.Command = GetCommand(message.Mid(5, 2));

            //디바이스 
            string dev = string.Empty;
            int devno = 0;

            GetDevice(message.Mid(8, 5), out dev, out devno);

            //디바이스 타입 확인
            obj.DeviceType = dev;

            //디바이스 번호 확인
            obj.DeviceNo = devno;

            //디바이스 점수
            obj.DeviceCount = message.Mid(13, 2).ToInt(16);

            if (obj.Command == COMMAND_TYPE.Bit_Write)
            {
                obj.Values = new string[obj.DeviceCount];

                for (int i = 0; i < obj.DeviceCount; i++)
                    obj.Values[i] = message.Mid(15 + i, 1);

            }
            else if (obj.Command == COMMAND_TYPE.Word_Write)
            {
                obj.Values = new string[obj.DeviceCount];

                for (int i = 0; i < obj.DeviceCount; i++)
                    obj.Values[i] = message.Mid(15 + (i * 4), 4).ToInt(16).ToString();
            }

            state = true;

            return state;
        }

        public bool RecvParseResponseData(string message, ref CommObject obj)
        {
            bool state = false;

            //데이터체크
            if (message.Left(1) == ASCII_SIGNAL.ACK && message.Length > 5)
            {
                int len = message.IndexOf(ASCII_SIGNAL.STX);
                message = message.Mid(len, message.Length - len);
            }

            //STX 확인
            if (message.Left(1) != ASCII_SIGNAL.STX)
                return state;

            //섬체크
            string checksum = GetCheckSum(message.Mid(1, message.Length - 3));
            if (message.Right(2) != checksum)
                return state;

            obj.Values = new string[obj.DeviceCount];

            for (int i=0; i< obj.DeviceCount; i++)
            {
                obj.Values[i] = message.Mid(5 + (i * 4), 4);
            }

            state = true;

            return state;
        }

        private string GetCheckSum(string data)
        {
            byte[] tmp = null;
            Int16 chksum = 0;

            tmp = Encoding.ASCII.GetBytes(data);

            for (int i = 0; i < tmp.Length; i++)
                chksum += tmp[i];

            return chksum.ToString("X").PadLeft(4, '0').Right(2);
        }

        private string GetData(COMMAND_TYPE t, string[] values)
        {
            StringBuilder tmp = new StringBuilder();

            switch (t)
            {
                case COMMAND_TYPE.Bit_Read:
                case COMMAND_TYPE.Bit_Write:
                case COMMAND_TYPE.Bit_Test:
                    for (int i = 0; i < values.Length; i++)
                        tmp.Append(values[i].ToString());
                    break;

                case COMMAND_TYPE.Word_Read:
                case COMMAND_TYPE.Word_Write:
                case COMMAND_TYPE.Word_Test:
                    for (int i = 0; i < values.Length; i++)
                        tmp.Append(values[i].ToInt().ToString("X").PadLeft(4, '0'));
                    break;
            }

            return tmp.ToString();
        }

        private string GetDevice(COMMAND_TYPE t, int num)
        {
            StringBuilder tmp = new StringBuilder();

            switch (t)
            {
                case COMMAND_TYPE.Bit_Read:
                case COMMAND_TYPE.Bit_Write:
                case COMMAND_TYPE.Bit_Test: tmp.Append("B"); break;

                case COMMAND_TYPE.Word_Read:
                case COMMAND_TYPE.Word_Write:
                case COMMAND_TYPE.Word_Test: tmp.Append("D"); break;
            }

            return tmp.Append(num.ToString("0000")).ToString();
        }

        private void GetDevice(string device, out string dtype, out int dno)
        {
            int tmp = 0;

            dtype = device.Left(1);

            if (dtype.Equals("B"))
                tmp = device.Mid(1, device.Length - 1).ToInt<string>(16);
            else if (dtype.Equals("D"))
                tmp = device.Mid(1, device.Length - 1).ToInt<string>();

            dno = tmp;
        }

        /// <summary>
        /// 커맨드 가져오기
        /// </summary>
        /// <returns></returns>
        private string GetCommand(COMMAND_TYPE t)
        {
            string tmp = string.Empty;

            switch (t)
            {
                case COMMAND_TYPE.Bit_Read: tmp = "BR"; break;
                case COMMAND_TYPE.Bit_Write: tmp = "BW"; break;
                case COMMAND_TYPE.Bit_Test: tmp = "BT"; break;
                case COMMAND_TYPE.Word_Read: tmp = "WR"; break;
                case COMMAND_TYPE.Word_Write: tmp = "WW"; break;
                case COMMAND_TYPE.Word_Test: tmp = "WT"; break;
            }

            return tmp;
        }

        /// <summary>
        /// 커맨드 설정하기
        /// </summary>
        /// <returns></returns>
        private COMMAND_TYPE GetCommand(string t)
        {
            COMMAND_TYPE tmp = COMMAND_TYPE.Bit_Read;

            switch (t)
            {
                case "BR": tmp = COMMAND_TYPE.Bit_Read; break;
                case "BW": tmp = COMMAND_TYPE.Bit_Write; break;
                case "BT": tmp = COMMAND_TYPE.Bit_Test; break;
                case "WR": tmp = COMMAND_TYPE.Word_Read; break;
                case "WW": tmp = COMMAND_TYPE.Word_Write; break;
                case "WT": tmp = COMMAND_TYPE.Word_Test; break;
            }

            return tmp;
        }

        
    }
}
