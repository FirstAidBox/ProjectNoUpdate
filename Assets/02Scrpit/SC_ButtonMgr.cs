using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ButtonMgr : MonoBehaviour
//인디케이터들을 제외한 UI 버튼들의 커서오버나 클릭시 실행되는 함수들을 모아놓은 스크립트
//SC_StringMgr에 있던 문자열들 중 버튼에 관련된 것들은 이곳에 옮겨짐
//finalAnswer버튼은 GameMgr에서 담당한다.
{
    public SC_StringMgr _stringMgr;

    public readonly string s_inn_buy = "돈으로 물건을 구매합니다.";
    public readonly string s_inn_sell = "물건을 판매해 돈을 얻습니다.";
    public readonly string s_inn_exit = "여관을 떠나 탐험을 시작합니다";
    public readonly string s_plm_stat = "캐릭터의 능력치를 확인합니다.";
    public readonly string s_plm_skill = "캐릭터의 기술을 확인합니다.";
    public readonly string s_plm_item = "캐릭터의 소지품을 확인합니다.";
    public readonly string s_plm_exit = "메뉴 창을 닫습니다.";
    public readonly string s_plm_FBskill = "캐릭터의 기본기술(탐험)을 확인합니다.";
    public readonly string s_plm_BBskill = "캐릭터의 기본기술(전투)을 확인합니다.";

    private void Awake()
    {
        _stringMgr = GetComponent<SC_StringMgr>();
    }

    public void MainTextBoxClick()
    {
        if (!SC_GameMgr._gameMgr.trigger_Click)
            return;
        SC_GameMgr._gameMgr.trigger_Click = false;
    }

    public void PointerUpPrintBase() { SC_GameMgr._gameMgr.PrintBaseBox(); }

    public void SysExitPointerDown() { }
    public void SysExitButtonClick() { SC_GameMgr._gameMgr.AnswerExitGame(); }

    public void InnBuyPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_inn_buy); }
    public void InnBuyButtonClick() { SC_MenuBar._menuBar.PopupBuyMenu(); }

    public void InnSellPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_inn_sell); }
    public void InnSellButtonClick() { SC_MenuBar._menuBar.PopupSellMenu(); }

    public void InnExitPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_inn_exit); }
    public void InnExitButtonClick() { SC_GameMgr._gameMgr.AnswerExitInn(); }

    public void PLMenuStatPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_plm_stat); }
    public void PLMenuStatClickButton() { SC_MenuBar._menuBar.OpenStatsWindow(); }

    public void PLMenuSkillPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_plm_skill); }
    public void PLMenuSkillClickButton() { SC_MenuBar._menuBar.OpenSkillWindow(); }

    public void PLMenuItemPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_plm_item); }
    public void PLMenuItemClickButton() { SC_MenuBar._menuBar.OpenItemWindow(); }

    public void PLMenuExitPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_plm_exit); }
    public void PLMenuExitButtonClick() { SC_MenuBar._menuBar.ClosePlayerMenu(); }

    public void PLMenuFBSPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_plm_FBskill); }
    public void PLMenuFBSButtonClick() { SC_MenuBar._menuBar.OpenFBSWindow(); }

    public void PLMenuBBSPointerDown() { SC_GameMgr._gameMgr.PrintTextBox(s_plm_BBskill); }
    public void PLMenuBBSButtonClick() { SC_MenuBar._menuBar.OpenBBSWindow(); }
}
