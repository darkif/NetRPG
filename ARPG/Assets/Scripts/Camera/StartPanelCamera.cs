using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanelCamera : MonoBehaviour {

    public float moveSpeed = 1.0f;

    private float endZ = -1.208f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < endZ)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime,Space.World);
        }
    }
}
