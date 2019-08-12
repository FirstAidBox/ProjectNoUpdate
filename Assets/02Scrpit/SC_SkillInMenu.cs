using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_SkillInMenu : MonoBehaviour
{
    public int skillIndex;
    public Image icon;
    public Text text;

    public void ButtonClick()
    {
        SC_SkillMgr._skillMgr.testSkills[skillIndex].UseEffect();
    }

    public void PointerDown()
    {
        SC_GameMgr._gameMgr.PrintTextBox(SC_SkillMgr._skillMgr.testSkills[skillIndex].Tooltip);
    }
}
