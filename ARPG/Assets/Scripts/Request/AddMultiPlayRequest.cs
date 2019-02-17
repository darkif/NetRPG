using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class AddMultiPlayRequest : BaseRequest {
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.AddMultiPlay;
        base.Awake();
    }

    public void SendRequest(int mapid)
    {
        string data = mapid.ToString();
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)(int.Parse(data));
        OnlinePanel._instance.OnResponseToAddMultiPlayRequest(returnCode);
    }

}
