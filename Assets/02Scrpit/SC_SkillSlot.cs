using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SkillSlot : SC_SlotBase
{
    public override void SlotInit()
    {
        slotObject = null;
        gameObject.SetActive(false);
    }
    public override void AddObject(SBO_SlotObject GetObject)
    {
        base.AddObject(GetObject);
        gameObject.SetActive(true);
        if (GetObject is I_Instant)
            (GetObject as I_Instant).GetEffect();
        SC_PlayerMgr._playerMgr.SkillCount++;
    }
    public override void RemoveObject()
    {
        if (slotObject != null)
        {
            SlotInit();
            SC_PlayerMgr._playerMgr.SkillCount--;
        }
    }
    public override void UseObject()
    {
        if (slotObject is I_CanUse)
        {
            (slotObject as I_CanUse).UseEffect();
            SlotCanUse();
        }
    }
    public override void ButtonClick()
    {
        if (slotObject is I_Instant)
            SC_GameMgr._gameMgr.PrintTextBox("지속효과(패시브) 기술입니다.");
        else if (isStackInAction)
            SC_GameMgr._gameMgr.PrintTextBox("이미 사용 예약이 되어있습니다.");
        else if (SC_FieldMgr._fieldMgr.isInBattle)
        {
            if (slotObject is I_BattleStack)
            {
                SC_FieldMgr._fieldMgr.PLActionInputInBattle(this);
                SlotCannotUse();
            }
            else
                SC_GameMgr._gameMgr.PrintTextBox("전투 중엔 사용할 수 없는 기술입니다.");
        }
        else if (SC_GameMgr._gameMgr.isRest)
        {
            if (slotObject is I_UseInInn)
                UseObject();
            else
                SC_GameMgr._gameMgr.PrintTextBox("휴식 중엔 사용할 수 없는 기술입니다.");
        }
        else if (!SC_FieldMgr._fieldMgr.isInBattle)
        {
            if (slotObject is I_FieldStack)
            {
                SC_FieldMgr._fieldMgr.PLActionInputInField(this);
                SlotCannotUse();
            }
            else
                SC_GameMgr._gameMgr.PrintTextBox("탐사 중엔 사용할 수 없는 기술입니다.");
        }
        else
            Debug.Log("상상조차 못한 상황 ㄴㅇㄱ");
    }
}
