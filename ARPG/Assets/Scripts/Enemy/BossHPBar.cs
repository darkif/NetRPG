using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour {
    private Slider hpSlider;

    public static BossHPBar _instance;

    private void Awake()
    {
        _instance = this;
        hpSlider = GetComponent<Slider>();
        hpSlider.value = 1.0f;
    }

    public void ChangedBossHp(int hp,int maxHp)
    {
        hpSlider.value = hp / (float)maxHp;
    }

}
