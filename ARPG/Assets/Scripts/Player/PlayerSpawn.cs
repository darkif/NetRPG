using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    public Transform[] playerSpawnPosArray;

    private void Awake()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (GameController._instance.battleType == BattleType.Solo)
        {
            //加载一个角色
            //根据角色索引创建对应的角色
            if (GameFacade.Instance.GetRoleData().RoleId == 0)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("Player/Player"), playerSpawnPosArray[0].position, Quaternion.identity);
                GameController._instance.player = go;
            }
            
        }
        else if(GameController._instance.battleType == BattleType.Team)
        {
            for(int i = 0; i < 2; i++)
            {
                GameObject go;
                if (GameFacade.Instance.GetRoleData().RoleId == 0)
                {
                    go = Instantiate(Resources.Load<GameObject>("Player/Player"), playerSpawnPosArray[i].position, Quaternion.identity);

                    //判断是否是当前客户端可以控制的角色
                    if (GameFacade.Instance.GetRoleData().Id == GameController._instance.TeamRoleId[i])
                    {
                        go.GetComponent<PlayerMove>().isCanControl = true;
                        GameController._instance.player = go;                      
                    }
                    else
                    {
                        go.GetComponent<PlayerMove>().isCanControl = false;
                    }
                   
                    go.GetComponent<Player>().id = GameController._instance.TeamRoleId[i];
                    GameController._instance.playerDict.Add(go.GetComponent<Player>().id, go);
                }
            }
        }
    }

}
