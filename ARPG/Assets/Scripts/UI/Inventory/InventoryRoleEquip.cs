using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryRoleEquip : MonoBehaviour {

    private Image sprite;
    private Button btn;
    private InventoryItem it;

    private void Awake()
    {
        sprite = GetComponent<Image>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnEquipClick);
    }

    //根据id更改图片显示
    public void SetId(int id)
    {
        Inventory inventory = null;
        bool isExit = InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
        if (isExit)
        {
            sprite.sprite= Resources.Load<Sprite>("Icon/" + inventory.icon);
        }
    }

    public void SetInventoryItem(InventoryItem it)
    {
        if (it == null)
            return;
        this.it = it;
        sprite.sprite = Resources.Load<Sprite>("Icon/" + it.Inventory.icon); 
    }

    void OnEquipClick()
    {
        if (it != null)
        {
            object[] o = new object[3];
            o[0] = it;
            o[1] = this;
            o[2] = true;
            transform.parent.parent.SendMessage("OnEquipClick", o);
        }
       
    }

    public void Clear() //清空
    {
        this.it = null;
        sprite.sprite = Resources.Load<Sprite>("bg_道具" );
    }

}
