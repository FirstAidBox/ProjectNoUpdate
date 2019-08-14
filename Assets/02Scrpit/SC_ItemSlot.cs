using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ItemSlot : SC_SlotBase
{
    public void Start()
    {
        AddObject(SC_ItemMgr._itemMgr.ItemList[1]);
    }
    public override void RemoveObject()
    {
        AddObject(SC_ItemMgr._itemMgr.ItemList[0]);
    }
    public override void UseObject()
    {
        if(slotObject is I_CanUse)
        {
            (slotObject as I_CanUse).UseEffect();
            RemoveObject();
        }
    }
    public override void ButtonClick()
    {
        if (slotObject.Index == 0)
            return;
        else if (isStackInAction)
            SC_GameMgr._gameMgr.PrintTextBox("이미 사용 예약이 되어있습니다.");
        else if (SC_FieldMgr._fieldMgr.isInBattle)
        {
            if (slotObject is I_BattleStack)
            {
                SC_FieldMgr._fieldMgr.PlayerActionInput(this);
                isStackInAction = true;
                mask.color = new Color(0, 0, 0, 0.4f);
            }
            else
                SC_GameMgr._gameMgr.PrintTextBox("전투 중엔 사용할 수 없는 아이템입니다.");
        }
        else if (SC_GameMgr._gameMgr.isInInn)
        {
            UseObject();
        }
        else if (!SC_FieldMgr._fieldMgr.isInBattle)
        {
            if (slotObject is I_FieldStack)
            {
                SC_FieldMgr._fieldMgr.PlayerActionInput(this);
                isStackInAction = true;
                mask.color = new Color(0, 0, 0, 0.4f);
            }
            else
                SC_GameMgr._gameMgr.PrintTextBox("탐사 중엔 사용할 수 없는 아이템입니다.");
        }
        else
            Debug.Log("상상조차 못한 상황 ㄴㅇㄱ");
    }
}
