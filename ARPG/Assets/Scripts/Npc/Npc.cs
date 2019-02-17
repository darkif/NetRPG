using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {

    public string talkDesc = "你准备好了吗，冒险家？Boss可不会轻易被打败";

    private GameObject tip;
    public int taskID;
    private Task task;

    private void Awake()
    {
        tip = transform.Find("Tip").gameObject;
        tip.SetActive(false);
    }

    private void Start()
    {
        task = TaskManager._instance.GetTaskById(taskID);
    }

    private void OnTriggerEnter(Collider other)
    {
        tip.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E))
        {
            TaskManager._instance.CurTask = task;
            NpcDialogPanel._instanace.ShowPanel(talkDesc,taskID);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TaskManager._instance.CurTask = task;
            NpcDialogPanel._instanace.ShowPanel(talkDesc, taskID);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        tip.SetActive(false);
    }

}
