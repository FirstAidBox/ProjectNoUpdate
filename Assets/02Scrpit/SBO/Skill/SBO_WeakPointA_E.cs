using UnityEngine;

[CreateAssetMenu(fileName = "Skill_WeakPointA_E", menuName = "SBO/Skill/Builtin/WeakPointA_E", order = 21)]
public class SBO_WeakPointA_E : SBO_UseObject, I_BattleStack
{

    public void WhenIsUse()
    {
        SC_FieldMgr._fieldMgr.enemyPhaseAction[(int)BATTLEPHASE.ATTACK] = this;
    }
    public override void UseEffect()
    {
        if (SC_PlayerMgr._playerMgr.IsCounter)
        {
            SC_GameMgr._gameMgr.isPlayingText = true;
            SC_GameMgr._gameMgr.PrintTextBox(SC_EnemyMgr._enemyMgr.Name + " 이(가) 공격했지만 흘려내고 반격했습니다.");
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectActiveCounter();
            SC_GameMgr._gameMgr.InvokeWaitEvent(SC_EnemyMgr._enemyMgr.ApplyDamage, SC_PlayerMgr._playerMgr.ATK);
            SC_EnemyMgr._enemyMgr.IsDmg = true;
        }
        else if (SC_PlayerMgr._playerMgr.IsGuard)
        {
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectSimpleHit(SC_PlayerMgr._playerMgr.playerIndicator.gameObject.transform.position);
            SC_SoundMgr._soundMgr.SFX_Guard();
            SC_GameMgr._gameMgr.PrintClickTextBox(SC_EnemyMgr._enemyMgr.Name + " 이(가) 공격했지만 잘 막아냈습니다.");
        }
        else
        {
            SC_GameMgr._gameMgr.isPlayingText = true;
            SC_GameMgr._gameMgr.PrintTextBox(SC_EnemyMgr._enemyMgr.Name + " 의 약점공격");
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectSimpleHit(SC_PlayerMgr._playerMgr.playerIndicator.gameObject.transform.position);
            SC_SoundMgr._soundMgr.SFX_SimpleHit();
            SC_GameMgr._gameMgr.InvokeWaitEvent(SC_PlayerMgr._playerMgr.ApplyDamagePure, SC_EnemyMgr._enemyMgr.ATK);
            SC_PlayerMgr._playerMgr.IsDmg = true;
        }
    }
}
