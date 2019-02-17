using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer.Servers
{
    public enum RoomState
    {
        WaitingJion,
        Battle
    }

    class Room
    {
        private List<Client> roomClientList = new List<Client>();
        private RoomState state = RoomState.WaitingJion;
        private Server server;
        private int taskId;     //用于区别是否是去同一个副本

        public int TaskId { get => taskId; set => taskId = value; }
        internal List<Client> RoomClientList { get => roomClientList; set => roomClientList = value; }

        public Room(Server server){
            this.server = server;
        }

        //加入房间
        public void AddClient(Client client)
        {
            client.Room = this;
            RoomClientList.Add(client);
            if(RoomClientList.Count == 3)
            {
                state = RoomState.Battle;
                //发送倒计时和进入副本的消息
                StartTimer();
            }
        }

        //移除
        public void RemoveClient(Client client)
        {
            client.Room = null;
            RoomClientList.Remove(client);
            Console.WriteLine(RoomClientList.Count);
            if(RoomClientList.Count == 0)
            {
                //该房间从服务器移除
                server.RemoveRoom(this);
                return;
            }
            if(RoomClientList.Count < 3)
                state = RoomState.WaitingJion;
        }

        public bool isWaiting()
        {
            return state == RoomState.WaitingJion;
        }


        //广播消息
        public void BroadcastMessage(Client excludeClient, ActionCode actionCode, string data)
        {
            foreach (Client client in RoomClientList)
            {
                if (client != excludeClient)
                {
                    server.SendResponse(client, actionCode, data);
                }
            }
        }

        //开始游戏q前的计时
        public void StartTimer()
        {
            new Thread(RunTimer).Start();
        }

        private void RunTimer()
        {
            Thread.Sleep(1000);
            for (int i = 3; i > 0; --i)
            {
                //广播数据
                BroadcastMessage(null, ActionCode.ShowTimer, i.ToString());
                Thread.Sleep(1000);
            }
            //BroadcastMessage(null, ActionCode.StartMultiPlay, "r");
        }

    }
}
