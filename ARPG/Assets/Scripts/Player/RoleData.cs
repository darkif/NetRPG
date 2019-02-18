using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleData{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int RoleId { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Exp { get; set; }
    public int Coin { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }

    public RoleData( int id,string name, int level, int roldId,int atk,int def,int coin,int hp,int exp,int maxHp)
    {
        Id = id;
        Name = name;
        Level = level;
        RoleId = roldId;
        Atk = atk;
        Def = def;
        Coin = coin;
        Hp = hp;
        Exp = exp;
        MaxHp = maxHp;
    }

}
