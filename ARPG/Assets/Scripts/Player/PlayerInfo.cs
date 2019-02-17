using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InfoType
{
    Name,
    HP,
    Level,
    Atk,
    Def,
    Coin,
    Exp,
    Equip,
    Request,
    All
}

public enum PlayerType
{
    Swordman,
    Wizard
}

public class PlayerInfo : MonoBehaviour {

    public delegate void OnPlayerInfoChangedEvent(InfoType infoType);
    public event OnPlayerInfoChangedEvent OnPlayerInfoChanged;

    public static PlayerInfo _instance;

    private UpdatePlayerInfoRequest updatePlayerInfoRequest;

    public int maxHp = 100;
    //public int maxExp = 100;

    private string _name;
    private int _hp = 100;
    private int _level = 1;
    private int _atk = 1;
    private int _def = 1;
    private int _coin = 0;
    private int _exp = 0;
    private PlayerType playerType;

    //public int helmID = 0;
    //public int clothID = 0;
    //public int weaponID = 0;
    //public int shoesID = 0;
    //public int necklaceID = 0;
    //public int braceletID = 0;
    //public int ringID = 0;
    //public int extraID = 0;


    //八个装备位置
    public InventoryItem helmInventory;
    public InventoryItem clothInventory;
    public InventoryItem weaponInventory;
    public InventoryItem shoesInventory;
    public InventoryItem necklaceInventory;
    public InventoryItem braceletInventory;
    public InventoryItem ringInventory;
    public InventoryItem extraInventory;

    #region 属性
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int HP
    {
        get { return _hp; }
        set { _hp = value; }
    }

    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public int Atk
    {
        get { return _def; }
        set { _def = value; }
    }

    public int Def
    {
        get { return _atk; }
        set { _atk = value; }
    }

    public int Coin
    {
        get { return _coin; }
        set { _coin = value; }
    }


    public int Exp
    {
        get { return _exp; }
        set { _exp = value; }
    }

    public PlayerType PlayerType
    {
        get
        {
            return playerType;
        }

        set
        {
            playerType = value;
        }
    }
    #endregion

    private void Init()
    {
        RoleData role = GameFacade.Instance.GetRoleData();
        this.Coin = role.Coin;
        this.Name = role.Name;
        this.Exp = role.Exp;
        this.Level = role.Level;
        this.Atk = role.Atk;
        this.Def = role.Def;
        this.HP = role.Hp;
        this.maxHp = role.MaxHp;

        if (GameFacade.Instance.GetRoleData().RoleId == 0)
        {
            this.playerType = PlayerType.Swordman;
        }

        //this.helmID = 1007;
        //this.shoesID = 1013;

        //InitProperty();

        OnPlayerInfoChanged(InfoType.All);
    }

    public int GetExpByLevel(int level)
    {
        return (int)((level - 1) * (100f + (100f + 10f * (level - 2f))) / 2);
    }

    //取钱
    public bool GetCoin(int count)
    {
        if (Coin >= count)
        {
            Coin -= count;
            OnPlayerInfoChanged(InfoType.Coin);
            OnPlayerInfoChanged(InfoType.Request);
            return true;
        }

        return false;
    }

    //存钱
    public void AddCoin(int count)
    {
        this.Coin += count;
        OnPlayerInfoChanged(InfoType.Request);
        OnPlayerInfoChanged(InfoType.Coin);
    }

    //物品的使用
    public void InventoryUse(InventoryItem it, int count)
    {
        it.Count -= count;
        if (it.Count == 0)
        {
            if (it.Inventory.inventoryType == InventoryType.Drug)
                InventoryPanel._instance.UpdateInventoryNum(-1);
            InventoryManager._instance.inventoryItemInBagDict.Remove(it.Inventory.id);
        }
        if (it.Inventory.inventoryType == InventoryType.Drug)
        {
            this._hp += it.Inventory.applyValue;
            if (this._hp > maxHp)
                this._hp = maxHp;

            changeEquipRequest.SendRequest(it, null);
            OnPlayerInfoChang(InfoType.Request);
            //OnPlayerInfoChanged(InfoType.HP);
        }
    }

    void InitProperty()
    {


        //PutonEquip(helmID);
        //PutonEquip(clothID);
        //PutonEquip(weaponID);
        //PutonEquip(shoesID);
        //PutonEquip(necklaceID);
        //PutonEquip(braceletID);
        //PutonEquip(ringID);
        //PutonEquip(extraID);


    }

