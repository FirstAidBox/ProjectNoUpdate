using UnityEngine;

[CreateAssetMenu(fileName = "Skill_HpUp", menuName = "SBO/Skill/HpUp", order = 7)]
public class SBO_HPUp : SBO_InstantObject
{
    public int increaseValue;

    public override void GetEffect()
    {
        SC_PlayerMgr._playerMgr.MaxHP += increaseValue;
        SC_PlayerMgr._playerMgr.CurrentHP += increaseValue;
    }
}
