using UnityEngine;

[CreateAssetMenu(fileName = "Skill_EgleEye", menuName = "SBO/Skill/EgleEye", order = 5)]
public class SBO_EgleEye : SBO_InstantObject
{
    public override void GetEffect()
    {
        SC_PlayerMgr._playerMgr.SightRange++;
    }
}
