using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UpdateRoleInfoRequest : BaseRequest {

    private RoleSelectPanel selectPanel;

    public override void Start()
    {
        selectPanel = GetComponent<RoleSelectPanel>();
        requestCode = RequestCode.User;
        actionCode = ActionCode.UpdateRoleInfo;
        base.Start();
    }

    //发起注册的请求
    public void SendRequest(string name, int level,int roleid,int exp,int coin,int atk,int def,int hp,int maxHp)
    {
        string data = name + "," + level.ToString() + "," + roleid.ToString() + "," + exp.ToString() + "," + coin.ToString()
            + "," + atk.ToString() + "," + def.ToString() + "," + hp.ToString() + "," + maxHp.ToString();
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        selectPanel.OnUpdateRoleResponse(returnCode);
    }

}
