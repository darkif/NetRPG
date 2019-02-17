using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public List<GameObject> enemyList;

    public static EnemyManager _instance;

    private void Awake()
    {
        _instance = this;
    }

}
