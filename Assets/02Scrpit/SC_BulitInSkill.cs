using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_BulitInSkill : SC_SkillInMenu
{
    void Start()
    {
        icon.sprite = SC_SkillMgr._skillMgr.testSkills[skillIndex].Image;
        text.text = SC_SkillMgr._skillMgr.testSkills[skillIndex].Name;
    }
}
