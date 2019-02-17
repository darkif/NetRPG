using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NpcDialogPanel : MonoBehaviour {

    public static NpcDialogPanel _instanace;

    private Text npcDialog;
    private Button accpetBtn;

    private Task task;

    public Task Task
    {
        get
        {
            return task;
        }

        set
        {
            task = value;
        }
    }

    private void Awake()
    {
        _instanace = this;

        npcDialog = transform.Find("dialog").GetComponent<Text>();
        accpetBtn = transform.Find("accpetBtn").GetComponent<Button>();

        accpetBtn.onClick.AddListener(OnAccpetButtonClick);

        this.gameObject.SetActive(false);
    }


    void OnAccpetButtonClick()
    {
        //通知任务管理器已经接受
        TaskManager._instance.OnAccpetTask(Task);
        Close();
    }

    public void ShowPanel(string npcTalk,int taskid)
    {
        //transform.localScale = Vector3.zero;
        TaskManager._instance.taskDict.TryGetValue(taskid, out task);
        this.gameObject.SetActive(true);
        npcDialog.text = npcTalk;
        transform.DOScale(1, 0.4f);
    }

    public void Close()
    {
        Tweener tweener = transform.DOScale(0, 0.4f);
        tweener.OnComplete(() =>
        {
            npcDialog.text = "";
            this.gameObject.SetActive(false);
        });
    }
}
