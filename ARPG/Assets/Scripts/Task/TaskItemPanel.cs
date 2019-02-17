using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskItemPanel : MonoBehaviour {

    private Task task;

    private Image taskType;
    private Image icon;
    private Text taskName;
    private Text desc;
    private Text coinNum;
    private Button accpetBtn;
    private Text accpetBtnLabel;
    private Button rewardBtn;
    private Text rewardBtnLabel;

    private void Awake()
    {
        taskType = transform.Find("type").GetComponent<Image>();
        icon = transform.Find("icon").GetComponent<Image>();
        taskName = transform.Find("name").GetComponent<Text>();
        desc = transform.Find("desc").GetComponent<Text>();
        coinNum = transform.Find("coin/coinNum").GetComponent<Text>();
        accpetBtn = transform.Find("accpetBtn").GetComponent<Button>();
        rewardBtn = transform.Find("rewardBtn").GetComponent<Button>();
        accpetBtnLabel = accpetBtn.GetComponentInChildren<Text>();
        rewardBtnLabel = rewardBtn.GetComponentInChildren<Text>();

        accpetBtn.onClick.AddListener(OnAccpetTaskClick);
        rewardBtn.onClick.AddListener(OnRewardClick);

        rewardBtn.gameObject.SetActive(false);
    }

    public void SetTask(Task task)
    {
        this.task = task;
        task.OnTaskStateChanged += OnTaskStateChanged;

        UpdateShow();
    }

    //更新显示
    void UpdateShow()
    {
        switch (task.TaskType)
        {
            case TaskType.Main:
                taskType.sprite = Resources.Load<Sprite>("Task/Main");
                break;
            case TaskType.Reward:
                taskType.sprite = Resources.Load<Sprite>("Task/Reward");
                break;
            case TaskType.Daily:
                taskType.sprite = Resources.Load<Sprite>("Task/Daily");
                break;
        }

        icon.sprite = Resources.Load<Sprite>("Task/" + task.TaskIcon);
        taskName.text = task.TaskName;
        desc.text = task.TaskDesc;
        coinNum.text = "+ " + task.Coin.ToString();

        switch (task.TaskProgress)
        {
            case TaskState.NoStart:
                accpetBtnLabel.text = "接受任务";
                if(accpetBtn!=null && rewardBtn != null)
                {
                    accpetBtn.gameObject.SetActive(true);
                    rewardBtn.gameObject.SetActive(false);
                }
               
                break;
            case TaskState.Accpet:
                accpetBtnLabel.text = "进行中";
                if (accpetBtn != null && rewardBtn != null)
                {
                    accpetBtn.gameObject.SetActive(true);
                    rewardBtn.gameObject.SetActive(false);
                }
                
                break;
            case TaskState.Complete:
                if (accpetBtn != null && rewardBtn != null)
                {
                    accpetBtn.gameObject.SetActive(false);
                    rewardBtnLabel.text = "领取奖励";
                    rewardBtn.gameObject.SetActive(true);
                }
               
                break;
            case TaskState.Reward:
                if (accpetBtn != null && rewardBtn != null)
                {
                    accpetBtn.gameObject.SetActive(false);
                    rewardBtnLabel.text = "已完成";
                    rewardBtn.gameObject.SetActive(true);
                }
               
                break;
        }
    }


    void OnAccpetTaskClick()
    {
        TaskManager._instance.OnExcuteTask(task);
    }

    void OnRewardClick()
    {
        if (task.TaskProgress == TaskState.Complete)
        {
            //领取奖励
            PlayerInfo._instance.AddCoin(task.Coin);
            task.TaskProgress = TaskState.Reward;
            task.TaskDB.TaskState = TaskState.Reward;
            //更新到服务器
            TaskManager._instance.AddTaskRequest.SendRequest(task.TaskDB);
        }
        else if (task.TaskProgress == TaskState.Reward)
        {
            MessageManager._instance.ShowMessage("任务已完成,请接受其他任务", 0.4f);
        }
       
    }

    void OnTaskStateChanged()
    {
        UpdateShow();
    }

    private void OnDestroy()
    {
        if (task != null)
            task.OnTaskStateChanged -= OnTaskStateChanged;
    }

}
