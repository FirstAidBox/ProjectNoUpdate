using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Find", menuName = "SBO/Skill/Builtin/Find", order = 1)]
public class SBO_Find : SBO_UseObject, I_FieldStack
{
    public override void UseEffect()
    {
        SC_GameMgr._gameMgr.isPlayingText = true;
        SC_GameMgr._gameMgr.PrintTextBox("탐색중...");
        SC_EffectMgr._effectMgr.isEvent = true;
        SC_EffectMgr._effectMgr.EffectFind();
        SC_GameMgr._gameMgr.InvokeWaitEvent(SC_PlayerMgr._playerMgr.GetRandomItem);
    }
}