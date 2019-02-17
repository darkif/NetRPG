using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDB{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public TaskState TaskState { get; set; }
    public TaskType TaskType { get; set; }
    public DateTime LastUpdateTime { get; set; }
}
