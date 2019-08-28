using UnityEngine;

[CreateAssetMenu(fileName = "Item_Potion", menuName = "SBO/Item/Potion", order = 1)]
public class SBO_Potion : SBO_UseObject, I_FieldStack, I_BattleStack, I_UseInInn
{
    public int healvalve;

    public void WhenIsUse()
    {
        SC_FieldMgr._fieldMgr.playerPhaseAction[(int)BATTLEPHASE.ITEM] = this;
    }
    public override void UseEffect()
    {
        SC_EffectMgr._effectMgr.isEvent = true;
        SC_EffectMgr._effectMgr.EffectPotion(Color);
        SC_SoundMgr._soundMgr.SFX_Heal();
        int f_heal = Mathf.Clamp(healvalve, 0, SC_PlayerMgr._playerMgr.MaxHP - SC_PlayerMgr._playerMgr.CurrentHP);
        SC_PlayerMgr._playerMgr.CurrentHP += f_heal;
        SC_GameMgr._gameMgr.PrintClickTextBox("체력이 " + f_heal + " 만큼 회복되었습니다.");
    }
}
