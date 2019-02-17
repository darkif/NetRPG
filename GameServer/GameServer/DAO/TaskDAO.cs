using GameServer.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameServer.DAO
{
    class TaskDAO
    {
        //根据roleid返回任务信息列表
        public List<GameServer.Model.Task> GetTaskByRoleId(MySqlConnection coon, int roleid)
        {
            List<GameServer.Model.Task> taskList = new List<GameServer.Model.Task>();
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from task where roleid=@id", coon);
                cmd.Parameters.AddWithValue("id", roleid);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    int taskid = reader.GetInt32("taskid");
                    int taskState = reader.GetInt32("taskState");
                    int taskType = reader.GetInt32("taskType");

                    DateTime dateTime = reader.GetDateTime("lastUpdateTime");
                    taskList.Add(new GameServer.Model.Task(id, taskid, (TaskState)taskState, (TaskType)taskType, dateTime));
                }

                return taskList;

            }
            catch (Exception e)
            {
                Console.WriteLine("在GetTaskByRoleId的时候出现异常:" + e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return null;
        }

        //更新或插入task
        public void UpdateOrAddTask(MySqlConnection conn,GameServer.Model.Task task,Role role)
        {
            try
            {
                MySqlCommand cmd = null;
                if (!IsExistTask(conn, task.TaskId,role.Id))
                {
                    cmd = new MySqlCommand("insert into task set taskId=@tid,taskState=@tState,taskType=@tType,lastUpdateTime=@updateTime,roleId=@rid", conn);
                }
                else
                {
                    cmd = new MySqlCommand("update task set taskState=@tState,taskType=@tType,lastUpdateTime=@updateTime,roleId=@rid where taskId=@tid", conn);
                }
                cmd.Parameters.AddWithValue("tid", task.TaskId);
                cmd.Parameters.AddWithValue("tState", task.TaskState);
                cmd.Parameters.AddWithValue("tType", task.TaskType);
                cmd.Parameters.AddWithValue("updateTime", task.LastUpdateTime);
                cmd.Parameters.AddWithValue("rid", role.Id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("在UpdateOrAddTask的时候出现异常:" + e);
            }
        }

        //验证是否已经有该任务
        public bool IsExistTask(MySqlConnection conn, int taskId,int roleId)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from task where taskid=@id and roleId=@rid", conn);
                cmd.Parameters.AddWithValue("id", taskId);
                cmd.Parameters.AddWithValue("rid", roleId);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)     //读取一行及以上相同的数据返回trues
                {
                    return true;        //存在
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在验证是否已经有该任务的时候出现异常:" + e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return false;
        }

    }
}
