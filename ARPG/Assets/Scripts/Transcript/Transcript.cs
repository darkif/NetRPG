using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transcript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        OnlinePanel._instance.ShowPanel();
    }

    private void OnTriggerExit(Collider other)
    {
        OnlinePanel._instance.ShowPanel();
    }

}