    //穿上装备
    public int DressOn(InventoryItem it)
    {
        //首先检测有没有穿上相同类型的装备
        //没有则直接穿上，否则原穿上的放回背包

        bool isDressed = false;
        InventoryItem inventoryItem = null;
        switch (it.Inventory.equipType)
        {
            case EquipType.Bracelet:
                if (braceletInventory != null)
                {
                    isDressed = true;
                    inventoryItem = braceletInventory;
                }
                braceletInventory = it;
                break;
            case EquipType.Cloth:
                if (clothInventory != null)
                {
                    isDressed = true;
                    inventoryItem = clothInventory;
                }
                clothInventory = it;
                break;
            case EquipType.Extra:
                if (extraInventory != null)
                {
                    isDressed = true;
                    inventoryItem = extraInventory;
                }
                extraInventory = it;
                break;
            case EquipType.Helm:
                if (helmInventory != null)
                {
                    isDressed = true;
                    inventoryItem = helmInventory;
                }
                helmInventory = it;
                break;
            case EquipType.Necklace:
                if (necklaceInventory != null)
                {
                    isDressed = true;
                    inventoryItem = necklaceInventory;
                }
                necklaceInventory = it;
                break;
            case EquipType.Ring:
                if (ringInventory != null)
                {
                    isDressed = true;
                    inventoryItem = ringInventory;
                }
                ringInventory = it;
                break;
            case EquipType.Shoes:
                if (shoesInventory != null)
                {
                    isDressed = true;
                    inventoryItem = shoesInventory;
                }
                shoesInventory = it;
                break;
            case EquipType.Weapon:
                if (weaponInventory != null)
                {
                    isDressed = true;
                    inventoryItem = weaponInventory;
                }
                weaponInventory = it;
                break;
        }

        if (isDressed)//已经存在同类型装备
        {
            //同一个装备就不用换
            if (it.Inventory.id == inventoryItem.Inventory.id)
            {
                return 0;
            }
            inventoryItem.isDressed = false;
            InventoryManager._instance.inventoryItemInEquipDict.Remove(inventoryItem.Inventory.id);
            InventoryPanel._instance.AddInventoryItem(inventoryItem);
            this.maxHp -= inventoryItem.Inventory.hp;
            this.HP -= inventoryItem.Inventory.hp;
            this.Atk -= inventoryItem.Inventory.atk;
            this.Def -= inventoryItem.Inventory.def;
        }

        this.maxHp += it.Inventory.hp;
        this.HP += it.Inventory.hp;
        this.Atk += it.Inventory.atk;
        this.Def += it.Inventory.def;
        it.isDressed = true;
        InventoryManager._instance.inventoryItemInEquipDict.Add(it.Inventory.id, it);
        if (it.Count - 1 == 0)
        {
            InventoryPanel._instance.UpdateInventoryNum(-1);
        }
        OnPlayerInfoChanged(InfoType.All);

        changeEquipRequest.SendRequest(it, inventoryItem);

        return 1;
    }

    //刚登陆游戏的时候从服务器上读取的以装备的装备装上
    public int DressOnEquipList(Dictionary<int, InventoryItem> equipDict)
    {
        foreach (KeyValuePair<int, InventoryItem> kvp in equipDict)
        {
            InventoryItem it = kvp.Value;
            switch (it.Inventory.equipType)
            {
                case EquipType.Bracelet:
                    braceletInventory = it;
                    break;
                case EquipType.Cloth:
                    clothInventory = it;
                    break;
                case EquipType.Extra:
                    extraInventory = it;
                    break;
                case EquipType.Helm:
                    helmInventory = it;
                    break;
                case EquipType.Necklace:
                    necklaceInventory = it;
                    break;
                case EquipType.Ring:
                    ringInventory = it;
                    break;
                case EquipType.Shoes:
                    shoesInventory = it;
                    break;
                case EquipType.Weapon:
                    weaponInventory = it;
                    break;
            }
        }
        OnPlayerInfoChanged(InfoType.All);
        return 1;
    }

