using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemPanel : MonoBehaviour {

    private Image sprite;
    private Text numLabel;
    public InventoryItem it;
    private Button btn;

    private bool isExit = false;

    private void Awake()
    {
        sprite = transform.Find("Sprite").GetComponent<Image>();
        numLabel = transform.Find("Num").GetComponent<Text>();
        btn = transform.Find("Sprite").GetComponent<Button>();
        btn.onClick.AddListener(OnInventoryItemClick);
    }

    public void SetInventoryItem(InventoryItem it)
    {
        this.it = it;
        sprite.sprite = Resources.Load<Sprite>("Icon/" + it.Inventory.icon);
        numLabel.text = it.Count.ToString();
        isExit = true;
    }

    //清空信息
    public void Clear()
    {
        it = null;
        sprite.sprite = Resources.Load<Sprite>("bg_道具");
        numLabel.text = "";
        isExit = false;
    }

    void OnInventoryItemClick()
    {
        if (!isExit)
            return;
        object[] o = new object[3];
        o[0] = it;
        o[1] = this;
        o[2] = false;
        transform.parent.parent.parent.parent.parent.SendMessage("OnEquipClick",o);
    }


    public void ChangeCount(int count)
    {
        if (it.Count + count <= 0)
        {
            Clear();
        }
        else
        {
            numLabel.text = (it.Count + count).ToString();
        }
    }

}
