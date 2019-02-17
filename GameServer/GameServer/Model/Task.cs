using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    public enum TaskState
    {
        NoStart=0,
        Accpet=1,
        Complete=2,
        Reward=3
    }

    public enum TaskType
    {
        Main=0,
        Reward=1,
        Daily=2
    }


    public class Task
    {
        public Task(int id,int taskId,TaskState taskState,TaskType taskType,DateTime dateTime)
        {
            this.Id = id;
            this.TaskId = taskId;
            this.TaskState = taskState;
            this.TaskType = taskType;
            this.LastUpdateTime = dateTime;
        }

        public Task(int taskId, TaskState taskState, TaskType taskType, DateTime dateTime)
        {
            this.TaskId = taskId;
            this.TaskState = taskState;
            this.TaskType = taskType;
            this.LastUpdateTime = dateTime;
        }


        public int Id { get; set; }
        public int TaskId { get; set; }
        public TaskState TaskState { get; set; }
        public TaskType TaskType { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