    //卸下装备
    public void DressOff(InventoryItem it)
    {
        switch (it.Inventory.equipType)
        {
            case EquipType.Bracelet:
                if (braceletInventory != null)
                {
                    braceletInventory = null;
                }
                break;
            case EquipType.Cloth:
                if (clothInventory != null)
                {
                    clothInventory = null;
                }
                break;
            case EquipType.Extra:
                if (extraInventory != null)
                {
                    extraInventory = null;
                }
                break;
            case EquipType.Helm:
                if (helmInventory != null)
                {
                    helmInventory = null;
                }
                break;
            case EquipType.Necklace:
                if (necklaceInventory != null)
                {
                    necklaceInventory = null;
                }
                break;
            case EquipType.Ring:
                if (ringInventory != null)
                {
                    ringInventory = null;
                }
                break;
            case EquipType.Shoes:
                if (shoesInventory != null)
                {
                    shoesInventory = null;
                }
                break;
            case EquipType.Weapon:
                if (weaponInventory != null)
                {
                    weaponInventory = null;
                }
                break;
        }

        it.isDressed = false;
        this.maxHp -= it.Inventory.hp;
        this.HP -= it.Inventory.hp;
        this.Atk -= it.Inventory.atk;
        this.Def -= it.Inventory.def;
        InventoryManager._instance.inventoryItemInEquipDict.Remove(it.Inventory.id);
        InventoryPanel._instance.AddInventoryItem(it);

        changeEquipRequest.SendRequest(null, it);
    }

    //角色信息更新后，就上传服务器更新角色信息
    void OnPlayerInfoChang(InfoType infoType)
    {
        if (infoType == InfoType.Request)
        {
            GameFacade.Instance.UpdateRoleData(Name, Level, Atk, Def, Exp, Coin, HP, maxHp);
            Invoke("UpdatePlayerInfoRequest", 0.5f);
        }
    }

    void UpdatePlayerInfoRequest()
    {
        updatePlayerInfoRequest.SendRequest(GameFacade.Instance.GetRoleData());
    }

    //穿上或卸下或卖出装备时就调用这个请求
    public void SendSellInventoryItemRequest(InventoryItem it1)
    {
        
    }

    public void OnResponseToChangeEquipRequest(ReturnCode returnCode) {
        if (returnCode == ReturnCode.Success)
        {
            isChangedInfo = true;
        }
        else
        {
            isChangedInfo = false;
        }
        
    }

    //穿上装备
    void PutonEquip(int id)
    {
        //0表示没有装备
        if (id == 0)
            return;
        Inventory inventory = null;
        InventoryManager._instance.inventoryDict.TryGetValue(id,out inventory);
        this.maxHp += inventory.hp;
        this.HP += inventory.hp;
        this.Atk += inventory.atk;
        this.Def += inventory.def;
    }

    //卸下装备
    void PutoffEquip(int id)
    {
        if (id == 0)
            return;
        Inventory inventory = null;
        InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
        this.maxHp -= inventory.hp;
        this.HP -= inventory.hp;
        this.Atk -= inventory.atk;
        this.Def -= inventory.def;
    }

    
    void UpdateInEquip()
    {
        if (InventoryManager._instance.inventoryItemInEquipDict.Count > 0)
        {
            DressOnEquipList(InventoryManager._instance.inventoryItemInEquipDict);
        }
    }

    void UpdateEquipInStart()
    {
        isSyncEquip = true;
    }

    private ChangeEquipRequest changeEquipRequest;
    private bool isSyncEquip = false;
    private bool isChangedInfo = false;

    private void Awake()
    {
        _instance = this;
        updatePlayerInfoRequest = GetComponent<UpdatePlayerInfoRequest>();
        changeEquipRequest = GetComponent<ChangeEquipRequest>();
        OnPlayerInfoChanged += OnPlayerInfoChang;
        InventoryManager._instance.IsSyncComplete += UpdateEquipInStart;
    }

    private void Start()
    {
        Init();
        //Invoke("UpdateInEquip", 2.0f);
    }

    private void Update()
    {
        if (isSyncEquip)
        {
            isSyncEquip = false;
            UpdateInEquip();
        }

        if (isChangedInfo)
        {
            isChangedInfo = false;
            OnPlayerInfoChang(InfoType.Request);
            OnPlayerInfoChanged(InfoType.All);
        }

    }

    private void OnDestroy()
    {
        OnPlayerInfoChanged -= OnPlayerInfoChang;
        InventoryManager._instance.IsSyncComplete -= UpdateEquipInStart;
    }

}
