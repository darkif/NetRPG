using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

public class SyncPlayerAnimRequest : BaseRequest {

    private bool isSync = false;
    private Animator anim;
    private string triggerName = "";
    private int playerid = -1;

    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.SyncPlayerAnim;
        base.Awake();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isSync)
        {
            isSync = false;
            OnSyncAnim(playerid, triggerName);
        }
    }

    public void SendRequest(int id,string triggerName)
    {
        string data = id.ToString() + "," + triggerName;
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        playerid = int.Parse(strs[0]);
        triggerName = strs[1];
        isSync = true;
    }

    void OnSyncAnim(int id,string triggerName)
    {
        GameObject go;
        GameController._instance.playerDict.TryGetValue(playerid, out go);
        if (go != null)
        {
            Animator anim = go.GetComponent<Animator>();
            anim.SetTrigger(triggerName);
        }
    }

}
