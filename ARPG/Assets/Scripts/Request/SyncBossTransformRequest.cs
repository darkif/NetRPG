using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class SyncBossTransformRequest : BaseRequest {

    private bool isSync = false;
    private Vector3 pos;
    private Vector3 eulerAngles;

    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.SyncBossTranform;
        base.Awake();
    }

    private void Start()
    {
        //InvokeRepeating("SendSyncBossTransformRequest", 2f, 1.0f / 10);
    }

    private void Update()
    {
        if (isSync)
        {
            isSync = false;
            Boss._instance.transform.position = pos;
            Boss._instance.transform.eulerAngles = eulerAngles;
        }
    }

    public void SendRequest(Vector3 pos,Vector3 eulerAngles)
    {
        string data = pos.x + "," + pos.y + "," + pos.z + "," + eulerAngles.x + "," +eulerAngles.y + "," + eulerAngles.z;
        base.SendRequest("r");
    }

    public override void OnResponse(string data)
    {
        //string[] strs = data.Split(',');
        //pos = new Vector3(float.Parse(strs[0]), float.Parse(strs[1]), float.Parse(strs[2]));
        //eulerAngles = new Vector3(float.Parse(strs[3]), float.Parse(strs[4]), float.Parse(strs[5]));
        //isSync = true;
    }

    void SendSyncBossTransformRequest()
    {
        this.SendRequest(Boss._instance.transform.position,Boss._instance.transform.eulerAngles);
    }

}
