using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class SellInventoryItemRequest : BaseRequest {

    private InventoryParentPanel parentPanel;

    public override void Start()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.SellInventoryItem;
        parentPanel = GetComponent<InventoryParentPanel>();
        base.Start();
    }

    public void SendRequest(InventoryItem it)
    {
        string data = it.Inventory.id + "," + it.Count + "," + it.isDressed;
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)(int.Parse(data));
        parentPanel.OnResponseToSellInventoryItemRequest(returnCode);
    }

}
