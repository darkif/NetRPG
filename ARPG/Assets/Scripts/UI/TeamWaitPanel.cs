using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamWaitPanel : MonoBehaviour {

    private Button cancelBtn;
    private Text content;
    private Text timer;

    public static TeamWaitPanel _instance;

    private bool isSync = false;
    private string msg = "";

    private CancelMultiPlayRequest cancelPlayRequest;

    private void Awake()
    {
        _instance = this;

        cancelBtn = transform.Find("CancelBtn").GetComponent<Button>();
        cancelBtn.onClick.AddListener(OnCancelButtonClick);
        content = transform.Find("Text").GetComponent<Text>();
        timer = transform.Find("Timer").GetComponent<Text>();

        cancelPlayRequest = GetComponent<CancelMultiPlayRequest>();

        timer.gameObject.SetActive(false);
        this.transform.localScale = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isSync)
        {
            isSync = false;
            ClosePanel();
        }

        if (msg != "")
        {
            print(msg);
            content.text = "进入副本倒计时....";
            ShowTimer(msg);
            msg = "";
        }

    }

    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
        transform.DOScale(1, 0.4f);
    }

    void OnCancelButtonClick()
    {
        cancelPlayRequest.SendRequest();      
    }

    void ClosePanel()
    {
        Tweener tweener = transform.DOScale(0, 0.4f);
        tweener.OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
        OnlinePanel._instance.ShowPanel();
    }

    void ShowTimer(string num)
    {
        timer.transform.localScale = new Vector3(3, 3, 3);
        Color tempColor = timer.color;
        tempColor.a = 1;
        timer.color = tempColor;
        timer.gameObject.SetActive(true);
        timer.text = num;
        timer.transform.DOScale(1, 0.3f).SetDelay(0.3f);
        timer.DOFade(0, 0.3f).SetDelay(0.3f).OnComplete(() => { timer.gameObject.SetActive(false); });
    }

    public void OnResponseToCancelRequest(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            isSync = true;
        }
    }

    public void OnResponseToShowTimer(int num)
    {
        msg = num.ToString();
    }

}
