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
            int taskid = int.Parse(data);//要传入任务id
            List<Room> roomList = server.GetRoomList();
            if (roomList.Count > 0)
            {
                foreach(Room room in roomList)
                {
                    if (room.TaskId == taskid)
                    {
                        //只有人数小于3的时候可以进入
                        if (room.RoomClientList.Count < 3)
                        {
                            room.AddClient(client);
                            return ((int)ReturnCode.Success).ToString();
                        }
                    }
                }
            }
            else
            {
                server.CreateRoom(client, taskid);
                return ((int)ReturnCode.Success).ToString();
            }

            //虽然存在房间但不是同一个id
            server.CreateRoom(client, taskid);
            return ((int)ReturnCode.Success).ToString();

        }

        //取消多人战斗
        public string CancelAddMultiPlay(string data, Client client, Server server)
        {
            client.Room.RemoveClient(client);
            return ((int)ReturnCode.Success).ToString();
        }

    }
}
