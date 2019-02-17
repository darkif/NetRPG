using GameServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    class RoleDAO
    {
        //根据userid返回角色信息
        public Role GetRoleById(MySqlConnection coon,int userid)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from role where userid=@id", coon);
                cmd.Parameters.AddWithValue("id", userid);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string name = reader.GetString("name");
                    int level = reader.GetInt32("level");
                    int roldId = reader.GetInt32("roleId");
                    int atk = reader.GetInt32("atk");
                    int def = reader.GetInt32("def");
                    int coin = reader.GetInt32("coin");
                    int exp = reader.GetInt32("exp");
                    int hp = reader.GetInt32("hp");
                    int maxHp = reader.GetInt32("maxhp");

                    Role role = new Role(id, name, level, roldId, userid, atk, def, exp, coin, hp, maxHp); ;
                    return role;
                }
                else
                {
                    return new Role(-1, "", 1, -1, userid,10,5,0,1000,100,100);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("在GetRoleById的时候出现异常:" + e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return null;
        }

        //更新或插入角色信息
        public void UpdateOrAddResult(MySqlConnection conn,Role role)
        {
            try
            {
                MySqlCommand cmd = null;
                if (role.Id <= -1)
                {
                    cmd = new MySqlCommand("insert into role set name=@nm,level=@ll,roleId=@rid,userid=@uid,exp=@ep,coin=@cn,atk=@ak,def=@df,hp=@h",conn);
                }
                else
                {
                    cmd = new MySqlCommand("update role set name=@nm,level=@ll,roleId=@rid,exp=@ep,coin=@cn,atk=@ak,def=@df,hp=@h where userid=@uid", conn);
                }
                cmd.Parameters.AddWithValue("nm", role.Name);
                cmd.Parameters.AddWithValue("ll", role.Level);
                cmd.Parameters.AddWithValue("rid", role.RoleId);
                cmd.Parameters.AddWithValue("uid", role.UserId);
                cmd.Parameters.AddWithValue("ep", role.Exp);
                cmd.Parameters.AddWithValue("cn", role.Coin);
                cmd.Parameters.AddWithValue("ak", role.Atk);
                cmd.Parameters.AddWithValue("df", role.Def);
                cmd.Parameters.AddWithValue("h", role.Hp);
                cmd.ExecuteNonQuery();

                if (role.Id <= -1)
                {
                    Role tempRole = GetRoleById(conn, role.Id);
                    role.Id = tempRole.Id;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("在UpdateOrAddResult的时候出现异常:" + e);
            }
        }


        //更新角色信息
        public void UpdateRole(MySqlConnection conn, Role role)
        {
            try
            {
                MySqlCommand cmd = null;
                cmd = new MySqlCommand("update role set name=@nm,level=@ll,roleId=@rid,exp=@ep,coin=@cn,atk=@ak,def=@df,hp=@h,maxhp=@mhp where userid=@uid", conn);
                cmd.Parameters.AddWithValue("nm", role.Name);
                cmd.Parameters.AddWithValue("ll", role.Level);
                cmd.Parameters.AddWithValue("rid", role.RoleId);
                cmd.Parameters.AddWithValue("uid", role.UserId);
                cmd.Parameters.AddWithValue("ep", role.Exp);
                cmd.Parameters.AddWithValue("cn", role.Coin);
                cmd.Parameters.AddWithValue("ak", role.Atk);
                cmd.Parameters.AddWithValue("df", role.Def);
                cmd.Parameters.AddWithValue("h", role.Hp);
                cmd.Parameters.AddWithValue("mhp", role.MaxHp);
                cmd.ExecuteNonQuery();

                if (role.Id <= -1)
                {
                    Role tempRole = GetRoleById(conn, role.Id);
                    role.Id = tempRole.Id;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在UpdateOrAddResult的时候出现异常:" + e);
            }
        }


    }
}
