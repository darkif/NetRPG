using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {

    public static SkillManager _instance;

    public TextAsset skillInfoText;
    private List<Skill> skillList = new List<Skill>();
    private Dictionary<int, Skill> skillDict = new Dictionary<int, Skill>();

    public delegate void OnSkillSyncComleteEvent();
    public event OnSkillSyncComleteEvent OnSkillSyncComlete;

    private GetSkillDBListRequest getSkillDBListRequest;
    private UpgradeRequest upgradeRequest;


    private void Awake()
    {
        _instance = this;
        upgradeRequest = GetComponent<UpgradeRequest>();
        getSkillDBListRequest = GetComponent<GetSkillDBListRequest>();
        InitSkill();
    }

    void InitSkill()
    {
        string[] skillArray = skillInfoText.ToString().Split('\n');
        foreach(string str in skillArray)
        {
            string[] proArray = str.Split(',');
            Skill skill = new Skill();
            skill.SkillId = int.Parse(proArray[0]);
            skill.SkillName = proArray[1];
            skill.SkillIcon = proArray[2];

            switch (proArray[3])
            {
                case "Swordman":
                    skill.PlayerType = PlayerType.Swordman;
                    break;
                case "Wizard":
                    skill.PlayerType = PlayerType.Wizard;
                    break;
            }

            switch (proArray[4])
            {
                case "Basic":
                    skill.SkillType = SkillType.Basic;
                    break;
                case "Skill":
                    skill.SkillType = SkillType.Skill;
                    break;
            }

            switch (proArray[5])
            {
                case "Basic":
                    skill.PosType = PosType.Basic;
                    break;
                case "One":
                    skill.PosType = PosType.One;
                    break;
                case "Two":
                    skill.PosType = PosType.Two;
                    break;
                case "Three":
                    skill.PosType = PosType.Three;
                    break;
            }

            skill.ColdTime = int.Parse(proArray[6]);
            skill.Damagae = int.Parse(proArray[7]);
            skill.Level = 1;

            skillList.Add(skill);
            skillDict.Add(skill.SkillId, skill);
        }

        Invoke("SendGetSkillListRequest", 0.5f);
    }

    void SendGetSkillListRequest()
    {
        getSkillDBListRequest.SendRequest();
    }


    public Skill GetSkillByPos(PosType posType)
    {
        PlayerInfo info = PlayerInfo._instance;
        foreach(Skill skill in skillList)
        {
            if(skill.PlayerType==info.PlayerType && skill.PosType == posType)
            {
                return skill;
            }
        }

        return null;
    }


    public void SendUpgradeRequest(SkillDB skillDB)
    {
        upgradeRequest.SendRequest(skillDB);
    }


    public void OnResponseToGetSkillRequest(List<SkillDB> dbList)
    {      
        if (dbList == null)
        {
            if (OnSkillSyncComlete != null)
                OnSkillSyncComlete();
            return;
        }

        foreach(SkillDB skillDB in dbList)
        {
            Skill skill = null;
            if(skillDict.TryGetValue(skillDB.SkillId,out skill))
            {
                skill.Sync(skillDB);
            }
        }

        if(OnSkillSyncComlete!=null)
            OnSkillSyncComlete();
    }

}
