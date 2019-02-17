using System.Collections;
using System.Collections.Generic;

public enum SkillType
{
    Basic,
    Skill
}

public enum PosType
{
    Basic,
    One,
    Two,
    Three
}

public class Skill{
    private int skillId;
    private string skillName;
    private string skillIcon;
    private PlayerType playerType;
    private SkillType skillType;
    private PosType posType;
    private int coldTime;
    private int damagae;
    private int level =1;

    private SkillDB skillDB;

    #region property
    public int SkillId
    {
        get
        {
            return skillId;
        }

        set
        {
            skillId = value;
        }
    }

    public string SkillName
    {
        get
        {
            return skillName;
        }

        set
        {
            skillName = value;
        }
    }

    public PlayerType PlayerType
    {
        get
        {
            return playerType;
        }

        set
        {
            playerType = value;
        }
    }

    public string SkillIcon
    {
        get
        {
            return skillIcon;
        }

        set
        {
            skillIcon = value;
        }
    }

    public SkillType SkillType
    {
        get
        {
            return skillType;
        }

        set
        {
            skillType = value;
        }
    }

    public PosType PosType
    {
        get
        {
            return posType;
        }

        set
        {
            posType = value;
        }
    }

    public int ColdTime
    {
        get
        {
            return coldTime;
        }

        set
        {
            coldTime = value;
        }
    }

    public int Damagae
    {
        get
        {
            return damagae;
        }

        set
        {
            damagae = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public SkillDB SkillDB
    {
        get
        {
            return skillDB;
        }

        set
        {
            skillDB = value;
        }
    }

    #endregion

    public void Upgrade()
    {
        level++;
        damagae += damagae * level + 50;
        if (skillDB == null)
        {
            skillDB = new SkillDB();
        }
        skillDB.SkillId = SkillId;
        skillDB.Level = level;
        skillDB.Damage = damagae;

        SkillManager._instance.SendUpgradeRequest(skillDB);
    }

    public void Sync(SkillDB skillDB)
    {
        this.skillDB = skillDB;
        this.level = skillDB.Level;
        this.damagae = skillDB.Damage;
    }

}
