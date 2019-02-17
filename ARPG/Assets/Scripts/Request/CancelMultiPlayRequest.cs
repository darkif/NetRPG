using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class CancelMultiPlayRequest : BaseRequest {
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.CancelAddMultiPlay;
        base.Awake();
    }

    public override void SendRequest()
    {
        base.SendRequest("r");
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)(int.Parse(data));
        TeamWaitPanel._instance.OnResponseToCancelRequest(returnCode);
    }

}
