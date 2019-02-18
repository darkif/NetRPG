using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.DAO;
using GameServer.Model;
using GameServer.Servers;
using Common;

namespace GameServer.Controller
{
    class UserController : BaseController
    {
        private UserDAO userDAO = new UserDAO();
        private RoleDAO roleDAO = new RoleDAO();
        private TaskDAO taskDAO = new TaskDAO();
        private InventoryItemDBDAO inventoryItemDBDAO = new InventoryItemDBDAO();
        private SkillDBDAO skillDBDAO = new SkillDBDAO();

        public UserController()
        {
            requestCode = RequestCode.User;
        }

        //处理登陆请求
        public string Login(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');    ////用,分割账户密码
            User user = userDAO.VerifyUser(client.MySqlConn, strs[0], strs[1]);

            if (user == null)   //不存在该账户密码
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                Console.WriteLine("验证成功");
                Role role = roleDAO.GetRoleByUserId(client.MySqlConn, user.Id);
                client.SetUserDate(user, role);
                return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", ((int)ReturnCode.Success).ToString(),role.Id ,role.Name, role.Level, role.RoleId, role.Atk, role.Def, role.Coin, role.Hp, role.Exp, role.MaxHp);
            }
        }


        //处理注册请求
        public string Register(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            string username = strs[0];
            string password = strs[1];

            bool isExist = userDAO.IsExistName(client.MySqlConn, username);
            if (isExist)    //存在同名
            {
                return ((int)ReturnCode.Fail).ToString();
            }

            //不存在同名 更新数据库
            userDAO.AddUser(client.MySqlConn, username, password);
            Console.WriteLine("注册成功");
            return ((int)ReturnCode.Success).ToString();
        }

        //更新角色信息
        public string UpdateRoleInfo(string data, Client client, Server server)
        {

            string[] strs = data.Split(',');
            Console.WriteLine(strs.Length);
            client.Role.Name = strs[0];
            client.Role.Level = int.Parse(strs[1]);
            client.Role.RoleId = int.Parse(strs[2]);
            client.Role.Exp = int.Parse(strs[3]);
            client.Role.Coin = int.Parse(strs[4]);
            client.Role.Atk = int.Parse(strs[5]);
            client.Role.Def = int.Parse(strs[6]);
            client.Role.Hp = int.Parse(strs[7]);
            client.Role.MaxHp = int.Parse(strs[8]);
            roleDAO.UpdateOrAddRole(client.MySqlConn, client.Role);
            return string.Format("{0},{1}", ((int)ReturnCode.Success).ToString(), client.Role.Id); 
        }


        //获取任务
        public string GetTask(string data, Client client, Server server)
        {
            List<GameServer.Model.Task> taskList = taskDAO.GetTaskByRoleId(client.MySqlConn, client.Role.Id);
            if (taskList != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (GameServer.Model.Task task in taskList)
                {
                    string tempData = task.TaskId.ToString() + "," + (int)task.TaskState + "," + (int)task.TaskType + "-";
                    sb.Append(tempData);
                }
                return string.Format("{0}|{1}", ((int)ReturnCode.Success).ToString(), sb.ToString());

            }
            else
            {
                return ((int)ReturnCode.Fail).ToString();
            }
        }

        //更新或添加任务
        public string UpdateOrAddTask(string data, Client client, Server server)
        {
            string[] proArray = data.Split(',');
            int taskId = int.Parse(proArray[0]);
            int taskState = int.Parse(proArray[1]);
            int taskType = int.Parse(proArray[2]);
            DateTime dateTime = DateTime.Parse(proArray[3]);
            GameServer.Model.Task task = new GameServer.Model.Task(taskId, (TaskState)taskState, (TaskType)taskType, dateTime);
            taskDAO.UpdateOrAddTask(client.MySqlConn, task, client.Role);

            return string.Format("{0},{1},{2}", ((int)ReturnCode.Success).ToString(), taskId, taskState); 
        }

        public string UpdatePlayerInfo(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            client.Role.Name = strs[0];
            client.Role.Level = int.Parse(strs[1]);
            client.Role.RoleId = int.Parse(strs[2]);
            client.Role.Exp = int.Parse(strs[3]);
            client.Role.Coin = int.Parse(strs[4]);
            client.Role.Atk = int.Parse(strs[5]);
            client.Role.Def = int.Parse(strs[6]);
            client.Role.Hp = int.Parse(strs[7]);
            client.Role.MaxHp = int.Parse(strs[8]);
            roleDAO.UpdateRole(client.MySqlConn, client.Role);
            return ((int)ReturnCode.Success).ToString();
        }

