using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class LoginPanel : MonoBehaviour {

    public Button loginBtn;
    public Button registerBtn;
    public GameObject registerPanel;

    public InputField username;
    public InputField password;
    public Text msg;

    private WaitForSeconds showTime = new WaitForSeconds(0.3f);

    private LoginRequest LoginRequest;
    private string message = "";

    public RoleSelectPanel roleSelectPanel;

    // Use this for initialization
    void Start () {
        loginBtn.onClick.AddListener(OnLoginButtonClick);
        registerBtn.onClick.AddListener(OnRegisterButtonClick);
        registerPanel.transform.localScale = Vector3.zero;
        registerPanel.SetActive(false);
        msg.gameObject.SetActive(false);

        LoginRequest = GetComponent<LoginRequest>();
    }

    private void Update()
    {
        if (message!="")
        {
            if (message == "账号或密码错误")
            {
                StartCoroutine(ShowMessage(message));
                message = "";
                return;
            }

            StartCoroutine(ShowMessage(message));
            message = "";
            Tweener tweener = transform.DOScale(0, 0.3f);
            tweener.OnComplete<Tweener>(() => {
                this.gameObject.SetActive(false);
                roleSelectPanel.gameObject.SetActive(true);
                if (GameFacade.Instance.GetRoleData().RoleId != -1)
                {
                    roleSelectPanel.selectRolePanel.SetActive(true);
                    roleSelectPanel.setSelectPanelRoleDate(GameFacade.Instance.GetRoleData().Name, GameFacade.Instance.GetRoleData().Level);
                }
                else
                {
                    roleSelectPanel.createRolePanel.SetActive(true);
                }
                roleSelectPanel.transform.DOScale(1, 0.3f);
            });
        }
    }


    private void OnLoginButtonClick()
    {
        string un = username.text;
        string pwd = password.text;
        if(un == "")
        {
            StartCoroutine(ShowMessage("用户名不能为空"));
            return;
        }
        if(pwd == "")
        {
            StartCoroutine(ShowMessage("密码不能为空"));
            return;
        }

        //通过服务器验证数据库中是否有该账号密码
        LoginRequest.SendRequest(un, pwd);
    }

    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            //登陆成功选择角色
            message = "登陆成功";
        }
        else
        {
            message = "账号或密码错误";
        }
    }

    private void OnRegisterButtonClick()
    {
        Tweener tweener = transform.DOScale(0, 0.3f);
        tweener.OnComplete<Tweener>(() => {
            this.gameObject.SetActive(false);
            registerPanel.SetActive(true);
            registerPanel.transform.DOScale(1, 0.3f);
        });
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
