using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void IsSyncCompleteEvent();

public class InventoryManager : MonoBehaviour {

    public static InventoryManager _instance;

    public TextAsset listInfo;

    public Dictionary<int, Inventory> inventoryDict = new Dictionary<int, Inventory>();
    public Dictionary<int, InventoryItem> inventoryItemInBagDict = new Dictionary<int, InventoryItem>();
    public Dictionary<int, InventoryItem> inventoryItemInEquipDict = new Dictionary<int, InventoryItem>();

    public delegate void OnInventoryChangedEvent();
    public event OnInventoryChangedEvent OnInventoryChanged;

    private GetInventoryItemDBsRequest inventoryItemDBsRequest;
    private UpdateOrAddInventoryRequest updateOrAddInventoryRequest;
    private bool isInventoryChanged = false;


    public event IsSyncCompleteEvent IsSyncComplete;
    private bool isSync = false;

    private void Awake()
    {
        _instance = this;
        ReadInventoryInfo();
        inventoryItemDBsRequest = GetComponent<GetInventoryItemDBsRequest>();
        updateOrAddInventoryRequest = GetComponent<UpdateOrAddInventoryRequest>();
    }

    private void Start()
    {
        Invoke("ReadInventoryItemInfo",2.0f);
    }


    private void Update()
    {
        if (isInventoryChanged)
        {
            OnInventoryChanged();
            isInventoryChanged = false;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            PickUpInventory();
        }
    }


    //从文本读取物品信息
    void ReadInventoryInfo()
    {
        string str = listInfo.ToString();
        string[] itemStrArray = str.Split('\n');
        foreach(string itemStr in itemStrArray)
        {
            string[] proArray = itemStr.Split(',');
            Inventory inventory = new Inventory();
            inventory.id = int.Parse(proArray[0]);
            inventory.name = proArray[1];
            inventory.icon = proArray[2];
            switch (proArray[3])
            {
                case "Equip":
                    inventory.inventoryType = InventoryType.Equip;
                    break;
                case "Drug":
                    inventory.inventoryType = InventoryType.Drug;
                    break;
            }
            if (inventory.inventoryType == InventoryType.Equip)
            {
                switch (proArray[4])
                {
                    case "Helm":
                        inventory.equipType = EquipType.Helm;
                        break;
                    case "Cloth":
                        inventory.equipType = EquipType.Cloth;
                        break;
                    case "Weapon":
                        inventory.equipType = EquipType.Weapon;
                        break;
                    case "Shoes":
                        inventory.equipType = EquipType.Shoes;
                        break;
                    case "Necklace":
                        inventory.equipType = EquipType.Necklace;
                        break;
                    case "Bracelet":
                        inventory.equipType = EquipType.Bracelet;
                        break;
                    case "Ring":
                        inventory.equipType = EquipType.Ring;
                        break;
                    case "Extra":
                        inventory.equipType = EquipType.Extra;
                        break;
                }
            }

            inventory.price = int.Parse(proArray[5]);
            inventory.hp = int.Parse(proArray[6]);
            inventory.atk = int.Parse(proArray[7]);
            inventory.def = int.Parse(proArray[8]);

            if (inventory.inventoryType == InventoryType.Drug)
            {
                switch (proArray[9])
                {
                    case "HP":
                        inventory.infoType = InfoType.HP;
                        inventory.applyValue = int.Parse(proArray[10]);
                        break;
                }
            }

            inventory.desc = proArray[11];
            inventoryDict.Add(inventory.id, inventory);
        }
    }

    //完成角色背包初始化
    void ReadInventoryItemInfo()
    {
        //连接服务器，取得玩家当前拥有的物品
        inventoryItemDBsRequest.SendRequest();

        #region//随机生成物品
        /*for(int i = 0; i < 20; i++)
        {
            int id = Random.Range(1001, 1019);

            InventoryItem it = null;
            bool isExit = inventoryItemDict.TryGetValue(id, out it);
            if (isExit)
            {
                it.Count++;
            }
            else
            {

                Inventory inventory;
                inventoryDict.TryGetValue(id, out inventory);
                it = new InventoryItem();
                it.Inventory = inventory;
                it.Count = 1;
                inventoryItemDict.Add(id, it);
            }
            OnInventoryChanged();
        }*/
        #endregion

        InventoryParentPanel._instance.gameObject.SetActive(false);
    }

    void PickUpInventory()
    {
        int id = Random.Range(1001, 1019);

        InventoryItem it = null;
        bool isExit = inventoryItemInBagDict.TryGetValue(id, out it);
        if (isExit)
        {
            it.Count++;
            InventoryItemDB itemDB = it.CreateInventoryItemDB();
            updateOrAddInventoryRequest.SendRequest(itemDB);
        }
        else
        {

            Inventory inventory;
            inventoryDict.TryGetValue(id, out inventory);
            it = new InventoryItem();
            it.Inventory = inventory;
            it.Count = 1;
            inventoryItemInBagDict.Add(id, it);
            InventoryItemDB itemDB = it.CreateInventoryItemDB();
            updateOrAddInventoryRequest.SendRequest(itemDB);
        }
        
        OnInventoryChanged();
    }

    public void OnResponseToGetInventoryItemDBsRequest(List<InventoryItemDB> dbList)
    {
        if (dbList == null)
        {
            return;
        }

        foreach (InventoryItemDB itemDB in dbList)
        {
            InventoryItem it = new InventoryItem(itemDB);
            if (it.Count == 0)
                continue;
            if (it.isDressed)
            {
                inventoryItemInEquipDict.Add(it.Inventory.id, it);
                it.Count--;
            }

            if (it.Count > 0)
            {
                inventoryItemInBagDict.Add(it.Inventory.id, it);
            }
        }


        //OnInventoryChanged();
        
        if (IsSyncComplete != null)
        {
            IsSyncComplete();
        }

        isInventoryChanged = true;
    }

    public void OnUpdateOrAddInventoryRequest(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            isInventoryChanged = true;
        }
        else
        {
            MessageManager._instance.ShowMessage("更新背包失败，请检查网络", 1f);
        }
    }

}
