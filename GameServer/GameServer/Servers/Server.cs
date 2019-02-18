using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Common;
using GameServer.Controller;

namespace GameServer.Servers
{
    class Server
    {
        private Socket serverSocket;
        private string ip;
        private int port;
        private EndPoint endPoint;

        private List<Client> clientList = new List<Client>();

        private ControllerManager controllerManager;

        private List<Room> roomList = new List<Room>();

        public Server() { }
        public Server(string ip,int port) {
            this.ip = ip;
            this.port = port;
            controllerManager = new ControllerManager(this);

            Timer timer = new Timer(1000);
            /*timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);*/
        }

        /*void Timer_Elapsed(object sender,ElapsedEventArgs e)
        {
            if (clientList.Count > 0)
            {
                foreach(Client client in clientList)
                {
                    if (client.isConnected())
                    {
                        Console.WriteLine(1);
                        client.Close();
                        continue;
                    }
                    client.Send(ActionCode.None, "test connect");
                }
            }
        }*/

        public void RemoveClient(Client client)
        {
            clientList.Remove(client);
        }

        //初始化服务器
        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(0);
            Console.WriteLine("开始监听");
            Console.WriteLine("等待客户端连接");
            serverSocket.BeginAccept(AcceptCallBack, null);
        }

        //接收客户端后回调
        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);
            Console.WriteLine("一个客户端连接");
            Client client = new Client(clientSocket,this);
            clientList.Add(client);
            //开始接收从客户端传来的消息
            client.Start(); 

            //继续接收客户端连接
            serverSocket.BeginAccept(AcceptCallBack, null);
        }


        //处理从客户端发来的请求
        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data,Client client)
        {
            controllerManager.HandleRequest(requestCode, actionCode, data, client);
        }


        //给客户端响应
        public void SendResponse(Client client, ActionCode actionCode, string data)
        {
            //把客户端需要的数据返回给客户端
            client.Send(actionCode, data);
        }


        //创建房间
        public void CreateRoom(Client client,int id)
        {
            Room room = new Room(this);
            room.TaskId = id;
            room.AddClient(client);
            roomList.Add(room);
        }

        //销毁房间 
        public void RemoveRoom(Room room)
        {
            if (roomList != null && room != null)
            {
                roomList.Remove(room);
            }
        }

        //获得房间列表
        public List<Room> GetRoomList()
        {
            return roomList;
        }

    }
}
