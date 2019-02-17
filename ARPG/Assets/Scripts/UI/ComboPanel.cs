using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ComboPanel : MonoBehaviour {

    public static ComboPanel _instanec;

    public float comboTime = 2;
    public int comboCount = 0;
    private float timer = 0;

    private Text comboNum;

    private void Awake()
    {
        _instanec = this;

        comboNum = transform.Find("num").GetComponent<Text>();
        this.gameObject.SetActive(false); 
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            this.gameObject.SetActive(false);
            comboCount = 0;
        }
            
    }

    public void ComboPlus()
    {
        this.gameObject.SetActive(true);
        timer = comboTime;
        comboCount++;
        comboNum.text = comboCount.ToString();
    }

}
