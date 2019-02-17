using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryType
{
    Equip,
    Drug
}

public enum EquipType
{
    Helm,
    Cloth,
    Weapon,
    Shoes,
    Necklace,
    Bracelet,
    Ring,
    Extra
}

public class Inventory{
    public int id;
    public string name;
    public string icon;
    public InventoryType inventoryType;
    public EquipType equipType;
    public int price = 0;  //出售价格
    public int hp = 0;     //增加的生命值
    public int atk = 0;    //增加的攻击力
    public int def = 0;    //增加的防御力
    public InfoType infoType;  //作用类型，表示作用在哪个属性上
    public int applyValue;     //作用值
    public string desc;

}
