using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerPanel : MonoBehaviour {

    public PlayerInfoPanel playerInfoPanel;
    public float posY = 519;
    private Text levelLabel;
    private Scrollbar hpSlider;
    private Scrollbar expSlider;
    private Text hpNum;
    private Text expNum;
    private Button infoBtn;

    private void Awake()
    {
        levelLabel = transform.Find("Level").GetComponent<Text>();
        hpSlider = transform.Find("HPbar").GetComponent<Scrollbar>();
        expSlider = transform.Find("Expbar").GetComponent<Scrollbar>();
        hpNum = transform.Find("HPbar/HpNum").GetComponent<Text>();
        expNum = transform.Find("Expbar/ExpNum").GetComponent<Text>();
        infoBtn = transform.Find("HeadPortrait").GetComponent<Button>();

        infoBtn.onClick.AddListener(OnInfoClick);

       PlayerInfo._instance.OnPlayerInfoChanged += OnPlayerInfoChanged;
    }

    void OnInfoClick()
    {
        if (!playerInfoPanel.gameObject.activeInHierarchy)
        {
            playerInfoPanel.gameObject.SetActive(true);
            Tweener tweener = playerInfoPanel.transform.DOLocalMoveY(0, 0.4f);
        }
        else
        {
            Tweener tweener = playerInfoPanel.transform.DOLocalMoveY(posY, 0.4f);
            tweener.OnComplete(() =>
            {
                playerInfoPanel.gameObject.SetActive(false);
            });
        }
       
    }

    void OnPlayerInfoChanged(InfoType infoType)
    {
        if(infoType== InfoType.Level || infoType == InfoType.HP || infoType == InfoType.Exp || infoType == InfoType.All)
        {
            UpdateShow();
        }
    }

    //更新显示
    void UpdateShow()
    {
        float hpValue = (float)PlayerInfo._instance.HP / PlayerInfo._instance.maxHp;
        float expValue = (float)PlayerInfo._instance.Exp / PlayerInfo._instance.GetExpByLevel(PlayerInfo._instance.Level);
   
        if(PlayerInfo._instance.Exp == 0)
        {
            expNum.text =  "0%";
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

    private void OnDestroy()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= OnPlayerInfoChanged;
    }

}
