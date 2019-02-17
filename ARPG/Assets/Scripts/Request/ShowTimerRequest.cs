using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class ShowTimerRequest : BaseRequest {

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.ShowTimer;
        base.Awake();
    }

    public override void OnResponse(string data)
    {
        int num = int.Parse(data);
        TeamWaitPanel._instance.OnResponseToShowTimer(num);
    }

}
