using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UpgradeRequest : BaseRequest {
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.UpgradeSkill;
        base.Awake();
    }

    public void SendRequest(SkillDB skillDB)
    {
        string data = skillDB.SkillId + "," + skillDB.Level + "," + skillDB.Damage;
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        
    }

}
