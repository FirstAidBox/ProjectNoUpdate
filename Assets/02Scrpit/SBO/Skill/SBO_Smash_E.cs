using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Smash_E", menuName = "SBO/Skill/Builtin/Smash_E", order = 20)]
public class SBO_Smash_E : SBO_UseObject, I_BattleStack
{
    public void WhenIsUse()
    {
        SC_FieldMgr._fieldMgr.enemyPhaseAction[(int)BATTLEPHASE.SMASH] = this;
    }
    public override void UseEffect()
    {
        if (SC_EnemyMgr._enemyMgr.IsDmg)
        {
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectDown(SC_EnemyMgr._enemyMgr.EnemyIndicator.gameObject.transform.position);
            SC_EnemyMgr._enemyMgr.IsDown = true;
            SC_GameMgr._gameMgr.PrintClickTextBox(SC_EnemyMgr._enemyMgr.Name + 
                " 이(가) 강타를 하려고 했으나 자세가 무너져 실패했습니다.");
        }
        else
        {
            float finDmg = SC_EnemyMgr._enemyMgr.ATK * 1.5f;
            SC_GameMgr._gameMgr.isPlayingText = true;
            SC_GameMgr._gameMgr.PrintTextBox(SC_EnemyMgr._enemyMgr.Name + " 의 강타.");
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectSimpleHit(SC_PlayerMgr._playerMgr.playerIndicator.gameObject.transform.position);
            SC_GameMgr._gameMgr.InvokeWaitEvent(SC_PlayerMgr._playerMgr.ApplyDamage, (int)finDmg);
            SC_PlayerMgr._playerMgr.IsDmg = true;
        }
    }
}
