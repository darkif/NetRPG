using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneBar : MonoBehaviour {

    public static LoadSceneBar _instance;

    private Slider slider;
    private Text num;
    private bool isAsync = false;
    private AsyncOperation ao;

    private void Awake()
    {
        _instance = this;

        slider = transform.Find("Slider").GetComponent<Slider>();
        num = transform.Find("Slider/num").GetComponent<Text>();

        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        //场景加载进度条
        if (isAsync)
        {
            slider.value = ao.progress;
            num.text = slider.value * 100 + "%";
        }
    }

    public void ShowPanel(AsyncOperation ao)
    {
        this.gameObject.SetActive(true);
        isAsync = true;
        this.ao = ao;
    }

}
