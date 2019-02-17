using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class ChangeEquipRequest : BaseRequest {

    public override void Start()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.ChangeEquip;
        base.Start();
    }

    public void SendRequest(InventoryItem dressOnDB, InventoryItem dressOffDB)
    {
        string data = "";
        if (dressOnDB != null)
        {
            data += dressOnDB.Inventory.id + "," + dressOnDB.Count + "," + dressOnDB.isDressed + ",";
        }
        if (dressOffDB != null)
        {
            data += dressOffDB.Inventory.id + "," + dressOffDB.Count + "," + dressOffDB.isDressed + ",";
        }
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)(int.Parse(data));
        PlayerInfo._instance.OnResponseToChangeEquipRequest(returnCode);
    }

}
