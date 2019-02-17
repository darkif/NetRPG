using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour {
    private Text nameLabel;
    private Image icon;
    private Text desc;
    private Button useBtn;
    private Button closeBtn;

    private InventoryItem it;
    private InventoryItemPanel itPanel;

    private void Awake()
    {
        nameLabel = transform.Find("name").GetComponent<Text>();
        icon = transform.Find("icon").GetComponent<Image>();
        desc = transform.Find("desc").GetComponent<Text>();
        useBtn = transform.Find("useBtn").GetComponent<Button>();
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();

        useBtn.onClick.AddListener(OnUseButtonClick);
        closeBtn.onClick.AddListener(OnCloseButtonClick);
        this.gameObject.SetActive(false);
    }

    void OnUseButtonClick()
    {
        itPanel.ChangeCount(-1);
        PlayerInfo._instance.InventoryUse(it, 1);
        OnCloseButtonClick();
    }

    void OnCloseButtonClick()
    {
        Close();
        transform.parent.SendMessage("DisableSellButton");
    }

    public void Show(InventoryItem it)
    {
        this.gameObject.SetActive(true);
        this.it = it;
        icon.sprite = Resources.Load<Sprite>("Icon/" + it.Inventory.icon);
        nameLabel.text = it.Inventory.name;
        desc.text = it.Inventory.desc;
    }

    public void Show(InventoryItem it,InventoryItemPanel panel)
    {
        this.gameObject.SetActive(true);
        this.it = it;
        this.itPanel = panel;
        icon.sprite = Resources.Load<Sprite>("Icon/" + it.Inventory.icon);
        nameLabel.text = it.Inventory.name;
        desc.text = it.Inventory.desc;
    }

    public void Close()
    {
        it = null;
        itPanel = null;
        this.gameObject.SetActive(false);
    }

}
