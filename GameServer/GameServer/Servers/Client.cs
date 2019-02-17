using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common;
using MySql.Data.MySqlClient;
using GameServer.Tool;
using GameServer.Model;

namespace GameServer.Servers
{
    class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message msg = new Message();

        private MySqlConnection mySqlConn;
        public MySqlConnection MySqlConn
        {
            get { return mySqlConn; }
        }

        private User user;
        private Role role;

        public void SetUserDate(User user,Role role)
        {
            this.user = user;
            this.role = role;
        }

        public User User
        {
            get { return user; }
        }
        public Role Role
        {
            get { return role; }
        }

        private Room room = null;
        public Room Room
        {
            get;set;
        }

        public Client() { }

        public Client(Socket clientSocket,Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
            //连接数据库
            mySqlConn = ConnHelper.Connect();
        }

        //初始化客户端
        public void Start()
        {
            //判断客户端是否被关闭
            if (clientSocket.Connected == false || clientSocket.Poll(10, SelectMode.SelectRead))
            {
                Close();
                return;
            }

            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
        }


        //接收消息后回调函数
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int len = clientSocket.EndReceive(ar);
                if (len == 0)
                {
                    //断开连接
                    Close();
                }
                //解析处理消息
                msg.ReadMessage(len, OnProcessMessage);
                //继续监听消息的接收
                Start();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
        }

        //处理解析好数据
        private void OnProcessMessage(RequestCode requestCode,ActionCode actionCode,string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }


        //发送数据到客户端
        public void Send(ActionCode actionCode, string data)
        {
            try
            {
                //判断客户端是否被关闭
                if (clientSocket.Connected == false || clientSocket.Poll(10, SelectMode.SelectRead))
                {
                    Close();
                    return;
                }

                byte[] bytes = Message.PackData(actionCode, data);
                clientSocket.Send(bytes);
            }
            catch(Exception e)
            {
                Console.WriteLine("无法发送消息：" + e);
            }
        }


        //断开连接
        private void Close()
        {
            if (clientSocket != null)
            {
                clientSocket.Close();
                if (room != null)
                {
                    room.RemoveClient(this);
                    room = null;
                }
                
            }
        }

    }
}
