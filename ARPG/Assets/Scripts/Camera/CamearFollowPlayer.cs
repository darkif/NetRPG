using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamearFollowPlayer : MonoBehaviour {

    public Vector3 offset;
    public float smooth = 3.0f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate () {
        Vector3 targetPos = player.position + offset;
        transform.position = targetPos;
        transform.LookAt(player);
	}
}
