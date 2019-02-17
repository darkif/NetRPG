using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillItemPanel : MonoBehaviour {

    private SkillManager skillManager;

    public PosType posType;
    private Skill skill;

    private Image sprite;

    private Button skillBtn;

    private bool isSync = false;

    private void Awake()
    {
        sprite = GetComponent<Image>();
        skillBtn = GetComponent<Button>();
        skillManager = SkillManager._instance;
        skillManager.OnSkillSyncComlete += SkillSyncComplete;

        skillBtn.onClick.AddListener(OnSkillButtonClick);
    }

    private void Update()
    {
        if (isSync)
        {
            isSync = false;
            UpdateShow();
        }
    }

    void SkillSyncComplete()
    {
        isSync = true;
    }

    void UpdateShow()
    {
        skill = skillManager.GetSkillByPos(posType);
        sprite.sprite = Resources.Load<Sprite>("Skill/"+skill.SkillIcon);
    }

    void OnSkillButtonClick()
    {
        transform.parent.parent.SendMessage("OnSkillClick", skill);
    }

    private void OnDestroy()
    {
        skillManager.OnSkillSyncComlete -= SkillSyncComplete;
    }

}
