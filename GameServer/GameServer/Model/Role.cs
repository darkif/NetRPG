using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    public class Role
    {
        public Role(int id,string name,int level,int roldId,int userId,int atk,int def,int exp,int coin,int hp,int maxHp)
        {
            Id = id;
            Name = name;
            Level = level;
            RoleId = roldId;
            UserId = userId;
            Atk = atk;
            Def = def;
            Exp = exp;
            Coin = coin;
            Hp = hp;
            MaxHp = maxHp;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Exp { get; set; }
        public int Coin { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
    }
}
