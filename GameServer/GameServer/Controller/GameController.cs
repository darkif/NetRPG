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
                                return ((int)ReturnCode.Success).ToString();
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("创建房间");
                    server.CreateRoom(client, taskid);
                    return ((int)ReturnCode.Success).ToString();
                }

                //虽然存在房间但不是同一个id
                Console.WriteLine("已有房间但人数已满或者没有改副本的房间,创建房间");
                server.CreateRoom(client, taskid);
                return ((int)ReturnCode.Success).ToString();
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
                client.Room.RemoveClient(client);
                return ((int)ReturnCode.Success).ToString();
            }
            catch(Exception e)
            {
                Console.WriteLine("取消加入多人战斗的时候出现异常:" + e);
            }
            return ((int)ReturnCode.Fail).ToString();
        }

    }
}
