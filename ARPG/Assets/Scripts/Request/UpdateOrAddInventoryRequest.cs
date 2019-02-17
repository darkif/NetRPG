using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UpdateOrAddInventoryRequest : BaseRequest {

    public override void Start()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.UpdateOrAddInventoryItemDB;
        base.Start();
    }

    public void SendRequest(InventoryItemDB itemDB)
    {
        string data = itemDB.InventoryId.ToString() + "," + itemDB.Count.ToString() + "," + itemDB.IsDressed.ToString();
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)(int.Parse(data));
        InventoryManager._instance.OnUpdateOrAddInventoryRequest(returnCode);
    }

}
