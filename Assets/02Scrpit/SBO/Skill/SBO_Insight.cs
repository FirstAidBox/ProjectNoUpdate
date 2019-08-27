using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Insight", menuName = "SBO/Skill/Insight", order = 3)]
public class SBO_Insight : SBO_InstantObject
{
    public override void GetEffect()
    {
        SC_PlayerMgr._playerMgr.ProphecyRange++;
    }
}
