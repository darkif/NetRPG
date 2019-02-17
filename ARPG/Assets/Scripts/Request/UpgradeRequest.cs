using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UpgradeRequest : BaseRequest {
    public override void Start()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.UpgradeSkill;
        base.Start();
    }

    public void SendRequest(SkillDB skillDB)
    {
        string data = skillDB.SkillId + "," + skillDB.Level;
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        
    }

}
