using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour {

    public static GameOverPanel _instance;

    private Button backToMainBtn;
    private Text resText;

    private void Awake()
    {
        _instance = this;
        backToMainBtn = transform.Find("BG/BackToMain").GetComponent<Button>();
        resText = transform.Find("BG/ResText").GetComponent<Text>();

        backToMainBtn.onClick.AddListener(OnBackToMainClick);

        transform.localScale = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    //返回城镇
    void OnBackToMainClick()
    {
        this.gameObject.SetActive(false);
        Destroy(GameController._instance.gameObject);
        AsyncOperation ao = SceneManager.LoadSceneAsync(1);
        LoadSceneBar._instance.ShowPanel(ao);
    }

    public void ShowPanel(string message)
    {
        this.gameObject.SetActive(true);
        resText.text = message;
        this.transform.DOScale(1, 0.4f);
    }

}
