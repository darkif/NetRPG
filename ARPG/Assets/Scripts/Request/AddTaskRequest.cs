using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class AddTaskRequest : BaseRequest {

    public override void Start()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.UpdateOrAddTask;
        base.Start();
    }

    public void SendRequest(TaskDB taskDB)
    {
        print("updateTask");
        string data = taskDB.TaskId.ToString() + "," + ((int)taskDB.TaskState).ToString() + "," + ((int)taskDB.TaskType).ToString() + "," + taskDB.LastUpdateTime.ToString();
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        
    }

}