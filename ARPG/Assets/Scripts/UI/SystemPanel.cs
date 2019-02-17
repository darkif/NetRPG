using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SystemPanel : MonoBehaviour {

    public Sprite openSound;
    public Sprite closeSound;

    private Button soundBtn;
    private Button aboutUs;
    private Button exitBtn;
    private Button backToLoginBtn;

    private Image soundImage;

    private Button closeBtn;
    private bool isSoundOpen = true;

    private void Awake()
    {
        soundBtn = transform.Find("BG/Sound").GetComponent<Button>();
        aboutUs = transform.Find("BG/AboutUs").GetComponent<Button>();
        exitBtn = transform.Find("BG/ExitGame").GetComponent<Button>();
        backToLoginBtn = transform.Find("BG/BackToLogin").GetComponent<Button>();
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();

        soundImage = soundBtn.GetComponent<Image>();

        soundBtn.onClick.AddListener(OnSoundButtonClick);
        aboutUs.onClick.AddListener(OnAboutUsButtonClick);
        exitBtn.onClick.AddListener(OnExitButtonClick);
        backToLoginBtn.onClick.AddListener(OnBackToLoginButtonClick);
        closeBtn.onClick.AddListener(OnCloseButtonClick);

        transform.localScale = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    //声音控制
    void OnSoundButtonClick()
    {
        if (isSoundOpen)
        {
            isSoundOpen = false;
            soundImage.sprite = closeSound;
            //TODO关闭声音

        }
        else
        {
            isSoundOpen = true;
            soundImage.sprite = openSound;
            //TODO开启声音

        }
    }

    //关于我们
    void OnAboutUsButtonClick()
    {

    }

    //退出游戏
    void OnExitButtonClick()
    {
        Application.Quit();
    }

    //返回登陆界面
    void OnBackToLoginButtonClick()
    {
        Destroy(GameFacade.Instance.gameObject);
        Destroy(GameController._instance.gameObject);
        AsyncOperation ao = SceneManager.LoadSceneAsync(0);
        LoadSceneBar._instance.ShowPanel(ao);
    }

    public void ShowPanel()
    {
        if (gameObject.activeInHierarchy)
        {
            OnCloseButtonClick();
        }
        else
        {
            this.gameObject.SetActive(true);
            transform.DOScale(1, 0.4f);
        }
    }

    void OnCloseButtonClick()
    {
        Tweener tweener = transform.DOScale(0, 0.4f);
        tweener.OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }

}
