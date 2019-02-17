using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryRole : MonoBehaviour {
    private InventoryRoleEquip helmEquip;
    private InventoryRoleEquip clothEquip;
    private InventoryRoleEquip weaponEquip;
    private InventoryRoleEquip shoesEquip;
    private InventoryRoleEquip necklaceEquip;
    private InventoryRoleEquip braceletEquip;
    private InventoryRoleEquip ringEquip;
    private InventoryRoleEquip extraEquip;


    private void Awake()
    {
        helmEquip = transform.Find("Helmet").GetComponent<InventoryRoleEquip>();
        clothEquip = transform.Find("Cloth").GetComponent<InventoryRoleEquip>();
        weaponEquip = transform.Find("Weapon").GetComponent<InventoryRoleEquip>();
        shoesEquip = transform.Find("Shoes").GetComponent<InventoryRoleEquip>();
        necklaceEquip = transform.Find("Necklace").GetComponent<InventoryRoleEquip>();
        braceletEquip = transform.Find("Bracelet").GetComponent<InventoryRoleEquip>();
        ringEquip = transform.Find("Ring").GetComponent<InventoryRoleEquip>();
        extraEquip = transform.Find("Extra").GetComponent<InventoryRoleEquip>();

        PlayerInfo._instance.OnPlayerInfoChanged += OnPlayerInfoChanged;
    }


    void OnPlayerInfoChanged(InfoType infoType)
    {
        if(infoType==InfoType.All || infoType == InfoType.Atk || infoType == InfoType.Def || infoType == InfoType.HP || infoType==InfoType.Equip)
        {
            UpdateShow();
        }
    }

    void UpdateShow()
    {
        //在背包角色面板显示装备
        //helmEquip.SetId(PlayerInfo._instance.helmID);
        //clothEquip.SetId(PlayerInfo._instance.clothID);
        //weaponEquip.SetId(PlayerInfo._instance.weaponID);
        //shoesEquip.SetId(PlayerInfo._instance.shoesID);
        //necklaceEquip.SetId(PlayerInfo._instance.necklaceID);
        //braceletEquip.SetId(PlayerInfo._instance.braceletID);
        //ringEquip.SetId(PlayerInfo._instance.ringID);
        //extraEquip.SetId(PlayerInfo._instance.extraID);

        PlayerInfo info = PlayerInfo._instance;
        helmEquip.SetInventoryItem(info.helmInventory);
        clothEquip.SetInventoryItem(info.clothInventory);
        weaponEquip.SetInventoryItem(info.weaponInventory);
        shoesEquip.SetInventoryItem(info.shoesInventory);
        necklaceEquip.SetInventoryItem(info.necklaceInventory);
        braceletEquip.SetInventoryItem(info.braceletInventory);
        ringEquip.SetInventoryItem(info.ringInventory);
        extraEquip.SetInventoryItem(info.extraInventory);
    }

    private void OnDestroy()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= OnPlayerInfoChanged;
    }

}
