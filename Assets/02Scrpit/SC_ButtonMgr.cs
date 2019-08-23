﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ButtonMgr : MonoBehaviour
//인디케이터들을 제외한 UI 버튼들의 커서오버나 클릭시 실행되는 함수들을 모아놓은 스크립트
//SC_StringMgr에 있던 문자열들 중 버튼에 관련된 것들은 이곳에 옮겨짐
//finalAnswer버튼은 GameMgr에서 담당한다.
{
    public SC_StringMgr _stringMgr;
    public SC_MenuBar _menuBar;

    public readonly string s_inn_buy = "돈으로 물건을 구매합니다.(미구현)";
    public readonly string s_inn_sell = "물건을 판매해 돈을 얻습니다.(미구현)";
    public readonly string s_inn_exit = "여관을 떠나 모험을 시작합니다";
    public readonly string s_plm_stat = "캐릭터의 능력치를 확인합니다.";
    public readonly string s_plm_skill = "캐릭터의 기술을 확인합니다.(미구현)";
    public readonly string s_plm_item = "캐릭터의 소지품을 확인합니다.(미구현)";
    public readonly string s_plm_exit = "메뉴 창을 닫습니다.";

    private void Awake()
    {
        _stringMgr = GetComponent<SC_StringMgr>();
        _menuBar = GetComponent<SC_MenuBar>();
    }

    public void MainTextBoxClick()
    {
        if (!SC_GameMgr._gameMgr.trigger_Click)
            return;
        SC_GameMgr._gameMgr.trigger_Click = false;
    }

    public void PointerUpPrintBase() { SC_GameMgr._gameMgr.PrintBaseBox(); }

    public void InnBuyPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_inn_buy); }
    public void InnBuyButtonClick() { }

    public void InnSellPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_inn_sell); }
    public void InnSellButtonClick() { }

    public void InnExitPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_inn_exit); }
    public void InnExitButtonClick() { SC_GameMgr._gameMgr.AnswerExitInn(); }

    public void PLMenuStatPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_plm_stat); }
    public void PLMenuStatClickButton() { _menuBar.OpenStatsWindow(); }

    public void PLMenuSkillPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_plm_skill); }
    public void PLMenuSkillClickButton() { _menuBar.OpenSkillWindow(); }

    public void PLMenuItemPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_plm_item); }
    public void PLMenuItemClickButton() { _menuBar.OpenItemWindow(); }

    public void PLMenuExitPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_plm_exit); }
    public void PLMenuExitButtonClick() { _menuBar.ClosePlayerMenu(); }

    public void Test()
    {
        Debug.Log("Test!");
    }
}
