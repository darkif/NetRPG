using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace GameServer.Servers
{
    class Message
    {
        private byte[] data = new byte[2048];
        private int startIndex = 0;//表示从哪里开始存数据

        public byte[] Data
        {
            get { return data; }
        }

        public int StartIndex
        {
            get { return startIndex; }
        }

        //剩余的空间
        public int RemainSize
        {
            get { return data.Length - startIndex; }
        }

        /// <summary>
        /// 读取数据（解析数据）
        /// </summary>
        public void ReadMessage(int dataAmount,Action<RequestCode,ActionCode,string> OnProcessMessage)
        {
            startIndex += dataAmount;
            while (true)
            {
                //小于4字节
                if (startIndex < 4)
                {
                    break;
                }

                //从数组里读取数据长度
                //数据前面4字节存储总数据的长度
                int count = BitConverter.ToInt32(data,0);

                //如果startIndex-4也就是已经存储的数据长度>=传送过来的数据
                //也就是传输完一条完整的数据后执行
                //解决粘包（数据太少，多个数据一起打包发送）、分包问题（数据太大，分成多组数据发送）
                if ((startIndex - 4) >= count)
                {
                    //解析数据
                    RequestCode requestCode = (RequestCode)BitConverter.ToInt32(data, 4);
                    ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8);

                    string s = Encoding.UTF8.GetString(data, 12, count - 8);
                    OnProcessMessage(requestCode, actionCode, s);
                    startIndex -= count + 4;
                }
                else
                {
                    break;
                }
            }

        }


        /// <summary>
        /// 打包要发送的数据
        /// </summary>
        public static byte[] PackData(ActionCode actionCode,string data)
        {
            Console.WriteLine("send messgae:" + data);
            byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            int dataAmount = actionCodeBytes.Length + dataBytes.Length;
            byte[] dataAmountbytes = BitConverter.GetBytes(dataAmount);
            //组装要传输的字节数组 
            return dataAmountbytes.Concat(actionCodeBytes).ToArray().Concat(dataBytes).ToArray();
        }

    }
}
