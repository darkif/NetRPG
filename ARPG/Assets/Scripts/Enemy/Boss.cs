using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public static Boss _instance;

    public int hp = 500;
    public int maxHp = 500;
    public float viewAngle = 50;    //视野范围
    public float rotateSpeed = 1;
    public float attackDistance = 2;
    public float moveSpeed = 3;
    public float atkTimeInterval = 1;   //攻击间隔
    private float atkTimer = 0;
    private bool isAttacking = false;   //是否正在进行攻击

    private int atkIndex = 0;

    public int[] atkArray;

    private Transform player;
    private Animation anim;
    private CharacterController cc;

    private Vector3 lastPos = Vector3.zero;
    private Vector3 lastEulerAnges = Vector3.zero;

    private void Awake()
    {
        _instance = this;
    }

    private SyncBossTransformRequest syncBossTransformRequest;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animation>();
        cc = GetComponent<CharacterController>();

        if (GameController._instance.battleType == BattleType.Team)
        {
            syncBossTransformRequest = GetComponent<SyncBossTransformRequest>();
            InvokeRepeating("SendSyncRequest", 0, 1f/30);
        }
    }

    // Update is called once per frame
    void Update() {
        if (isAttacking || hp <= 0)
            return;

        Vector3 playerPos = player.position;
        playerPos.y = transform.position.y;
        float angle = Vector3.Angle(playerPos - transform.position, transform.forward);
        if (angle < viewAngle / 2)
        {
            //在攻击视野内
            float distance = Vector3.Distance(playerPos, transform.position);
            if (distance <= attackDistance)   //可以进行攻击
            {
                if (!isAttacking)
                {
                    anim.CrossFade("idle");
                    atkTimer += Time.deltaTime;
                    if (atkTimer > atkTimeInterval)    //开始攻击
                    {
                        atkTimer = 0;
                        Attack();
                    }
                }
            }
            else//向玩家移动
            {
                anim.CrossFade("walk");
                cc.Move(transform.forward.normalized * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            //再攻击视野之外 进行转向
            Quaternion targetRotation = Quaternion.LookRotation(playerPos - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            anim.Play("walk");
        }

    }


    void Attack()
    {
        isAttacking = true;
        atkIndex = Random.Range(0, 3);
        atkIndex += 1;
        anim.CrossFade("atk" + atkIndex);
    }

    void EndAttack()
    {
        isAttacking = false;
    }

    void PlayAttack1()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= attackDistance)
        {
            //player.GetComponent<Player>().TakeDamage(atkArray[0]);
        }
    }

    //参数：1.受到伤害 2.受击后退距离
    void TakeDamage(string args)
    {

        if (hp <= 0)
            return;

        ComboPanel._instanec.ComboPlus();

        //减少生命值
        string[] strs = args.Split(',');
        int damage = int.Parse(strs[0]);
        hp -= damage;

        BossHPBar._instance.ChangedBossHp(hp, maxHp);

        if (hp <= 0)
        {
            anim.Play("die");
            GameController._instance.OnBossDead(1001);
        }
            

        //播放受击动画
        //anim.Play("hit");

        //后退
        float backMove = int.Parse(strs[1]);
        if(backMove > 0.1)
        {
            transform.DOBlendableLocalMoveBy(-transform.forward * moveSpeed, 0.3f);
        }
    }


    void SendSyncRequest()
    {
        if(transform.position!=lastPos || transform.eulerAngles != lastEulerAnges)
        {
            lastEulerAnges = transform.eulerAngles;
            lastPos = transform.position;
            syncBossTransformRequest.SendRequest(lastPos, lastEulerAnges);
        }
    }

}
