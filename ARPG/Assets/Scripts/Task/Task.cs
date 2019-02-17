using System.Collections;
using System.Collections.Generic;

public enum TaskType
{
    Main=0,       //主线
    Reward=1,     //赏金任务
    Daily=2       //日常
}

public enum TaskState    //任务状态
{
    NoStart=0,    //未接受
    Accpet=1,     //已接受
    Complete=2,   //完成
    Reward=3      //领取奖励
}


public class Task{
    private int id;             //任务id        
    private TaskType taskType;  //任务类型
    private string taskName;    //任务名
    private string taskIcon;    //任务图标
    private string taskDesc;    //任务描述
    private int coin;       //获得奖励金币
    private string talkNpc; //npc说的话
    private int idNpc;       //npc的id   
    private int idMession;  //副本id
    private TaskState taskProgress = TaskState.NoStart;

    public TaskDB TaskDB { get; set; } 

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public TaskType TaskType
    {
        get
        {
            return taskType;
        }

        set
        {
            taskType = value;
        }
    }

    public string TaskName
    {
        get
        {
            return taskName;
        }

        set
        {
            taskName = value;
        }
    }

    public string TaskIcon
    {
        get
        {
            return taskIcon;
        }

        set
        {
            taskIcon = value;
        }
    }

    public string TaskDesc
    {
        get
        {
            return taskDesc;
        }

        set
        {
            taskDesc = value;
        }
    }

    public int Coin
    {
        get
        {
            return coin;
        }

        set
        {
            coin = value;
        }
    }

    public string TalkNpc
    {
        get
        {
            return talkNpc;
        }

        set
        {
            talkNpc = value;
        }
    }

    public int IdNpc
    {
        get
        {
            return idNpc;
        }

        set
        {
            idNpc = value;
        }
    }

    public int IdMession
    {
        get
        {
            return idMession;
        }

        set
        {
            idMession = value;
        }
    }

    public TaskState TaskProgress
    {
        get
        {
            return taskProgress;
        }

        set
        {
            if (taskProgress != value)
            {
                taskProgress = value;
                OnTaskStateChanged();
            }
            
        }
    }

    public delegate void OnTaskStateChangedEvent();
    public event OnTaskStateChangedEvent OnTaskStateChanged;

    //同步任务信息
    public void SyncTask(TaskDB taskDB)
    {
        TaskDB = taskDB;
        switch (taskDB.TaskState)
        {
            case TaskState.NoStart:
                taskProgress = TaskState.NoStart;
                break;
            case TaskState.Accpet:
                taskProgress = TaskState.Accpet;
                break;
            case TaskState.Complete:
                taskProgress = TaskState.Complete;
                break;
            case TaskState.Reward:
                taskProgress = TaskState.Reward;
                break;
        }
    }

    //更新任务状态到数据库 
    public void UpdateTask()
    {
        if (TaskDB == null)
            TaskDB = new TaskDB();
        TaskDB.TaskId = Id;
        TaskDB.TaskState = taskProgress;
        TaskDB.TaskType = taskType;
        TaskDB.LastUpdateTime = System.DateTime.Now;
        TaskManager._instance.AddTaskRequest.SendRequest(TaskDB);
    }

}
