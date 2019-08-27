using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Guard_E", menuName = "SBO/Skill/Builtin/Guard_E", order = 19)]
public class SBO_Guard_E : SBO_UseObject, I_BattleStack
{
    public void WhenIsUse()
    {
        SC_FieldMgr._fieldMgr.enemyPhaseAction[(int)BATTLEPHASE.GUARD] = this;
    }
    public override void UseEffect()
    {
        SC_EnemyMgr._enemyMgr.IsGuard = true;
        SC_EffectMgr._effectMgr.isEvent = true;
        SC_EffectMgr._effectMgr.EffectGetSlotObject(this, SC_EnemyMgr._enemyMgr.EnemyIndicator.gameObject.transform.position);
        SC_GameMgr._gameMgr.PrintClickTextBox(SC_EnemyMgr._enemyMgr.Name + " 이(가) 방어합니다.");
    }
}
