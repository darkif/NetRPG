using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Model;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    class UserDAO
    {
        //登陆时验证用户账号密码
        public User VerifyUser(MySqlConnection conn,string username,string password)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from user where username=@un and password=@pwd",conn);
                cmd.Parameters.AddWithValue("un", username);
                cmd.Parameters.AddWithValue("pwd", password);
                reader = cmd.ExecuteReader();

                if (reader.Read())      //如果读取到数据
                {
                    int id = reader.GetInt32("id");
                    User user = new User(id, username, password);
                    return user;
                }
                else                    //不存在该数据
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("在验证账号密码的时候出现异常:" + e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return null;
        }


        //注册时验证是否已经存在该账号
        public bool IsExistName(MySqlConnection conn,string username)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from user where username=@un",conn);
                cmd.Parameters.AddWithValue("un", username);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)     //读取一行及以上相同的数据返回trues
                {
                    return true;        //存在同名
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("在注册验证账户是否已经存在的时候出现异常:" + e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return false;
        }


        //往数据库中添加用户
        public void AddUser(MySqlConnection conn,string username,string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into user set username=@un,password=@pwd",conn);
                cmd.Parameters.AddWithValue("un", username);
                cmd.Parameters.AddWithValue("pwd", password);
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Console.WriteLine("在插入新用户的时候出现异常:" + e);
            }
            
        }
    }
}
