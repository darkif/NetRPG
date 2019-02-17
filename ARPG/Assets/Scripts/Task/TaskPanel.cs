using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TaskPanel : MonoBehaviour {

    public GameObject taskItemPanel;
    public Transform content;
    private Button closeBtn;

    private bool isInit = false;

    private void Awake()
    {
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(OnCloseButtonClick);
    }

    private void Start()
    {
        TaskManager._instance.OnSyncComplete += this.OnSyncComplete;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isInit)
        {
            isInit = false;
            InitTaskList();
        }
    }

    //  初始化任务列表
    void InitTaskList()
    {
        List<Task> taskList = TaskManager._instance.taskList;
        foreach(Task task in taskList)
        {
            GameObject go = Instantiate(taskItemPanel, content) as GameObject;
            go.GetComponent<TaskItemPanel>().SetTask(task);
        }
    }

    //线程不能直接调用unity组件等
    void OnSyncComplete()
    {
        isInit = true;
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
        if (transform.gameObject.activeInHierarchy)
        {
            Tweener tweener = transform.DOScale(0, 0.4f);
            tweener.OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
        }
        else
        {
            transform.localScale = Vector3.zero;
            this.gameObject.SetActive(true);
            transform.DOScale(1, 0.4f);
        }
       
    }

}
