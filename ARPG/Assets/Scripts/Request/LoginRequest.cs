using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class LoginRequest : BaseRequest {

    private LoginPanel loginPanel;

    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        loginPanel = GetComponent<LoginPanel>();
        base.Awake();
    }

    //发送消息给服务器
    public void SendRequest(string username, string password)
    {
        string data = username + "," + password;
        base.SendRequest(data);
    }

    //处理从服务器返回的消息
    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');//返回的数据有returncode以及玩家信息
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);

        if (returnCode == ReturnCode.Success)
        {
            //保存游戏角色信息
            RoleData tempRole = new RoleData(strs[1], int.Parse(strs[2]), int.Parse(strs[3]), 
                int.Parse(strs[4]), int.Parse(strs[5]), int.Parse(strs[6]), int.Parse(strs[7]), int.Parse(strs[8]),int.Parse(strs[9]));
            GameFacade.Instance.SetRoleData(tempRole);
        }

        loginPanel.OnLoginResponse(returnCode);
    }

}
