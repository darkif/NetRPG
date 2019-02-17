using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem{

    private Inventory inventory;
    private int count = 1;
    private InventoryItemDB inventoryItemDB;

    public Inventory Inventory
    {
        get { return inventory; }
        set { inventory = value; }
    }

    public int Count
    {
        get { return count; }
        set { count = value; }
    }

    public InventoryItemDB InventoryItemDB
    {
        get
        {
            return inventoryItemDB;
        }

        set
        {
            inventoryItemDB = value;
        }
    }

    public bool isDressed = false;

    public InventoryItem() { }

    public InventoryItem(InventoryItemDB itemDB)
    {
        this.InventoryItemDB = itemDB;
        Inventory inven;
        InventoryManager._instance.inventoryDict.TryGetValue(itemDB.InventoryId, out inven);
        this.Inventory = inven;
        this.Count = itemDB.Count;
        this.isDressed = itemDB.IsDressed;
    }

    public InventoryItemDB CreateInventoryItemDB()
    {
        InventoryItemDB itemDB = new InventoryItemDB();
        itemDB.Count = Count;
        itemDB.InventoryId = inventory.id;
        itemDB.IsDressed = isDressed;
        return itemDB;
    }

}
