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
        requestCode = RequestCode.Game;
        actionCode = ActionCode.SyncBossTranform;
        base.Awake();
    }

    private void Start()
    {
        InvokeRepeating("SendSyncBossTransformRequest",0.5f, 1.0f / 30);
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
        string data = pos.x.ToString() + "," + pos.y.ToString() + "," + pos.z.ToString() + "," 
            + eulerAngles.x.ToString() + "," +eulerAngles.y.ToString() + "," + eulerAngles.z.ToString();
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        pos = new Vector3(float.Parse(strs[0]), float.Parse(strs[1]), float.Parse(strs[2]));
        eulerAngles = new Vector3(float.Parse(strs[3]), float.Parse(strs[4]), float.Parse(strs[5]));
        isSync = true;
    }

    void SendSyncBossTransformRequest()
    {
        this.SendRequest(Boss._instance.transform.position,Boss._instance.transform.eulerAngles);
    }

}
