using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryParentPanel : MonoBehaviour {

    public static InventoryParentPanel _instance;

    private EquipPopupPanel equipPopupPanel;
    private InventoryPopup inventoryPopup;

    private Button closeBtn;

    private Button sellBtn;
    private Text sellPrice;

    private InventoryItemPanel itPanel;

    private SellInventoryItemRequest sellRequest;
    private bool isSyncSell = false;
    private int sellNum = 0;

    private void Awake()
    {
        _instance = this;

        equipPopupPanel = transform.Find("EquipPopup").GetComponent<EquipPopupPanel>();
        inventoryPopup = transform.Find("InventoryPopup").GetComponent<InventoryPopup>();

        closeBtn = transform.Find("Inventory/CloseBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(OnCloseButtonClick);

        sellBtn = transform.Find("Inventory/SellBtn").GetComponent<Button>();
        sellBtn.onClick.AddListener(OnSellButtonClick);
        sellBtn.enabled = false;

        sellPrice = transform.Find("EquipPopup/Sell/SellNum").GetComponent<Text>();

        transform.localScale = Vector3.zero;

        sellRequest = GetComponent<SellInventoryItemRequest>();
    }

    private void Update()
    {
        if (isSyncSell)
        {
            isSyncSell = false;
            SellClear(sellNum);
            sellNum = 0;
        }
    }

    //装备
    public void OnEquipClick(object[] o)
    {
        InventoryItem it = o[0] as InventoryItem;
        bool isDressed = (bool)o[2];
        if (isDressed)
        {
            InventoryRoleEquip panel = o[1] as InventoryRoleEquip;
            if (it.Inventory.inventoryType == InventoryType.Equip)
            {
                equipPopupPanel.Show(it, panel, isDressed);
                inventoryPopup.Close();
            }
                
            else if (it.Inventory.inventoryType == InventoryType.Drug)
            {
                equipPopupPanel.Close();
                inventoryPopup.Show(it);
            }
                
        }
        else
        {
            InventoryItemPanel panel = o[1] as InventoryItemPanel;
            if (it.Inventory.inventoryType == InventoryType.Equip)
            {
                equipPopupPanel.Show(it, panel, isDressed);
                inventoryPopup.Close();
            }              
            else if (it.Inventory.inventoryType == InventoryType.Drug)
            {
                equipPopupPanel.Close();
                inventoryPopup.Show(it, panel);
            }
                

            itPanel = panel;
            sellBtn.enabled = true;
        }
    }

    //点击出售
    void OnSellButtonClick()
    {
        string[] str = sellPrice.text.Split(' ');
        sellNum = int.Parse(str[0]) * itPanel.it.Count;
        itPanel.it.Count = 0;
        sellRequest.SendRequest(itPanel.it);
        
    }

    //点击卖出服务器发回响应后调用
    void SellClear(int sellNum)
    {
        PlayerInfo._instance.AddCoin(sellNum);
        InventoryManager._instance.inventoryItemInBagDict.Remove(itPanel.it.Inventory.id);
        InventoryPanel._instance.UpdateInventoryNum(-1);
        itPanel.Clear();
        itPanel = null;
        equipPopupPanel.Close();
        inventoryPopup.Close();
    }

    public void OnResponseToSellInventoryItemRequest(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            isSyncSell = true;
        }
        else
        {
            isSyncSell = false;
        }
    }

    void DisableSellButton()
    {
        sellBtn.enabled = false;
    }


    void OnCloseButtonClick()
    {
        Tweener tweener = transform.DOScale(0, 0.4f);
        tweener.OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }


    public void ShowPanel()
    {
        if (gameObject.activeInHierarchy)
        {
            Tweener tweener = transform.DOScale(0, 0.4f);
            tweener.OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
        }
        else
        {
            this.gameObject.SetActive(true);
            transform.DOScale(1, 0.4f);
        }
        
    }

}
