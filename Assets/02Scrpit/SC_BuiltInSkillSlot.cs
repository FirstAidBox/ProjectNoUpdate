using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BuiltInSkillSlot : SC_SlotBase
//기본스킬 슬롯. 일반스킬 슬롯과는 다르게 여러번 액션 슬롯에 넣을 수 있다.
{
    private void Awake()
    {
        if (slotObject != null)
            SlotInit();
        else
            Debug.Log(this.gameObject.name + " 의 soltObject가 비어있어 초기화에 실패했습니다.");
    }
    public override void SlotInit()
    {
        icon.sprite = slotObject.Image;
        icon.color = slotObject.Color;
        text.text = slotObject.Name;
        gameObject.SetActive(true);
    }
    public override void RemoveObject()
    {
    }
    public override void SlotCanUse()
    {
    }
    public override void SlotCannotUse()
    {
    }
    public override void UseObject()
    {
        if (slotObject is I_CanUse)
        {
            (slotObject as I_CanUse).UseEffect();
        }
    }
    public override void ButtonClick()
    {
        if (SC_FieldMgr._fieldMgr.isInBattle)
        {
            if (slotObject is I_BattleStack)
            {
                SC_FieldMgr._fieldMgr.PLActionInputInBattle(this);
                SC_SoundMgr._soundMgr.SFX_ClickOK();
            }
            else
            {
                SC_GameMgr._gameMgr.PrintTextBox("전투 중엔 사용할 수 없는 기술입니다.");
                SC_SoundMgr._soundMgr.SFX_ClickBiff();
            }
        }
        else if (SC_GameMgr._gameMgr.isRest)
        {
            if (slotObject is I_UseInInn)
            {
                UseObject();
                SC_SoundMgr._soundMgr.SFX_ClickOK();
            }
            else
            {
                SC_GameMgr._gameMgr.PrintTextBox("휴식 중엔 사용할 수 없는 기술입니다.");
                SC_SoundMgr._soundMgr.SFX_ClickBiff();
            }
        }
        else if (!SC_FieldMgr._fieldMgr.isInBattle)
        {
            if (slotObject is I_FieldStack)
            {
                SC_FieldMgr._fieldMgr.PLActionInputInField(this);
                SC_SoundMgr._soundMgr.SFX_ClickOK();
            }
            else
            {
                SC_GameMgr._gameMgr.PrintTextBox("탐사 중엔 사용할 수 없는 기술입니다.");
                SC_SoundMgr._soundMgr.SFX_ClickBiff();
            }
        }
        else
            Debug.Log("상상조차 못한 상황 ㄴㅇㄱ");
    }
}
