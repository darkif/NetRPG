using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

public class SyncBossTransformRequest : BaseRequest {

    private Boss boss;
    private bool isSync = false;
    private Vector3 pos;
    private Vector3 eulerAngles;
    private DateTime lastTime = DateTime.MinValue;

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.SyncBossTranform;
        base.Awake();
    }

    private void Update()
    {
        if (isSync)
        {
            isSync = false;
            transform.position = pos;
            transform.eulerAngles = eulerAngles;
        }
    }

    public void SendRequest(Vector3 pos,Vector3 eulerAngles)
    {
        string data = pos.x.ToString() + "," + pos.y.ToString() + "," + pos.z.ToString() + ","
            + eulerAngles.x.ToString() + "," + eulerAngles.y.ToString() + "," + eulerAngles.z.ToString() + "," + DateTime.Now;
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
       
        DateTime time = DateTime.Parse(strs[6]);
        if (time > lastTime)
        {
            lastTime = time;
            pos = new Vector3(float.Parse(strs[0]), float.Parse(strs[1]), float.Parse(strs[2]));
            eulerAngles = new Vector3(float.Parse(strs[3]), float.Parse(strs[4]), float.Parse(strs[5]));
            isSync = true;
        }
    }

}
