using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class SyncTransformRequest : BaseRequest {
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.SyncPosAndRotation;
        base.Awake();
    }


    public void SendRequest(int roleId,Vector3 pos, Vector3 eulerAngles,bool isMove)
    {
        string data = roleId.ToString() + "," + pos.x.ToString() + "," + pos.y.ToString()+ "," 
            + pos.z.ToString() + ","+ eulerAngles.x.ToString() + "," + eulerAngles.y.ToString() 
            + "," + eulerAngles.z.ToString() + "," + isMove.ToString();
        base.SendRequest(data);
    }


    public override void OnResponse(string data)
    {
        if (data == "r")
            return;
        string[] strs = data.Split(',');
        int playerid = int.Parse(strs[0]);
        Vector3 pos = new Vector3(float.Parse(strs[1]), float.Parse(strs[2]), float.Parse(strs[3]));
        Vector3 eulerAngler = new Vector3(float.Parse(strs[4]), float.Parse(strs[5]), float.Parse(strs[6]));
        bool isMove = bool.Parse(strs[7]);
        GameController._instance.OnResponseToSyncTransformRequest(playerid, pos, eulerAngler,isMove);
    }

}
