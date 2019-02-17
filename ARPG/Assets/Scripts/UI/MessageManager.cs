using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageManager : MonoBehaviour {
    public static MessageManager _instance;

    private Text msg;

    private void Awake()
    {
        _instance = this;
        msg = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    public void ShowMessage(string message,float time)
    {
        gameObject.SetActive(true);
        msg.text = message;
        StartCoroutine(Show(time));
    }

    IEnumerator Show(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

}
