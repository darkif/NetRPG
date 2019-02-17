using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GetTaskRequest : BaseRequest {

    public override void Start()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.GetTask;
        base.Start();
    }

    //发起请求
    public override void SendRequest()
    {
        base.SendRequest("r");
    }


    //处理服务器返回的数据
    public override void OnResponse(string data)
    {
        string[] strs = data.Split('|');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        if (returnCode == ReturnCode.Success)
        {
            
            string[] taskArray = strs[1].Split('-');
            List<TaskDB> dbList = new List<TaskDB>();
            foreach (string s in taskArray)
            {
                if (s == "" || s==" ")
                    break;

                string[] proArray = s.Split(',');

                TaskDB taskDB = new TaskDB();
                taskDB.TaskId = int.Parse(proArray[0]);
                taskDB.TaskState = (TaskState)int.Parse(proArray[1]);
                taskDB.TaskType = (TaskType)int.Parse(proArray[2]);
                dbList.Add(taskDB);
            }
            TaskManager._instance.OnResponseToGetTask(dbList);
        }
        else
        {
            TaskManager._instance.OnResponseToGetTask(null);
        }
    }
}
