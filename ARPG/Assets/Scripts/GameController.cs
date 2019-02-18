using System;
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

    private bool isHost = false;

    public bool IsHost
    {
        get
        {
            return isHost;
        }

        set
        {
            isHost = value;
        }
    }

    public int[] TeamRoleId = new int[3];

    [HideInInspector]
    public GameObject player;

    [HideInInspector]
    public Dictionary<int, GameObject> playerDict = new Dictionary<int, GameObject>();

    private bool isSyncTransform = false;
    private int playerid = -1;
    private Vector3 pos = Vector3.zero;
    private Vector3 eulerAngles = Vector3.zero;

    private bool isSyncMoveAnim = false;
    private bool isMove = false;
    private int moveId = -1;

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


    private void FixedUpdate()
    {
        if (isSyncTransform)
        {
            isSyncTransform = false;
            OnSyncPositionAndRotation(playerid, pos, eulerAngles,isMove);          
        }
        //if (isSyncMoveAnim)
        //{
        //    isSyncMoveAnim = false;
        //    OnSyncMoveAnim(moveId, isMove);
        //}
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
                task.TaskDB.LastUpdateTime = DateTime.Now;
                //更新任务
                TaskManager._instance.AddTaskRequest.SendRequest(task.TaskDB);
            }
            
        }
    }


    public void OnResponseToGetTeamInfo(string data)
    {
        string[] strs = data.Split(',');
        int i = 0;
        foreach(string s in strs)
        {
            if (s == "" || s == " ")
                continue;
            TeamRoleId[i] = int.Parse(s);
            i++;
        }
    }

    void OnSyncPositionAndRotation(int playerid, Vector3 pos,Vector3 eulerAngles,bool isMove)
    {
        GameObject go;
        playerDict.TryGetValue(playerid, out go);
        if (go != null)
        {
            go.transform.position = pos;
            go.transform.eulerAngles = eulerAngles;
            go.GetComponent<Animator>().SetBool("run", isMove);
        }
    }

    void OnSyncMoveAnim(int playerid, bool isMove)
    {
        GameObject go;
        playerDict.TryGetValue(playerid, out go);
        if (go != null)
        {
            go.GetComponent<Animator>().SetBool("run", isMove);
        }
    }


    public void OnResponseToSyncTransformRequest(int playerid,Vector3 pos,Vector3 eulerAngles,bool isMove)
    {
        isSyncTransform = true;
        this.playerid = playerid;
        this.pos = pos;
        this.eulerAngles = eulerAngles;
        this.isMove = isMove;
    }

}
