using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float gravity = 0.5f;
    public float speed = 3;

    //private Rigidbody rg;
    private Animator anim;

    private CharacterController cc;

    private Vector3 moveDirection = Vector3.zero;

    public bool isCanControl = false;//表示是否可以用键盘控制

    private Vector3 lastPos = Vector3.zero;
    private Vector3 lastEulerAngles = Vector3.zero;
    private SyncTransformRequest syncTransformRequest;
    private Player player;

    private bool isMove = false;

    // Use this for initialization
    void Start () {
        //rg = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();

        player = GetComponent<Player>();
        
        if (GameController._instance.battleType==BattleType.Team && isCanControl)
        {
            gameObject.AddComponent<SyncTransformRequest>();
            syncTransformRequest = GetComponent<SyncTransformRequest>();
            InvokeRepeating("SendSyncPosAndRotation", 1.5f,1f/60);          
        }
    }
	
	void FixedUpdate () {
        if (!isCanControl)
            return;

        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Die"))
            return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //Vector3 velocity = rg.velocity;

        if (!cc.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            cc.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
            {
                if (anim.GetCurrentAnimatorStateInfo(1).IsName("EmptyState"))
                {
                    //rg.velocity = new Vector3(h * speed, velocity.y, v * speed);
                    Vector3 move = new Vector3(h, transform.position.y, v);
                    cc.Move(move * speed * Time.deltaTime);
                    transform.LookAt(transform.position + new Vector3(h, 0, v));
                    anim.SetBool("run", true);
                }
                else
                {
                    //rg.velocity = new Vector3(0, velocity.y, 0);
                    anim.SetBool("run", false);
                }
            }
            else
            {
                //rg.velocity = new Vector3(0, velocity.y,0);
                anim.SetBool("run", false);
            }
        }
    }


    //发送同步位置和旋转
    void SendSyncPosAndRotation()
    {
        Vector3 position = transform.position;
        Vector3 eulerAngles = transform.eulerAngles;
        if(lastPos != position || eulerAngles!=lastEulerAngles || isMove != anim.GetBool("run"))
        {
            lastPos = position;
            lastEulerAngles = eulerAngles;
            isMove = anim.GetBool("run");
            //发送请求
            syncTransformRequest.SendRequest(player.id, position, eulerAngles, isMove);
        }
    }

}
