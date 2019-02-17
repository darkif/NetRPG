using System;
using Common;
using System.Text;
using System.Linq;

public class Message
{
    private byte[] data = new byte[2048];
    private int startIndex = 0;//表示从data的什么位置开始存数据,即已经存储了多少数据

    public byte[] Date
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
    public void ReadMessage(int newDataAmount, Action<ActionCode, string> OnProcessMessage)
    {
        startIndex += newDataAmount;//更新索引，即已存数据
        //Debug.Log(startIndex);
        while (true)
        {
            //小于4字节;
            if (startIndex <= 4)
            {
                break;
            }

            //从数组里读取数据长度
            //count是表示传送数据的长度
            int count = BitConverter.ToInt32(data, 0);

            //前面4位是存的数据长度
            //如果startIndex-4也就是已经存储的数据长度>=传送过来的数据
            //也就是传输完一条完整的数据后执行
            //解决粘包（数据太少，多个数据一起打包发送）、分包问题（数据太大，分成多组数据发送）
            if ((startIndex - 4) >= count)
            {
                //string s = Encoding.UTF8.GetString(data, 4, count);
                //Console.WriteLine("解析出一条数据:" + s);

                //toInt32只解析4字节
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
                //读取数据
                string s = Encoding.UTF8.GetString(data, 8, count - 4);

                OnProcessMessage(actionCode, s);

                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex -= count + 4;
            }
            else
            {
                break;
            }
        }
    }

    //包装数据
    public static byte[] PackData(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
        byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataAmount = requestCodeBytes.Length + dataBytes.Length + actionCodeBytes.Length;
        //Debug.Log(dataAmount);
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        //组装要传输的字节数组
        return dataAmountBytes.Concat(requestCodeBytes).ToArray().Concat(actionCodeBytes).ToArray().Concat(dataBytes).ToArray();
    }
}
