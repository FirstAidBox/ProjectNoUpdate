using UnityEngine;

[CreateAssetMenu(fileName = "Skill_CombatMastery", menuName = "SBO/Skill/CombatMastery", order = 1)]
public class SBO_CombatMastery : SBO_InstantObject
{
    public int increaseValue;
    public override void GetEffect()
    {
        SC_PlayerMgr._playerMgr.ATK += increaseValue;
        SC_PlayerMgr._playerMgr.DEF += increaseValue;
    }
}
