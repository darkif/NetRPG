using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillPanel : MonoBehaviour {
    private Text skillDesc;
    private Text skillName;
    private Button levelBtn;
    private Button closeBtn;

    private Skill curSkill;

    private void Awake()
    {
        skillDesc = transform.Find("BG/descLabel").GetComponent<Text>();
        skillName = transform.Find("BG/skillName").GetComponent<Text>();
        levelBtn = transform.Find("levelUpBtn").GetComponent<Button>();
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();

        levelBtn.onClick.AddListener(OnLevelUpButtonClick);
        closeBtn.onClick.AddListener(OnCloseButtonClick);

        transform.localScale = Vector3.zero; 
    }

    //点击升级
    void OnLevelUpButtonClick()
    {
        if (curSkill == null)
        {
            MessageManager._instance.ShowMessage("请选择技能", 0.8f);
            return;
        }

        if (curSkill.Level == PlayerInfo._instance.Level)
        {
            MessageManager._instance.ShowMessage("不能超过角色等级", 0.8f);
            return;
        }

        int coinNeed = (curSkill.Level + 1) * 500;
        bool success = PlayerInfo._instance.GetCoin(coinNeed);
        if (success)
        {
            curSkill.Upgrade(); //成功升级 同步到数据库
            OnSkillClick(curSkill);//更新显示
        }
        else
        {
            MessageManager._instance.ShowMessage("所需金币不够", 0.8f);
        }
    }

    void OnCloseButtonClick()
    {
        Tweener tweener = transform.DOScale(0, 0.4f);
        tweener.OnComplete(() =>
        {
            curSkill = null;
            this.gameObject.SetActive(false);
        });
    }

    public void ShowPanel()
    {
        if (!gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(true);
            transform.DOScale(1, 0.4f);
        }
        else
        {
            OnCloseButtonClick();
        }
        
    }

    void OnSkillClick(Skill skill)
    {
        curSkill = skill;
        skillName.text = skill.SkillName + " LV" + skill.Level;
        skillDesc.text = "damage:  " + skill.Damagae + "\n" + "升级所需金币:  " + (skill.Level + 1) * 500;
    }

}
