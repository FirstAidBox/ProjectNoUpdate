﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ItemSlot : SC_SlotBase
{
    public override void SlotInit()
    {
        base.AddObject(SC_SBODataMgr._SBODataMgr.itemData[0]);
    }
    public override void AddObject(SBO_SlotObject GetObject)
    {
        base.AddObject(GetObject);
        SC_PlayerMgr._playerMgr.ItemCount++;
    }
    public override void RemoveObject()
    {
        if (slotObject.Index != 0)
        {
            SlotInit();
            SC_PlayerMgr._playerMgr.ItemCount--;
        }
    }
    public override void UseObject()
    {
        if(slotObject is I_CanUse)
        {
            (slotObject as I_CanUse).UseEffect();
            SlotCanUse();
            RemoveObject();
        }
    }
    public override void PointerDown()
    {
        if (slotObject.Index == 0)
        {
            SC_GameMgr._gameMgr.PrintTextBox(slotObject.Image, slotObject.Text, slotObject.Color);     
        }
        else
        {
            if(SC_MenuBar._menuBar.CurrentPage == MENUPAGE.SELL)
            {
                SC_GameMgr._gameMgr.PrintTextBox(slotObject.Image, slotObject.Name + " 판매 가격: " + slotObject.Price + " " + slotObject.Text, slotObject.Color);
            }
            else
            {
                SC_GameMgr._gameMgr.PrintTextBox(slotObject.Image, slotObject.Text, slotObject.Color);
            }
        }
    }
    public override void ButtonClick()
    {
        if (slotObject.Index == 0)
            SC_SoundMgr._soundMgr.SFX_ClickBiff();
        else if (isStackInAction)
        {
            SC_GameMgr._gameMgr.PrintTextBox("이미 사용 예약이 되어있습니다.");
            SC_SoundMgr._soundMgr.SFX_ClickBiff();
        }
        else if (SC_FieldMgr._fieldMgr.isInBattle)
        {
            if (slotObject is I_BattleStack)
            {
                SC_FieldMgr._fieldMgr.PLActionInputInBattle(this);
                SlotCannotUse();
                SC_SoundMgr._soundMgr.SFX_ClickOK();
            }
            else
            {
                SC_GameMgr._gameMgr.PrintTextBox("전투 중엔 사용할 수 없는 아이템입니다.");
                SC_SoundMgr._soundMgr.SFX_ClickBiff();
            }
        }
        else if (SC_GameMgr._gameMgr.isRest)
        {
            if (SC_MenuBar._menuBar.CurrentPage == MENUPAGE.SELL)
            {
                SC_PlayerMgr._playerMgr.SellItem(slotObject);
                RemoveObject();
                SC_SoundMgr._soundMgr.SFX_Coin();
            }
            else
            {
                if (slotObject is I_UseInInn)
                {
                    UseObject();
                    SC_SoundMgr._soundMgr.SFX_ClickOK();
                }
                else
                {
                    SC_GameMgr._gameMgr.PrintTextBox("휴식 중엔 사용할 수 없는 아이템입니다.");
                    SC_SoundMgr._soundMgr.SFX_ClickBiff();
                }
            }
        }
        else if (!SC_FieldMgr._fieldMgr.isInBattle)
        {
            if (slotObject is I_FieldStack)
            {
                SC_FieldMgr._fieldMgr.PLActionInputInField(this);
                SlotCannotUse();
                SC_SoundMgr._soundMgr.SFX_ClickOK();
            }
            else
            {
                SC_GameMgr._gameMgr.PrintTextBox("탐사 중엔 사용할 수 없는 아이템입니다.");
                SC_SoundMgr._soundMgr.SFX_ClickBiff();
            }
        }
        else
            Debug.Log("상상조차 못한 상황 ㄴㅇㄱ");
    }
}
