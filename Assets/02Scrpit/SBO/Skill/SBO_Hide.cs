using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Hide", menuName = "SBO/Skill/Builtin/Hide", order = 2)]
public class SBO_Hide : SBO_UseObject, I_FieldStack
{
    public override void UseEffect()
    {
        SC_PlayerMgr._playerMgr.IsHide = true;
        SC_EffectMgr._effectMgr.isEvent = true;
        SC_EffectMgr._effectMgr.EffectGetSlotObject(this);
        SC_GameMgr._gameMgr.PrintClickTextBox("기척을 숨깁니다.");
    }
}
