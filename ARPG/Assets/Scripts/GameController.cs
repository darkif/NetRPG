using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleType
{
    Solo,
    Team,
    None
}

public class GameController : MonoBehaviour {

    public static GameController _instance;

    public Transform playerSpawn;
    private RoleData role;

    public BattleType battleType = BattleType.None;
    public int taskId;

    private void Awake()
    {
        _instance = this;

        role = GameFacade.Instance.GetRoleData();
        if (role.RoleId == 0)
        {
            GameObject player = Instantiate(Resources.Load<GameObject>("Player/VillagePlayer"), playerSpawn.position, Quaternion.identity); ;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnPlayerDead()
    {
        GameOverPanel._instance.ShowPanel("作战失败");
    }

    public void OnBossDead(int taskId)
    {
        GameOverPanel._instance.ShowPanel("作战成功");
        //完成达到boss的任务
        Task task = null;
        TaskManager._instance.accpetTaskList.TryGetValue(taskId, out task);
        if (task != null)
        {
            if(task.TaskProgress!=TaskState.Complete || task.TaskProgress != TaskState.Reward)
            {
                task.TaskDB.TaskState = TaskState.Complete;
                //更新任务
                TaskManager._instance.AddTaskRequest.SendRequest(task.TaskDB);
            }
            
        }
    }

}
