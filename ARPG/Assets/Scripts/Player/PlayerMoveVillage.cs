using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveVillage : MonoBehaviour {
    public float speed = 3;

    private Rigidbody rg;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 velocity = rg.velocity;

        if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
        {
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("EmptyState"))
            {
                rg.velocity = new Vector3(h * speed, velocity.y, v * speed);
                transform.LookAt(transform.position + new Vector3(h, 0, v));
                anim.SetBool("run", true);
            }
            else
            {
                rg.velocity = new Vector3(0, velocity.y, 0);
                anim.SetBool("run", false);
            }
        }
        else
        {
            rg.velocity = new Vector3(0, velocity.y, 0);
            anim.SetBool("run", false);
        }
    }
}
