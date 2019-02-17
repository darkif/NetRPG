using GameServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    class InventoryItemDBDAO
    {
        //获得物品列表
        public List<InventoryItemDB> GetInventoryItemDBsByRoleId(MySqlConnection conn,Role role)
        {
            List<InventoryItemDB> inventoryItemList = new List<InventoryItemDB>();
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from inventoryitemdb where roleid=@id", conn);
                cmd.Parameters.AddWithValue("id", role.Id);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InventoryItemDB inventoryItemDB = new InventoryItemDB();
                        inventoryItemDB.Id = reader.GetInt32("id");
                        inventoryItemDB.InventoryId = reader.GetInt32("inventoryid");
                        inventoryItemDB.Count = reader.GetInt32("count");
                        inventoryItemDB.IsDressed = reader.GetBoolean("isdressed");
                        inventoryItemDB.RoleId = reader.GetInt32("roleid");
                        inventoryItemList.Add(inventoryItemDB);
                    }
                    return inventoryItemList;
                }
                else
                {
                    return null;
                } 

            }
            catch (Exception e)
            {
                Console.WriteLine("在GetInventoryItemDBsByRoleId的时候出现异常:" + e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return null;
        }

        //添加或更新物品
        public void UpdateOrAddInventoryItemDB(MySqlConnection conn,InventoryItemDB itemDB,Role role)
        {
            try
            {
                MySqlCommand cmd = null;
                if (!IsExistInventoryItem(conn,itemDB.InventoryId, role.Id))
                {
                    cmd = new MySqlCommand("insert into inventoryitemdb set inventoryid=@inid,count=@cnt,isdressed=@isd,roleid=@rid", conn);
                }
                else
                {
                    cmd = new MySqlCommand("update inventoryitemdb set count=@cnt,isdressed=@isd where roleid=@rid and inventoryid=@inid", conn);
                }
                cmd.Parameters.AddWithValue("inid", itemDB.InventoryId);
                cmd.Parameters.AddWithValue("cnt", itemDB.Count);
                cmd.Parameters.AddWithValue("rid", role.Id);
                cmd.Parameters.AddWithValue("isd", itemDB.IsDressed);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("在UpdateOrAddInventoryItemDB的时候出现异常:" + e);
            }
        }

        //判断是否有该物品
        public bool IsExistInventoryItem(MySqlConnection conn, int inventoryId,int roleId)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from inventoryItemdb where roleId=@rid and inventoryid=@inid", conn);
                cmd.Parameters.AddWithValue("rid", roleId);
                cmd.Parameters.AddWithValue("inid", inventoryId);
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
                Console.WriteLine("在验证是否有该物品的时候出现异常:" + e);
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
