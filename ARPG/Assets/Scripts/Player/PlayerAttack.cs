using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//攻击范围
public enum AttackRange
{
    Forward,
    Around
}

public delegate void OnPlayerHpChangeEvent(int hp);

public class PlayerAttack : MonoBehaviour {

    private Animator anim;
    public float atkForwardDistance = 2;
    public float atkRangeDistance = 2;
    public int[] damageArray = new int[2] { 20, 30 };

    private int hp;
    public event OnPlayerHpChangeEvent OnPlayerHpChange;

    private Player player;
    private SyncPlayerAnimRequest syncPlayerAnimRequest;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        hp = PlayerInfo._instance.HP;
        player = GetComponent<Player>();
        //当前角色属于当前客户端
        if(GameController._instance.battleType==BattleType.Team)
        {
            gameObject.AddComponent<SyncPlayerAnimRequest>();
            syncPlayerAnimRequest = GetComponent<SyncPlayerAnimRequest>();
        }
    }


    public void OnAttackClick(ButtonType buttonType)
    {
        if (buttonType == ButtonType.normalAtk)
        {
            anim.SetTrigger("normalAtk");
            syncPlayerAnimRequest.SendRequest(player.id,"normalAtk");
        }
        else{
            anim.SetTrigger("skill" + (int)buttonType);
            syncPlayerAnimRequest.SendRequest(player.id, "skill" + (int)buttonType);
        }
    }

    //args参数
    // 0 攻击类型 normal skill1....
    // 1 effect name
    // 2 sound name
    // 3 move forward 攻击的时候是否前移
    void Attack(string args)
    {
        string[] strs = args.Split(',');
        string type = strs[0];
        string effectName = strs[1];
        //TODO播放特效
        string soundName = strs[2];
        //TODO播放声音
        float move = float.Parse(strs[3]);
        if (move > 0.1f)
        {
            //移动
            transform.DOBlendableLocalMoveBy(transform.forward * move, 0.3f);
        }

        List<GameObject> enemyList;
        if (type == "forward")
        {
            enemyList = GetEnemyInAttackRange(AttackRange.Forward);
            foreach(GameObject go in enemyList)
            {
                go.SendMessage("TakeDamage",damageArray[0]+",0");
            }
        }
        else if(type == "around")
        {
            enemyList = GetEnemyInAttackRange(AttackRange.Around);
            foreach (GameObject go in enemyList)
            {
                go.SendMessage("TakeDamage", damageArray[1] + ",0");
            }
        }
    }

    //得到在攻击范围内的敌人
    //TOD0：解决侧面不能打到boss 
    List<GameObject> GetEnemyInAttackRange(AttackRange attackRange)
    {
        List<GameObject> enemyList = new List<GameObject>();
        if (attackRange == AttackRange.Forward)
        {
            if (EnemyManager._instance.enemyList.Count == 0)
                return null;
            foreach (GameObject go in EnemyManager._instance.enemyList)
            {
                //转成玩家的局部坐标
                Vector3 pos = transform.InverseTransformDirection(go.transform.position);
                if (pos.z < 0)  //如果在玩家的前方
                {
                    float distance = Vector3.Distance(transform.position, go.transform.position);
                    if (distance < atkForwardDistance)
                    {
                        enemyList.Add(go);
                    }
                }
            }
        }
        else if (attackRange == AttackRange.Around)
        {

            foreach (GameObject go in EnemyManager._instance.enemyList)
            {
                float distance = Vector3.Distance(transform.position, go.transform.position);
                if (distance < atkRangeDistance)
                {
                    enemyList.Add(go);
                }

            }
        }

        return enemyList;
    }

    //受到伤害
    public void TakeDamage(int damage)
    {
        if (hp <= 0)
            return;
       
        this.hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            GameController._instance.OnPlayerDead();
            anim.SetTrigger("die");

            syncPlayerAnimRequest.SendRequest(player.id, "die");
        }
        else
        {
            anim.SetTrigger("takeDamage");

            syncPlayerAnimRequest.SendRequest(player.id, "takeDamage");
        }
        OnPlayerHpChange(hp);        
    }

}
