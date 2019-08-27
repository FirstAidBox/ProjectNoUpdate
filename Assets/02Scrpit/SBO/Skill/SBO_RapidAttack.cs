using UnityEngine;

[CreateAssetMenu(fileName = "Skill_RapidAttack", menuName = "SBO/Skill/RapidAttack", order = 6)]
public class SBO_RapidAttack : SBO_UseObject, I_BattleStack
{
    public void WhenIsUse()
    {
        SC_FieldMgr._fieldMgr.playerPhaseAction[(int)BATTLEPHASE.ATTACK] = this;
        SC_FieldMgr._fieldMgr.playerPhaseAction[(int)BATTLEPHASE.EXTRA] = this;
    }
    public override void UseEffect()
    {
        if(SC_FieldMgr._fieldMgr.currentBattlePhase == BATTLEPHASE.EXTRA)
        {
            if(SC_EnemyMgr._enemyMgr.IsDown)
            {
                SC_GameMgr._gameMgr.isPlayingText = true;
                SC_GameMgr._gameMgr.PrintTextBox(SC_EnemyMgr._enemyMgr.Name + " 에게 추가공격");
                SC_EffectMgr._effectMgr.isEvent = true;
                SC_EffectMgr._effectMgr.EffectSimpleHit(SC_EnemyMgr._enemyMgr.EnemyIndicator.gameObject.transform.position);
                SC_GameMgr._gameMgr.InvokeWaitEvent(SC_EnemyMgr._enemyMgr.ApplyDamagePure, SC_PlayerMgr._playerMgr.SPD);
            }
        }
        else if (SC_EnemyMgr._enemyMgr.IsGuard)
        {
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectSimpleHit(SC_EnemyMgr._enemyMgr.EnemyIndicator.gameObject.transform.position);
            SC_GameMgr._gameMgr.PrintClickTextBox(SC_EnemyMgr._enemyMgr.Name + " 을(를) 공격했지만 방어 중이라 통하지 않았습니다.");
        }
        else
        {
            SC_GameMgr._gameMgr.isPlayingText = true;
            SC_GameMgr._gameMgr.PrintTextBox(SC_EnemyMgr._enemyMgr.Name + " 에게 공격");
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectSimpleHit(SC_EnemyMgr._enemyMgr.EnemyIndicator.gameObject.transform.position);
            SC_GameMgr._gameMgr.InvokeWaitEvent(SC_EnemyMgr._enemyMgr.ApplyDamage, SC_PlayerMgr._playerMgr.ATK);
            SC_EnemyMgr._enemyMgr.IsDmg = true;
        }
    }
}
