using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerInfoPanel : MonoBehaviour {

    public float posY;

    private Text levelLabel;
    private Text atkLabel;
    private Text defLabel;
    private Text coinLabel;
    private Scrollbar expSlider;
    private Button closeBtn;
    private Text expNum;
    private Text nameLabel;

    private void Awake()
    {
        levelLabel = transform.Find("Level").GetComponent<Text>();
        atkLabel = transform.Find("Attack/AtkNum").GetComponent<Text>();
        defLabel = transform.Find("Def/DefNum").GetComponent<Text>();
        coinLabel = transform.Find("CoinNum").GetComponent<Text>();
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        expSlider = transform.Find("Expbar").GetComponent<Scrollbar>();
        expNum = transform.Find("Expbar/ExpNum").GetComponent<Text>();
        nameLabel= transform.Find("Name/Label").GetComponent<Text>();

        closeBtn.onClick.AddListener(OnCloseClick);
        PlayerInfo._instance.OnPlayerInfoChanged += OnPlayerInfoChanged;

        this.gameObject.SetActive(false);
    }

    private void OnCloseClick()
    {
        Tweener tweener = transform.DOLocalMoveY(posY, 0.4f);
        tweener.OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
        
    }

    void OnPlayerInfoChanged(InfoType infoType)
    {
        if (infoType != InfoType.HP)
        {
            UpdateShow();
        }
    }

    //更新显示
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

        hpValue *= 100;
        int hpV = (int)hpValue;

        levelLabel.text = PlayerInfo._instance.Level.ToString();
        nameLabel.text = PlayerInfo._instance.Name.ToString();
        coinLabel.text = PlayerInfo._instance.Coin.ToString();
        atkLabel.text = PlayerInfo._instance.Atk.ToString();
        defLabel.text = PlayerInfo._instance.Def.ToString();
    }

    private void OnDestroy()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= OnPlayerInfoChanged;
    }

}
