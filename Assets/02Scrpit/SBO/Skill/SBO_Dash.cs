using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Dash", menuName = "SBO/Skill/Builtin/Dash", order = 3)]
public class SBO_Dash : SBO_UseObject, I_FieldStack
{
    public override void UseEffect()
    {
        SC_PlayerMgr._playerMgr.IsDash = true;
        SC_EffectMgr._effectMgr.isEvent = true;
        SC_EffectMgr._effectMgr.EffectGetSlotObject(this);
        SC_SoundMgr._soundMgr.SFX_SimpleSkill();
        SC_GameMgr._gameMgr.PrintClickTextBox("강행돌파할 준비를 합니다.");
    }
}
