using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestSkillData", menuName = "CrateTestSkill")]
public class TestSkill : ScriptableObject, I_CanUse
{
    public int Index;
    public string Name;
    public string Tooltip;
    public Sprite Image;
    public Color Color = Color.white;

    public void UseEffect()
    {
        SC_GameMgr._gameMgr.PrintTextBox(Name + "을(를) 사용했습니다.");
    }
}
