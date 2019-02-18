using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GetTeamInfoRequest : BaseRequest {
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.GetTeamInfo;
        base.Awake();
    }

    public override void OnResponse(string data)
    {
        GameController._instance.OnResponseToGetTeamInfo(data);
    }

}
