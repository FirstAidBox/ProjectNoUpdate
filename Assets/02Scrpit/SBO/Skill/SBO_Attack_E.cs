using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Attack_Enemy", menuName = "SBO/Skill/Builtin/Attack_E", order = 8)]
public class SBO_Attack_E : SBO_UseObject, I_BattleStack
{
    public void WhenIsUse()
    {
        SC_FieldMgr._fieldMgr.enemyPhaseAction[(int)BATTLEPHASE.ATTACK] = this;
    }
    public override void UseEffect()
    {
        if (SC_PlayerMgr._playerMgr.IsGuard)
        {
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectSimpleHit(SC_PlayerMgr._playerMgr.playerIndicator.gameObject.transform.position);
            SC_GameMgr._gameMgr.PrintClickTextBox(SC_EnemyMgr._enemyMgr.Name + " 이(가) 공격했지만 잘 막아냈습니다.");
        }
        else
        {
            SC_GameMgr._gameMgr.isPlayingText = true;
            SC_GameMgr._gameMgr.PrintTextBox(SC_EnemyMgr._enemyMgr.Name + " 의 공격");
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectSimpleHit(SC_PlayerMgr._playerMgr.playerIndicator.gameObject.transform.position);
            SC_GameMgr._gameMgr.InvokeWaitEvent(SC_PlayerMgr._playerMgr.ApplyDamage, SC_PlayerMgr._playerMgr.ATK);
            SC_PlayerMgr._playerMgr.IsDown = true;
        }
    }
}
