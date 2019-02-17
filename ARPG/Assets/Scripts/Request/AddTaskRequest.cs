using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class AddTaskRequest : BaseRequest {

    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.UpdateOrAddTask;
        base.Awake();
    }

    public void SendRequest(TaskDB taskDB)
    {
        string data = taskDB.TaskId.ToString() + "," + ((int)taskDB.TaskState).ToString() + "," + ((int)taskDB.TaskType).ToString() + "," + taskDB.LastUpdateTime.ToString();
        base.SendRequest(data);
    }

}