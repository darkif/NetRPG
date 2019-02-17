using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Model;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    class SkillDBDAO
    {
        //获得拥有技能列表 
        public List<SkillDB> GetSkillListByRoleId(MySqlConnection conn,Role role)
        {
            MySqlDataReader reader = null;
            List<SkillDB> skillList = new List<SkillDB>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("select* from skilldb where roleid=@rid", conn);
                cmd.Parameters.AddWithValue("rid", role.Id);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SkillDB skillDB = new SkillDB();
                        skillDB.SkillId = reader.GetInt32("skillid");
                        skillDB.Level = reader.GetInt32("level");
                        skillDB.RoleId = role.Id;
                        skillList.Add(skillDB);
                    }
                    return skillList;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception e)
            {
                Console.WriteLine("在GetSkillListByRoleId的时候出现异常:" + e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return null;
        }

        //更新技能信息 升级 
        public void UpdateSkillDB(MySqlConnection conn, Role role,SkillDB skillDB){
            try
            {
                MySqlCommand cmd = new MySqlCommand("update from skilldb set level=@ll where roleid=@rid", conn);
                cmd.Parameters.AddWithValue("ll", skillDB.Level);
                cmd.Parameters.AddWithValue("rid", role.Id);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine("在UpdateSkillDB的时候出现异常:" + e);
            }
        }

        //添加技能到数据库
        public void AddSkill(MySqlConnection conn,SkillDB skillDB)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into skilldb set skillid=@sid,level=@ll, roleid=@rid", conn);
                cmd.Parameters.AddWithValue("sid", skillDB.SkillId);
                cmd.Parameters.AddWithValue("ll", skillDB.Level);
                cmd.Parameters.AddWithValue("rid", skillDB.RoleId);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine("在AddSkill的时候出现异常:" + e);
            }
        }


        public bool isExistSkillDB(MySqlConnection conn,int skillId,int roleid)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from skilldb where skillid=@id and roleId=@rid", conn);
                cmd.Parameters.AddWithValue("id", skillId);
                cmd.Parameters.AddWithValue("rid", roleid);
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
                Console.WriteLine("在验证是否已经有该技能的时候出现异常:" + e);
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
