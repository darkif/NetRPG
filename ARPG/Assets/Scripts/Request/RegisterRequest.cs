using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RegisterRequest : BaseRequest {

    private RegisterPanel registerPanel;

    public override void Awake()
    {
        registerPanel = GetComponent<RegisterPanel>();
        requestCode = RequestCode.User;
        actionCode = ActionCode.Register;
        base.Awake();
    }

    //发起注册的请求
    public void SendRequest(string username, string password)
    {
        string data = username + "," + password;
        base.SendRequest(data);
    }


    //处理服务器返回的数据
    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        registerPanel.OnRegisterResponse(returnCode);
    }

}
