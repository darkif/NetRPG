using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour {

    public static InventoryPanel _instance;

    public List<InventoryItemPanel> itemPanelList = new List<InventoryItemPanel>();

    private Button clearUpBtn;
    private Text inventoryNumLabel;
    private int count = 0;

    private void Awake()
    {
        _instance = this;
        InventoryManager._instance.OnInventoryChanged += OnInventoryChanged;
        clearUpBtn = transform.Find("ClearUpBtn").GetComponent<Button>();
        clearUpBtn.onClick.AddListener(OnClearUpClick);

        inventoryNumLabel = transform.Find("InventoryNumLabel").GetComponent<Text>();

    }


    void OnInventoryChanged()
    {
        UpdateShow();
    }

    //更新背包显示
    void UpdateShow()
    {
        for (int i = 0; i < itemPanelList.Count; i++)
        {
            itemPanelList[i].Clear();
        }

        int index = 0;
        foreach (KeyValuePair<int, InventoryItem> kvp in InventoryManager._instance.inventoryItemInBagDict)
        {
            InventoryItem it = kvp.Value;
            itemPanelList[index].SetInventoryItem(it);
            index++;
        }
        count = index;
        inventoryNumLabel.text = count + "/20";
    }


    //添加一个物品
    public void AddInventoryItem(InventoryItem it)
    {
        foreach (InventoryItemPanel itemPanel in itemPanelList)
        {
            //已经有相同装备则数量加1
            if (itemPanel.it!=null && itemPanel.it.Inventory.id==it.Inventory.id)
            {
                itemPanel.ChangeCount(1);
                it.Count++;
                InventoryManager._instance.inventoryItemInBagDict[it.Inventory.id] = it;
                return;
            }
        }

        foreach (InventoryItemPanel itemPanel in itemPanelList)
        {
            //没有该装备则在空格子放入
            if (itemPanel.it == null)
            {
                it.Count = 1;
                InventoryManager._instance.inventoryItemInBagDict.Add(it.Inventory.id,it);
                itemPanel.SetInventoryItem(it);
                count++;
                inventoryNumLabel.text = count + "/20";
                return;
            }
        }
    }

    //整理背包
    void OnClearUpClick()
    {
        UpdateShow();
    }

    public void UpdateInventoryNum(int count)
    {
        this.count += count;
        inventoryNumLabel.text = this.count + "/20";
    }

    private void OnDestroy()
    {
        InventoryManager._instance.OnInventoryChanged -= OnInventoryChanged;
    }



}
