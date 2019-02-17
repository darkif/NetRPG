using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using UnityEngine.SceneManagement;

public class RoleSelectPanel : MonoBehaviour {
    public GameObject createRolePanel;
    public GameObject selectRolePanel;

    private InputField nameInput;
    private Button createPanelBtn;

    public Text level;
    public Text name;
    private Button selectPanelBtn;

    private UpdateRoleInfoRequest updateRoleInfoRequest;

    private bool createSuccess;

    private void Start()
    {
        nameInput = createRolePanel.GetComponentInChildren<InputField>();
        createPanelBtn= createRolePanel.GetComponentInChildren<Button>();
        selectPanelBtn = selectRolePanel.GetComponentInChildren<Button>();
        createPanelBtn.onClick.AddListener(OnCreateRoleEnterGameBtn);
        selectPanelBtn.onClick.AddListener(OnSelectRoleEnterGameBtn);

        updateRoleInfoRequest = GetComponent<UpdateRoleInfoRequest>();
    }

    private void Update()
    {
        if (createSuccess)
        {
            createSuccess = false;
            LoadScene();
        }
    }

    public void setSelectPanelRoleDate(string name,int level)
    {
        this.name.text = name;
        this.level.text = "LV "+ level.ToString();
    }

    public void OnCreateRoleEnterGameBtn()
    {
        string name = nameInput.text;
        RoleData roleData = new RoleData(name, 1, 0,10,5,1000,100,0,100);
        GameFacade.Instance.SetRoleData(roleData);
        //把角色等级、名字、id发给服务器，然后跳转场景
        updateRoleInfoRequest.SendRequest(roleData.Name, roleData.Level, roleData.RoleId, roleData.Exp, roleData.Coin, roleData.Atk, roleData.Def, roleData.Hp,roleData.MaxHp); ;
    }

    public void OnSelectRoleEnterGameBtn()
    {
        //跳转场景
        LoadScene();
    }

    void LoadScene()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(1);
        LoadSceneBar._instance.ShowPanel(ao);
    }

    public void OnUpdateRoleResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            createSuccess = true;
        }
    }

}
