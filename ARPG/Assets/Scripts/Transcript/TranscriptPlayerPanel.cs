using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranscriptPlayerPanel : MonoBehaviour {

    private Text levelLabel;
    private Scrollbar hpSlider;
    private Scrollbar expSlider;
    private Text hpNum;
    private Text expNum;

    private Transform player;

    private void Awake()
    {
        levelLabel = transform.Find("Level").GetComponent<Text>();
        hpSlider = transform.Find("HPbar").GetComponent<Scrollbar>();
        expSlider = transform.Find("Expbar").GetComponent<Scrollbar>();
        hpNum = transform.Find("HPbar/HpNum").GetComponent<Text>();
        expNum = transform.Find("Expbar/ExpNum").GetComponent<Text>();

        PlayerInfo._instance.OnPlayerInfoChanged += OnPlayerInfoChanged;
    }

    private void Start()
    {
        UpdateShow();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player.GetComponent<PlayerAttack>().OnPlayerHpChange += OnPlayerHpChange;
    }

    void OnPlayerInfoChanged(InfoType infoType)
    {
        if (infoType == InfoType.Level || infoType == InfoType.HP || infoType == InfoType.Exp || infoType == InfoType.All)
        {
            UpdateShow();
        }
    }

    //更新显示hp exp
    void UpdateShow()
    {
        float hpValue = (float)PlayerInfo._instance.HP / PlayerInfo._instance.maxHp;
        float expValue = (float)PlayerInfo._instance.Exp / PlayerInfo._instance.GetExpByLevel(PlayerInfo._instance.Level);

        if (PlayerInfo._instance.Exp == 0)
        {
            expNum.text = "0%";
            expSlider.size = 0;
        }
        else
        {
            expSlider.size = (float)expValue;
            expValue *= 100;
            int expV = (int)expValue;
            expNum.text = expV.ToString() + "%";
        }

        hpSlider.size = (float)hpValue;
        hpValue *= 100;
        int hpV = (int)hpValue;

        hpNum.text = hpV.ToString() + "%";
        levelLabel.text = PlayerInfo._instance.Level.ToString();
    }

    void OnPlayerHpChange(int hp)
    {
        float hpValue = (float)hp / PlayerInfo._instance.maxHp;
        hpSlider.size = (float)hpValue;
        hpValue *= 100;
        int hpV = (int)hpValue;
        hpNum.text = hpV.ToString() + "%";
    }

    private void OnDestroy()
    {
        if(player!=null)
            player.GetComponent<PlayerAttack>().OnPlayerHpChange -= OnPlayerHpChange;
    }

}
