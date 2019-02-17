using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipPopupPanel : MonoBehaviour {
    private Image equipIcon;
    private Text nameLabel;
    private Text atkNum;
    private Text defNum;
    private Text hpNum;
    private Text sellNum;

    private Button equipBtn;
    private Button closeBtn;

    private InventoryItem it;
    private InventoryItemPanel itPanel;
    private InventoryRoleEquip rolePanel;

    private bool isDressed = false;

    private void Awake()
    {
        equipIcon = transform.Find("Equip").GetComponent<Image>();
        nameLabel = transform.Find("Name").GetComponent<Text>();
        atkNum = transform.Find("Atk/AtkNum").GetComponent<Text>();
        defNum = transform.Find("Def/DefNum").GetComponent<Text>();
        hpNum = transform.Find("HP/HpNum").GetComponent<Text>();
        sellNum = transform.Find("Sell/SellNum").GetComponent<Text>();

        equipBtn = transform.Find("EquipBtn").GetComponent<Button>();
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();

        equipBtn.onClick.AddListener(OnEquipButtonClick);
        closeBtn.onClick.AddListener(OnCloseButtonClick);

        this.gameObject.SetActive(false);
    }

    //处理背包里点击装备的显示
    public void Show(InventoryItem it, InventoryItemPanel itemPanel,bool isDressed)
    {
        if (it != null || it.Inventory != null)
        {
            this.gameObject.SetActive(true);
            this.it = it;
            this.itPanel = itemPanel;
            this.isDressed = isDressed;
            equipIcon.sprite = Resources.Load<Sprite>("Icon/" + it.Inventory.icon);
            nameLabel.text = it.Inventory.name;
            atkNum.text = it.Inventory.atk.ToString();
            defNum.text = it.Inventory.def.ToString();
            hpNum.text = it.Inventory.hp.ToString();
            sellNum.text = it.Inventory.price.ToString() + " coin";
            if (isDressed)
            {
                equipBtn.GetComponentInChildren<Text>().text = "卸下";
            }
            else
            {
                equipBtn.GetComponentInChildren<Text>().text = "装备";
            }
            
        }
       
    }

    //处理角色信息里点击装备的显示
    public void Show(InventoryItem it, InventoryRoleEquip itemPanel, bool isDressed)
    {
        if (it != null || it.Inventory != null)
        {
            this.gameObject.SetActive(true);
            this.it = it;
            this.rolePanel = itemPanel;
            this.isDressed = isDressed;
            equipIcon.sprite = Resources.Load<Sprite>("Icon/" + it.Inventory.icon);
            nameLabel.text = it.Inventory.name;
            atkNum.text = it.Inventory.atk.ToString();
            defNum.text = it.Inventory.def.ToString();
            hpNum.text = it.Inventory.hp.ToString();
            sellNum.text = it.Inventory.price.ToString() + " coin";
            if (isDressed)
            {
                equipBtn.GetComponentInChildren<Text>().text = "卸下";
            }
            else
            {
                equipBtn.GetComponentInChildren<Text>().text = "装备";
            }

        }

    }

    void OnEquipButtonClick()
    {
        if (it == null)
            return;

        if (isDressed) { //点击的是已经穿上的装备则为卸下装备
            PlayerInfo._instance.DressOff(it);
            rolePanel.Clear();
            it = null;
            itPanel = null;
        }
        else
        {
            //穿上装备
            int res = PlayerInfo._instance.DressOn(it);
            ////不是相同的装备或者装备栏为空的时候,成功穿上装备
            if (res != 0)
            {
                if (it.Count - 1 > 0)
                {
                    itPanel.ChangeCount(-1);
                }
                else
                {
                    itPanel.Clear();//清空格子
                }
                PlayerInfo._instance.InventoryUse(it, 1);
                it = null;
                itPanel = null;
            }
        }
        OnCloseButtonClick();
    }

    void OnCloseButtonClick()
    {
        Close();
        transform.parent.SendMessage("DisableSellButton");
    }

    public void Close()
    {
        it = null;
        itPanel = null;
        this.gameObject.SetActive(false);
    }

}
