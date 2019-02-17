using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class GameFacade : MonoBehaviour {

    private static GameFacade _instance;
    public static GameFacade Instance
    {
        get { return _instance; }
    }

    private ClientManager clientManager;
    private RequestManager requestManager;

    private RoleData roleData;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
        Init();
    }

    public void SetRoleData(RoleData roleData)
    {
        this.roleData = roleData;
    }

    public RoleData GetRoleData()
    {
        if (roleData != null)
            return roleData;
        else
            return null;
    }

    public void UpdateRoleData(string name, int level, int atk, int def, int exp, int coin, int hp,int maxHp)
    {
        roleData.Name = name;
        roleData.Level = level;
        roleData.Atk = atk;
        roleData.Def = def;
        roleData.Exp = exp;
        roleData.Coin = coin;
        roleData.Hp = hp;
        roleData.MaxHp = maxHp;
    }

    private void Init()
    {
        clientManager = new ClientManager(this);
        requestManager = new RequestManager(this);

        clientManager.OnInit();
        requestManager.OnInit();
    }

    private void OnDestroy()
    {
        clientManager.OnDestroy();
        requestManager.OnDestroy();
    }


    //把RequestCode、BaseRequest添加到requestManager的字典里
    public void AddRequest(ActionCode actionCode, BaseRequest baseRequest)
    {
        requestManager.AddRequest(actionCode, baseRequest);
    }

    //字典中移除
    public void RemoveRequest(ActionCode actionCode)
    {
        requestManager.RemoveRequest(actionCode);
    }

    //处理消息
    public void HandleReponse(ActionCode actionCode, string data)
    {
        requestManager.HandleReponse(actionCode, data);
    }

    //发送消息给服务器
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        clientManager.SendRequest(requestCode, actionCode, data);
    }

}
