using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BuiltInSkillSlot : SC_SkillSlot
    //기본스킬 슬롯. 일반스킬 슬롯과는 다르게 여러번 액션 슬롯에 넣을 수 있다.
{
    public override void SlotCanUse()
    {
    }
    public override void SlotCannotUse()
    {
    }
    public override void ButtonClick()
    {
        if (slotObject is I_Instant)
            return;
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
        else if (SC_GameMgr._gameMgr.isInInn)
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
