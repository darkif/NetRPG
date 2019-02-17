using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnlinePanel : MonoBehaviour {

    public static OnlinePanel _instance;

    private Button soloBtn;
    private Button multiBtn;

    private void Awake()
    {
        _instance = this;

        soloBtn = transform.Find("soloPlay").GetComponent<Button>();
        multiBtn = transform.Find("multiPlay").GetComponent<Button>();


        soloBtn.onClick.AddListener(OnSoloButtonClick);
        multiBtn.onClick.AddListener(OnMultiButtonClick);

        transform.localScale = Vector3.zero;
        this.gameObject.SetActive(false);

    }

    void OnSoloButtonClick()
    {
        GameController._instance.battleType = BattleType.Solo;
        if (TaskManager._instance.CurTask != null)
            GameController._instance.taskId = TaskManager._instance.CurTask.Id;
        this.gameObject.SetActive(false);
        AsyncOperation ao = SceneManager.LoadSceneAsync(2);
        LoadSceneBar._instance.ShowPanel(ao);
    }

    void OnMultiButtonClick()
    {
        GameController._instance.battleType = BattleType.Team;
        if(TaskManager._instance.CurTask != null)
            GameController._instance.taskId = TaskManager._instance.CurTask.Id;
        this.gameObject.SetActive(false);

        TeamWaitPanel._instance.ShowPanel();
        //AsyncOperation ao = SceneManager.LoadSceneAsync(2);
        //LoadSceneBar._instance.ShowPanel(ao);
    }


    public void ShowPanel()
    {
        if (gameObject.activeInHierarchy)
        {
            Tweener tweener = transform.DOScale(0, 0.4f);
            tweener.OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
        }
        else
        {
            this.gameObject.SetActive(true);
            transform.DOScale(1, 0.4f);
        }
    }

}
