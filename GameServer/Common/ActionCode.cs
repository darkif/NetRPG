using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public enum ActionCode
    {
        None,
        Login,              //登陆
        Register,           //注册
        UpdateRoleInfo,     //更新角色信息
        GetTask,
        UpdateOrAddTask,
        UpdatePlayerInfo,
        GetInventoryItemDBs, //获取背包物品
        UpdateOrAddInventoryItemDB,      //添加物品
        ChangeEquip,             //穿上或脱下装备
        AddSkill,                //添加技能
        GetSkill,                //获得技能信息
        UpgradeSkill ,           //技能升级
        SellInventoryItem,        //卖背包里的东西
        AddMultiPlay,              //多人游戏
        CancelAddMultiPlay,          //取消多人游戏
        StartMultiPlay,              //开始游戏
        StartPlay,
        ShowTimer
    }
}
