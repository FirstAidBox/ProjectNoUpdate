using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Smash", menuName = "SBO/Skill/Builtin/Smash", order = 16)]
public class SBO_Smash : SBO_UseObject, I_BattleStack
{
    public void WhenIsUse()
    {
        SC_FieldMgr._fieldMgr.playerPhaseAction[(int)BATTLEPHASE.SMASH] = this;
    }
    public override void UseEffect()
    {
        if(SC_PlayerMgr._playerMgr.IsDmg)
        {
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectDown(SC_PlayerMgr._playerMgr.playerIndicator.gameObject.transform.position);
            SC_SoundMgr._soundMgr.SFX_Stun();
            SC_PlayerMgr._playerMgr.IsDown = true;
            SC_GameMgr._gameMgr.PrintClickTextBox("자세가 무너져 제대로 행동할 수 없습니다.");
        }
        else
        {
            float finDmg = SC_PlayerMgr._playerMgr.ATK * 1.5f;
            SC_GameMgr._gameMgr.isPlayingText = true;
            SC_GameMgr._gameMgr.PrintTextBox(SC_EnemyMgr._enemyMgr.Name + " 을(를) 강하게 공격합니다.");
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectSimpleHit(SC_EnemyMgr._enemyMgr.EnemyIndicator.gameObject.transform.position);
            SC_SoundMgr._soundMgr.SFX_SimpleHit();
            SC_GameMgr._gameMgr.InvokeWaitEvent(SC_EnemyMgr._enemyMgr.ApplyDamage, (int)finDmg);
            SC_EnemyMgr._enemyMgr.IsDmg = true;
        }
    }
}
