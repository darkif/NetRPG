using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPanel : MonoBehaviour {
    private Text coinNum;

    private void Awake()
    {
        coinNum = transform.Find("CoinNum").GetComponent<Text>();
        PlayerInfo._instance.OnPlayerInfoChanged += OnPlayerInfoChanged;
    }

    void OnPlayerInfoChanged(InfoType infoType)
    {
        if (infoType == InfoType.Coin || infoType == InfoType.All)
        {
            UpdateShow();
        }
    }

    //更新显示
    void UpdateShow()
    {
        coinNum.text = PlayerInfo._instance.Coin.ToString();
    }

    private void OnDestroy()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= OnPlayerInfoChanged;
    }

}
