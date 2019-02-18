using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class TaskManager : MonoBehaviour {

    public static TaskManager _instance;

    public TextAsset taskInfoListText;
    public List<Task> taskList = new List<Task>();
    public Dictionary<int, Task> taskDict = new Dictionary<int, Task>();

    public Dictionary<int, Task> accpetTaskList = new Dictionary<int, Task>();
    private Task curTask;

    public Task CurTask
    {
        get
        {
            return curTask;
        }

        set
        {
            curTask = value;
        }
    }

    public AddTaskRequest AddTaskRequest
    {
        get
        {
            return addTaskRequest;
        }
    }

    private GetTaskRequest getTaskRequest;
    private AddTaskRequest addTaskRequest;

    public delegate void OnSyncCompleteEvent();
    public event OnSyncCompleteEvent OnSyncComplete;

    private void Awake()
    {
        _instance = this;
        getTaskRequest = GetComponent<GetTaskRequest>();
        addTaskRequest = GetComponent<AddTaskRequest>();
        InitTask();
    }

    void GetTaskRequest() {
        getTaskRequest.SendRequest();
    }

    //任务信息初始化
    public void InitTask()
    {
        string[] taskStrs = taskInfoListText.ToString().Split('\n');
        foreach(string str in taskStrs)
        {
            string[] proArray = str.Split(',');
            Task task = new Task();
            task.Id = int.Parse(proArray[0]);
            switch (proArray[1])
            {
                case "Main":
                    task.TaskType = TaskType.Main;
                    break;
                case "Reward":
                    task.TaskType = TaskType.Reward;
                    break;
                case "Daily":
                    task.TaskType = TaskType.Daily;
                    break;
            }
            task.TaskName = proArray[2];
            task.TaskIcon = proArray[3];
            task.TaskDesc = proArray[4];
            task.Coin = int.Parse(proArray[5]);
            task.TalkNpc = proArray[6];
            task.IdNpc = int.Parse(proArray[7]);
            task.IdMession = int.Parse(proArray[8]);
            taskList.Add(task);
            taskDict.Add(task.Id, task);
        }

        Invoke("GetTaskRequest", 1.0f);
    }

    //执行某个任务
    public void OnExcuteTask(Task task)
    {
        CurTask = task;
        //到Npc那接受任务
        if (task.TaskProgress == TaskState.NoStart)
        {
            MessageManager._instance.ShowMessage("请到 " + task.IdNpc + "UnityChan 接受任务", 1f);
        }
        else if(task.TaskProgress == TaskState.Accpet){
            MessageManager._instance.ShowMessage("任务进行中...", 1f);
        }
    }

    public void OnAccpetTask(Task task)
    {
        if (curTask == null)
            curTask = task;
        if (task.TaskProgress != TaskState.NoStart)
            return;
        if (!accpetTaskList.ContainsKey(task.Id))
        {
            accpetTaskList.Add(task.Id, task);
            curTask.TaskProgress = TaskState.Accpet;
            curTask.UpdateTask();
        }           
    }

    public Task GetTaskById(int id)
    {
        foreach(Task task in taskList)
        {
            if (task.Id == id)
            {
                return task;
            }
        }

        return null;
    }

    //对gettaskRequest进行响应
    public void OnResponseToGetTask(List<TaskDB> taskDBList)
    {
        if (taskDBList == null)
        {
            return;
        }
        else
        {

            foreach (TaskDB taskDB in taskDBList)
            {
                Task task = null;
                if (taskDict.TryGetValue(taskDB.TaskId, out task))
                {
                    task.SyncTask(taskDB);
                    if (taskDB.TaskState == TaskState.Accpet && !accpetTaskList.ContainsKey(task.Id)) {
                        accpetTaskList.Add(task.Id, task);
                    }
                }
            }

            if (OnSyncComplete != null)
            {
                OnSyncComplete();
            }
        }
    }


    public void OnResponseToAddOrUpdateTaskRequest(TaskDB taskDB)
    {
        Task task = null;
        taskDict.TryGetValue(taskDB.TaskId, out task);
        task.TaskProgress = taskDB.TaskState;
    }

}
