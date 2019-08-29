using UnityEngine;

[CreateAssetMenu(fileName = "Item_Bomb", menuName = "SBO/Item/Bomb", order = 2)]
public class SBO_Bomb : SBO_UseObject, I_BattleStack
{
    public int dmgvalue;

    public void WhenIsUse()
    {
        SC_FieldMgr._fieldMgr.playerPhaseAction[(int)BATTLEPHASE.ITEM] = this;
    }

    public override void UseEffect()
    {
        SC_GameMgr._gameMgr.isPlayingText = true;
        SC_GameMgr._gameMgr.PrintTextBox(SC_EnemyMgr._enemyMgr.Name + " 에게 " + Name + " 을(를) 사용했습니다.");
        SC_EffectMgr._effectMgr.isEvent = true;
        SC_EffectMgr._effectMgr.EffectBomb();
        SC_SoundMgr._soundMgr.SFX_Bomb();
        SC_GameMgr._gameMgr.InvokeWaitEvent(SC_EnemyMgr._enemyMgr.ApplyDamagePure, dmgvalue);
        SC_EnemyMgr._enemyMgr.IsDmg = true;
    }
}
