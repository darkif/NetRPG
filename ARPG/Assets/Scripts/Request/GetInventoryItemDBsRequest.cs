using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GetInventoryItemDBsRequest : BaseRequest {
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.GetInventoryItemDBs;
        base.Awake();
    }

    public override void SendRequest()
    {
        base.SendRequest("r");
    }

    public override void OnResponse(string data)
    {
        string[] strs = data.Split('|');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        if (returnCode == ReturnCode.Success)
        {

            string[] inventoryArray = strs[1].Split('-');
            List<InventoryItemDB> dbList = new List<InventoryItemDB>();
            foreach (string s in inventoryArray)
            {
                if (s == "" || s == " ")
                    break;

                string[] proArray = s.Split(',');
                InventoryItemDB inventoryItemDB = new InventoryItemDB();
                inventoryItemDB.InventoryId = int.Parse(proArray[0]);
                inventoryItemDB.Count = int.Parse(proArray[1]);
                inventoryItemDB.IsDressed = bool.Parse(proArray[2]);
                dbList.Add(inventoryItemDB);
            }
            //背包响应
            InventoryManager._instance.OnResponseToGetInventoryItemDBsRequest(dbList);
        }
        else
        {
            InventoryManager._instance.OnResponseToGetInventoryItemDBsRequest(null);
        }
    }

}
