using UnityEngine;

[CreateAssetMenu(fileName = "Item_Potion", menuName = "SBO/Item/Potion", order = 1)]
public class SBO_Potion : SBO_UseObject, I_FieldStack, I_BattleStack
{
    public int healvalve;

    /*아이템의 정보를 넣는게 아닌 슬롯 자체의 정보를 넣는 식으로 변경
    public void FieldStack()
    {
        SC_FieldMgr._fieldMgr.playerFieldActionSlot[SC_FieldMgr._fieldMgr.actionIndex] = this;
    }
    public void BattleStack()
    {
        SC_FieldMgr._fieldMgr.playerBattleActionSlot[SC_FieldMgr._fieldMgr.actionIndex] = this;
    }
    */
    public void WhenIsUse(SC_SlotBase mySlot)
    {
        SC_FieldMgr._fieldMgr.playerBattlePhase[(int)BATTLEPHASE.ITEM] = mySlot;
    }
    public override void UseEffect()
    {
        int f_heal = Mathf.Clamp(healvalve, 0, SC_PlayerMgr._playerMgr.MaxHP - SC_PlayerMgr._playerMgr.CurrentHP);
        SC_PlayerMgr._playerMgr.CurrentHP += f_heal;
        SC_GameMgr._gameMgr.PrintClickTextBox("체력이 " + f_heal + " 만큼 회복되었습니다.");
    }
}
