using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class RegisterPanel : MonoBehaviour {

    public GameObject LoginPanel;
    public InputField username;
    public InputField password;
    public InputField confirmPwd;
    public Button backBtn;
    public Button registerBtn;
    public Text msg;

    private string un = "";
    private string pwd = "";
    private string repeatPwd = "";

    private WaitForSeconds showTime = new WaitForSeconds(0.3f);

    private string message = "";
    private RegisterRequest registerRequest;
    private bool isRegisterSuccess = false;

    // Use this for initialization
    void Start () {
        backBtn.onClick.AddListener(OnBackButtonClick);
        registerBtn.onClick.AddListener(OnRegisterButtonClick);
        registerRequest = GetComponent<RegisterRequest>();
    }

    private void Update()
    {
        if (message != "")
        {
            StartCoroutine(ShowMessage(message));
            message = "";
            if (isRegisterSuccess)
            {
                OnBackButtonClick();
                LoginPanel.GetComponent<LoginPanel>().username.text = un;
                LoginPanel.GetComponent<LoginPanel>().password.text = pwd;
                isRegisterSuccess = false;
            }  
        }   
    }

    private void OnBackButtonClick()
    {
        Tweener tweener = transform.DOScale(0, 0.3f);
        tweener.OnComplete<Tweener>(() =>
        {
            this.gameObject.SetActive(false);
            LoginPanel.SetActive(true);
            LoginPanel.transform.DOScale(1, 0.3f);
        });
    }

    private void OnRegisterButtonClick()
    {
        un = username.text;
        pwd = password.text;
        repeatPwd = confirmPwd.text;
        if (un == "")
        {
            StartCoroutine(ShowMessage("用户名不能为空"));
            return;
        }
        if (pwd == "")
        {
            StartCoroutine(ShowMessage("密码不能为空"));
            return;
        }
        if(pwd != repeatPwd)
        {
            StartCoroutine(ShowMessage("两次密码不一致"));
            return;
        }
        //账号密码发送到服务器 注册到数据库
        registerRequest.SendRequest(un, pwd);
    }


    public void OnRegisterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            message = "注册成功";
            isRegisterSuccess = true;
        }
        else
        {
            message = "已存在相同账号";
            isRegisterSuccess = false;
        }
    }

    IEnumerator ShowMessage(string value)
    {
        msg.gameObject.SetActive(true);
        msg.text = value;
        yield return showTime;
        msg.text = "";
        msg.gameObject.SetActive(false);
    }

}
