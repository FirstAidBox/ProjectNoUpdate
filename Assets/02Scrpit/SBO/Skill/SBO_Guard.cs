using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Guard", menuName = "SBO/Skill/Builtin/Guard", order = 5)]
public class SBO_Guard : SBO_UseObject, I_BattleStack
{
    public void WhenIsUse()
    {
        SC_FieldMgr._fieldMgr.playerPhaseAction[(int)BATTLEPHASE.GUARD] = this;
    }
    public override void UseEffect()
    {
        SC_PlayerMgr._playerMgr.IsGuard = true;
        SC_EffectMgr._effectMgr.isEvent = true;
        SC_EffectMgr._effectMgr.EffectGetSlotObject(this);
        SC_GameMgr._gameMgr.PrintClickTextBox("방어합니다.");
    }
}
