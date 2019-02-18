using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    class GameController:BaseController
    {
        public GameController()
        {
            requestCode = RequestCode.Game;
        }

        //加入多人战斗
        public string AddMultiPlay(string data, Client client, Server server)
        {
            try
            {
                int taskid = int.Parse(data);//要传入任务id
                List<Room> roomList = server.GetRoomList();
                if (roomList.Count > 0)
                {
                    foreach (Room room in roomList)
                    {
                        if (room.TaskId == taskid)
                        {
                            //只有人数小于3的时候可以进入
                            if (room.RoomClientList.Count < 3)
                            {
                                Console.WriteLine("加入房间");
                                room.AddClient(client);
                                return string.Format("{0},{1}", ((int)ReturnCode.Success).ToString(), room.RoomClientList.Count.ToString()); 
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("创建房间");
                    server.CreateRoom(client, taskid);
                    return string.Format("{0},{1}", ((int)ReturnCode.Success).ToString(), client.Room.RoomClientList.Count);
                }

                //虽然存在房间但不是同一个id
                Console.WriteLine("已有房间但人数已满或者没有改副本的房间,创建房间");
                server.CreateRoom(client, taskid);
                return string.Format("{0},{1}", ((int)ReturnCode.Success).ToString(), client.Room.RoomClientList.Count);
            }           
            catch(Exception e)
            {
                Console.WriteLine("加入多人战斗的时候出现异常:" + e);
            }
            return ((int)ReturnCode.Fail).ToString();
        }

        //取消多人战斗
        public string CancelAddMultiPlay(string data, Client client, Server server)
        {
            try
            {
                bool isHost = client.Room.isHouseOwner(client);
                if (isHost)
                {
                    client.Room.BroadcastMessage(client, ActionCode.CancelAddMultiPlay, ((int)ReturnCode.Success).ToString());
                }
                client.Room.QuitRoom(client);
                return ((int)ReturnCode.Success).ToString();
            }
            catch(Exception e)
            {
                Console.WriteLine("取消加入多人战斗的时候出现异常:" + e);
            }
            return ((int)ReturnCode.Fail).ToString();
        }


        //同步玩家位置和旋转
        public string SyncPosAndRotation(string data, Client client, Server server)
        {
            if (client.Room != null)
            {
                client.Room.BroadcastMessage(client, ActionCode.SyncPosAndRotation, data);
            }

            return null;
        }

        public string SyncBossTranform(string data, Client client, Server server)
        {
            if (client.Room != null)
            {
                client.Room.BroadcastMessage(client, ActionCode.SyncBossTranform, data);
            }

            return null;
        }
    }
}
