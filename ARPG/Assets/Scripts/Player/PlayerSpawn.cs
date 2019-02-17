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
            Instantiate(Resources.Load<GameObject>("Player/Player"), playerSpawnPosArray[0].position, Quaternion.identity);
        }
        else if(GameController._instance.battleType == BattleType.Team)
        {

        }
    }

}
