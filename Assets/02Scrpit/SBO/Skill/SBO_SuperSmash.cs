using UnityEngine;

[CreateAssetMenu(fileName = "Skill_SuperSmash", menuName = "SBO/Skill/SuperSmash", order = 4)]
public class SBO_SuperSmash : SBO_UseObject, I_BattleStack
{
    public float increaseValue;
    public void WhenIsUse()
    {
        SC_FieldMgr._fieldMgr.playerPhaseAction[(int)BATTLEPHASE.SMASH] = this;
    }
    public override void UseEffect()
    {
        if (SC_PlayerMgr._playerMgr.IsDmg)
        {
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectDown(SC_PlayerMgr._playerMgr.playerIndicator.gameObject.transform.position);
            SC_PlayerMgr._playerMgr.IsDown = true;
            SC_GameMgr._gameMgr.PrintClickTextBox("자세가 무너져 제대로 행동할 수 없습니다.");
        }
        else
        {
            float finDmg = SC_PlayerMgr._playerMgr.ATK * increaseValue;
            SC_GameMgr._gameMgr.isPlayingText = true;
            SC_GameMgr._gameMgr.PrintTextBox(SC_EnemyMgr._enemyMgr.Name + " 을(를) 매우 강하게 공격합니다.");
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectSimpleHit(SC_EnemyMgr._enemyMgr.EnemyIndicator.gameObject.transform.position);
            SC_GameMgr._gameMgr.InvokeWaitEvent(SC_EnemyMgr._enemyMgr.ApplyDamage, (int)finDmg);
            SC_EnemyMgr._enemyMgr.IsDmg = true;
        }
    }
}
