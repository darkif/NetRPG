using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UpdatePlayerInfoRequest : BaseRequest {

    public override void Start()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.UpdatePlayerInfo;
        base.Start();
    }


    public void SendRequest(RoleData roleData)
    {
        string data = roleData.Name + "," + roleData.Level.ToString() + "," + roleData.RoleId.ToString() + "," +
            roleData.Exp.ToString() + "," + roleData.Coin.ToString() + "," + roleData.Atk.ToString() + "," +
            roleData.Def.ToString() + "," + roleData.Hp.ToString() + "," + roleData.MaxHp;
        base.SendRequest(data);
    }

}
