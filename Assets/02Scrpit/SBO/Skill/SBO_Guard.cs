using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Guard", menuName = "SBO/Skill/Builtin/Guard", order = 15)]
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
        SC_EffectMgr._effectMgr.EffectGuard(SC_PlayerMgr._playerMgr.playerIndicator.gameObject.transform.position);
        SC_SoundMgr._soundMgr.SFX_Guard();
        SC_GameMgr._gameMgr.PrintClickTextBox("방어합니다.");
    }
}
