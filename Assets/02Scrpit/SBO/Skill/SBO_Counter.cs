using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Counter", menuName = "SBO/Skill/Counter", order = 2)]
public class SBO_Counter : SBO_UseObject, I_BattleStack
{
    public void WhenIsUse()
    {
        SC_FieldMgr._fieldMgr.playerPhaseAction[(int)BATTLEPHASE.GUARD] = this;
    }

    public override void UseEffect()
    {
        SC_PlayerMgr._playerMgr.IsCounter = true;
        SC_EffectMgr._effectMgr.isEvent = true;
        SC_EffectMgr._effectMgr.EffectCounter();
        SC_SoundMgr._soundMgr.SFX_Counter();
        SC_GameMgr._gameMgr.PrintClickTextBox("반격할 준비를 합니다.");
    }
}
