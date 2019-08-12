using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSkillprefep : MonoBehaviour
{
    public int skillIndex;
    public Image icon;
    public Text text;
	void Start ()
    {
        Debug.Log("StartOnSkill");
        icon.sprite = SC_SkillMgr._skillMgr.testSkills[skillIndex].Image;
        text.text = SC_SkillMgr._skillMgr.testSkills[skillIndex].Name;
    }

    public void ButtonClick()
    {
        SC_SkillMgr._skillMgr.testSkills[skillIndex].UseEffect();
    }

    public void PointerDown()
    {
        SC_GameMgr._gameMgr.PrintTextBox(SC_SkillMgr._skillMgr.testSkills[skillIndex].Tooltip);
    }
}
