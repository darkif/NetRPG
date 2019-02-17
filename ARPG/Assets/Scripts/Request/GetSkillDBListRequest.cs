using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GetSkillDBListRequest : BaseRequest {

    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.GetSkill;
        base.Awake();
    }

    public override void SendRequest()
    {
        base.SendRequest("r");
    }

    public override void OnResponse(string data)
    {
        string[] strs = data.Split('|');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        if (returnCode == ReturnCode.Success)
        {

            string[] skillArray = strs[1].Split('-');
            List<SkillDB> dbList = new List<SkillDB>();
            foreach (string s in skillArray)
            {
                if (s == "" || s == " ")
                    continue;

                string[] proArray = s.Split(',');
                SkillDB skillDB = new SkillDB();
                skillDB.SkillId = int.Parse(proArray[0]);
                skillDB.Level = int.Parse(proArray[1]);
                skillDB.Damage = int.Parse(proArray[2]);
                dbList.Add(skillDB);
                SkillManager._instance.OnResponseToGetSkillRequest(dbList);
            }
        }
        else
        {
            SkillManager._instance.OnResponseToGetSkillRequest(null);
        }
            
    }
}