        //获取背包物品
        public string GetInventoryItemDBs(string data, Client client, Server server)
        {
            List<InventoryItemDB> inventoryItemDBList = inventoryItemDBDAO.GetInventoryItemDBsByRoleId(client.MySqlConn, client.Role);
            if (inventoryItemDBList != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (InventoryItemDB inventoryItem in inventoryItemDBList)
                {
                    string tempData = inventoryItem.InventoryId.ToString() + "," + inventoryItem.Count.ToString() + "," + inventoryItem.IsDressed.ToString() + "-";
                    sb.Append(tempData);
                }
                return string.Format("{0}|{1}", ((int)ReturnCode.Success).ToString(), sb.ToString());

            }
            else
            {
                return ((int)ReturnCode.Fail).ToString();
            }
        }

        //更新背包
        public string UpdateOrAddInventoryItemDB(string data, Client client, Server server)
        {
            string[] proArray = data.Split(',');
            InventoryItemDB itemDB = new InventoryItemDB();
            itemDB.InventoryId = int.Parse(proArray[0]);
            itemDB.Count = int.Parse(proArray[1]);
            itemDB.IsDressed = bool.Parse(proArray[2]);
            inventoryItemDBDAO.UpdateOrAddInventoryItemDB(client.MySqlConn, itemDB, client.Role);

            return ((int)ReturnCode.Success).ToString();
        }

        //穿卸装备
        public string ChangeEquip(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            //Console.WriteLine(strs.Length);
            if (strs.Length == 4)
            {
                InventoryItemDB itemDB = new InventoryItemDB();
                itemDB.InventoryId = int.Parse(strs[0]);
                itemDB.Count = int.Parse(strs[1]);
                itemDB.IsDressed = bool.Parse(strs[2]);
                inventoryItemDBDAO.UpdateOrAddInventoryItemDB(client.MySqlConn, itemDB, client.Role);
            }
            else if (strs.Length == 7)
            {
                InventoryItemDB itemDB1 = new InventoryItemDB();
                itemDB1.InventoryId = int.Parse(strs[0]);
                itemDB1.Count = int.Parse(strs[1]);
                itemDB1.IsDressed = bool.Parse(strs[2]);
                InventoryItemDB itemDB2 = new InventoryItemDB();
                itemDB2.InventoryId = int.Parse(strs[3]);
                itemDB2.Count = int.Parse(strs[4]);
                itemDB2.IsDressed = bool.Parse(strs[5]);
                inventoryItemDBDAO.UpdateOrAddInventoryItemDB(client.MySqlConn, itemDB1, client.Role);
                inventoryItemDBDAO.UpdateOrAddInventoryItemDB(client.MySqlConn, itemDB2, client.Role);
            }

            return ((int)ReturnCode.Success).ToString();
        }


        //获取技能信息
        public string GetSkill(string data, Client client, Server server)
        {

            List<SkillDB> skillList = skillDBDAO.GetSkillListByRoleId(client.MySqlConn, client.Role);
            if (skillList != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (SkillDB skill in skillList)
                {
                    string temp = skill.SkillId + "," + skill.Level + ","+skill.Damage+"-";
                    sb.Append(temp);
                }
                return string.Format("{0}|{1}", ((int)ReturnCode.Success).ToString(), sb.ToString());
            }
            else
            {
                return ((int)ReturnCode.Fail).ToString();
            }
        }

        public string UpgradeSkill(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            SkillDB skillDB = new SkillDB();
            skillDB.SkillId = int.Parse(strs[0]);
            skillDB.Level = int.Parse(strs[1]);
            skillDB.Damage = int.Parse(strs[2]);
            skillDB.RoleId = client.Role.Id;
            if (skillDBDAO.isExistSkillDB(client.MySqlConn, skillDB.SkillId, client.Role.Id))
            {
                skillDBDAO.UpdateSkillDB(client.MySqlConn, client.Role, skillDB);
            }
            else
            {
                skillDBDAO.AddSkill(client.MySqlConn, skillDB);
            }


            return ((int)ReturnCode.Success).ToString();
        }

        public string AddSkill(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            SkillDB skillDB = new SkillDB();
            skillDB.SkillId = int.Parse(strs[0]);
            skillDB.Level = int.Parse(strs[1]);
            skillDB.Damage = int.Parse(strs[2]);
            skillDB.RoleId = client.Role.Id;
            skillDBDAO.AddSkill(client.MySqlConn, skillDB);

            return ((int)ReturnCode.Success).ToString();
        }

        //卖出东西
        public string SellInventoryItem(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            InventoryItemDB itemDB = new InventoryItemDB();
            itemDB.InventoryId = int.Parse(strs[0]);
            itemDB.Count = int.Parse(strs[1]);
            itemDB.IsDressed = bool.Parse(strs[2]);
            inventoryItemDBDAO.UpdateOrAddInventoryItemDB(client.MySqlConn, itemDB, client.Role);

            return ((int)ReturnCode.Success).ToString();
        }
    }
}
