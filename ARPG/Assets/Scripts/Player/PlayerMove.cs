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

    // Use this for initialization
    void Start () {
        //rg = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        cc = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
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

}
