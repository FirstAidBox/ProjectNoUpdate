﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MENUPAGE { STAT=1, F_BSKILL, B_BSKILL, BUY, SELL };

public class SC_MenuBar : MonoBehaviour
    //메뉴바 관리. 메뉴바에 표시되는 내용들을 담당
{
    public static SC_MenuBar _menuBar;
    public GameObject playerMenuBar;

    public bool isPopupPlayerBar;

    public MENUPAGE CurrentPage;

    public GameObject buttons;
    public GameObject statButton;
    public Image statImage;
    public GameObject skillButton;
    public Image skillImage;
    public GameObject itemButton;
    public Image itemImage;
    public GameObject fieldBSkillButton;
    public Image fieldBSkillImage;
    public GameObject battelBSkillButton;
    public Image battleBSkillImage;

    public GameObject statsWindow;
    public GameObject title;
    public Text titleText;
    public const string title_stat = "능력치";
    public const string title_skill = "기술";
    public const string title_itme = "소지품";
    public const string title_buy = "구매";
    public const string title_sell = "판매";
    public const string title_fieldBS = "기본-탐험";
    public const string title_battleBS = "기본-전투";
    public Image statsImage;
    public Text jobText;
    public const string jobHead = "직업: ";
    public Text hpText;
    public const string hpHead = "생명력: ";
    public Text atkText;
    public const string atkHead = "공격력: ";
    public Text defText;
    public const string defHead = "방어력: ";
    public Text spdText;
    public const string spdHead = "속도: ";
    public Text moneyText;
    public const string moneyHead = "소지금: ";

    public GameObject skillWindow;

    public GameObject fieldBSkillWindow;

    public GameObject battleBSkillWindow;

    public GameObject itemWindow;

    public GameObject innItemWindow;
    public GameObject moneyIndi;
    public Text moneyIndiText;

    public GameObject optionWindow;
    public GameObject exitWindow;
    public GameObject helpWindow;

    private void Awake()
    {
        _menuBar = this;
    }
    public void PopupStatMenu()
    {
        PopupPlayerMenu();
        OpenStatsWindow();
        CurrentPage = MENUPAGE.STAT;
    }
    private void PopupPlayerMenu()
    {
        isPopupPlayerBar = true;
        playerMenuBar.SetActive(true);
    }
    public void ClosePlayerMenu()
    {
        isPopupPlayerBar = false;
        playerMenuBar.SetActive(false);
    }
    public void RefreshStat()
    {
        statsImage.sprite = SC_PlayerMgr._playerMgr.Image;
        jobText.text = jobHead + SC_PlayerMgr._playerMgr.Job;
        hpText.text = hpHead + SC_PlayerMgr._playerMgr.CurrentHP + "/" + SC_PlayerMgr._playerMgr.MaxHP;
        atkText.text = atkHead + SC_PlayerMgr._playerMgr.ATK;
        defText.text = defHead + SC_PlayerMgr._playerMgr.DEF;
        spdText.text = spdHead + SC_PlayerMgr._playerMgr.SPD;
        moneyText.text = moneyHead + SC_PlayerMgr._playerMgr.Money;
    }
    public void RefreshMoneyIndi()
    {
        moneyIndiText.text = moneyHead + SC_PlayerMgr._playerMgr.Money;
    }
    public void OpenStatsWindow()
    {
        RefreshStat();
        title.SetActive(false);
        statsWindow.SetActive(true);
        skillWindow.SetActive(false);
        fieldBSkillWindow.SetActive(false);
        battleBSkillWindow.SetActive(false);
        itemWindow.SetActive(false);
        innItemWindow.SetActive(false);
        moneyIndi.SetActive(false);
        buttons.SetActive(true);
        statButton.SetActive(true);
        statImage.color = Color.yellow;
        skillImage.color = Color.white;
        itemImage.color = Color.white;
        fieldBSkillButton.SetActive(false);
        battelBSkillButton.SetActive(false);
    }
    public void OpenSkillWindow()
    {
        title.SetActive(false);
        statsWindow.SetActive(false);
        skillWindow.SetActive(true);
        fieldBSkillWindow.SetActive(false);
        battleBSkillWindow.SetActive(false);
        itemWindow.SetActive(false);
        innItemWindow.SetActive(false);
        moneyIndi.SetActive(false);
        buttons.SetActive(true);
        if(CurrentPage == MENUPAGE.B_BSKILL)
        {
            statButton.SetActive(false);
            fieldBSkillButton.SetActive(false);
            battelBSkillButton.SetActive(true);
            battleBSkillImage.color = Color.white;
            skillImage.color = Color.yellow;
            itemImage.color = Color.white;
        }
        else if(CurrentPage == MENUPAGE.F_BSKILL)
        {
            statButton.SetActive(false);
            fieldBSkillButton.SetActive(true);
            battelBSkillButton.SetActive(false);
            fieldBSkillImage.color = Color.white;
            skillImage.color = Color.yellow;
            itemImage.color = Color.white;
        }
        else
        {
            statButton.SetActive(true);
            fieldBSkillButton.SetActive(false);
            battelBSkillButton.SetActive(false);
            statImage.color = Color.white;
            skillImage.color = Color.yellow;
            itemImage.color = Color.white;
        }
    }
    public void OpenItemWindow()
    {
        title.SetActive(false);
        statsWindow.SetActive(false);
        skillWindow.SetActive(false);
        fieldBSkillWindow.SetActive(false);
        battleBSkillWindow.SetActive(false);
        itemWindow.SetActive(true);
        innItemWindow.SetActive(false);
        moneyIndi.SetActive(false);
        buttons.SetActive(true);
        if (CurrentPage == MENUPAGE.B_BSKILL)
        {
            statButton.SetActive(false);
            fieldBSkillButton.SetActive(false);
            battelBSkillButton.SetActive(true);
            battleBSkillImage.color = Color.white;
            skillImage.color = Color.white;
            itemImage.color = Color.yellow;
        }
        else if (CurrentPage == MENUPAGE.F_BSKILL)
        {
            statButton.SetActive(false);
            fieldBSkillButton.SetActive(true);
            battelBSkillButton.SetActive(false);
            fieldBSkillImage.color = Color.white;
            skillImage.color = Color.white;
            itemImage.color = Color.yellow;
        }
        else
        {
            statButton.SetActive(true);
            fieldBSkillButton.SetActive(false);
            battelBSkillButton.SetActive(false);
            statImage.color = Color.white;
            skillImage.color = Color.white;
            itemImage.color = Color.yellow;
        }
    }
    public void OpenBuyWindow()
    {
        titleText.text = title_buy;
        title.SetActive(true);
        statsWindow.SetActive(false);
        skillWindow.SetActive(false);
        fieldBSkillWindow.SetActive(false);
        battleBSkillWindow.SetActive(false);
        itemWindow.SetActive(false);
        innItemWindow.SetActive(true);
        moneyIndi.SetActive(true);
        RefreshMoneyIndi();
        buttons.SetActive(false);
    }
    public void PopupBuyMenu()
    {
        PopupPlayerMenu();
        OpenBuyWindow();
        CurrentPage = MENUPAGE.BUY;
    }
    public void OpenSellWindow()
    {
        titleText.text = title_sell;
        title.SetActive(true);
        statsWindow.SetActive(false);
        skillWindow.SetActive(false);
        fieldBSkillWindow.SetActive(false);
        battleBSkillWindow.SetActive(false);
        itemWindow.SetActive(true);
        innItemWindow.SetActive(false);
        moneyIndi.SetActive(true);
        RefreshMoneyIndi();
        buttons.SetActive(false);
    }
    public void PopupSellMenu()
    {
        PopupPlayerMenu();
        OpenSellWindow();
        CurrentPage = MENUPAGE.SELL;
    }
    public void OpenFBSWindow()
    {
        titleText.text = title_fieldBS;
        statsWindow.SetActive(false);
        skillWindow.SetActive(false);
        fieldBSkillWindow.SetActive(true);
        battleBSkillWindow.SetActive(false);
        itemWindow.SetActive(false);
        innItemWindow.SetActive(false);
        moneyIndi.SetActive(false);
        buttons.SetActive(true);
        statButton.SetActive(false);
        fieldBSkillButton.SetActive(true);
        battelBSkillButton.SetActive(false);
        fieldBSkillImage.color = Color.yellow;
        skillImage.color = Color.white;
        itemImage.color = Color.white;
    }
    public void PopupFBSMenu()
    {
        PopupPlayerMenu();
        OpenFBSWindow();
        CurrentPage = MENUPAGE.F_BSKILL;
    }
    public void OpenBBSWindow()
    {
        titleText.text = title_battleBS;
        statsWindow.SetActive(false);
        skillWindow.SetActive(false);
        fieldBSkillWindow.SetActive(false);
        battleBSkillWindow.SetActive(true);
        itemWindow.SetActive(false);
        innItemWindow.SetActive(false);
        moneyIndi.SetActive(false);
        buttons.SetActive(true);
        statButton.SetActive(false);
        fieldBSkillButton.SetActive(false);
        battelBSkillButton.SetActive(true);
        battleBSkillImage.color = Color.yellow;
        skillImage.color = Color.white;
        itemImage.color = Color.white;
    }
    public void PopupBBSMenu()
    {
        PopupPlayerMenu();
        OpenBBSWindow();
        CurrentPage = MENUPAGE.B_BSKILL;
    }
    public void OptionMenuOnOff()
    {
        if (optionWindow.activeSelf)
            optionWindow.SetActive(false);
        else
            optionWindow.SetActive(true);
    }
    public void ExitMenuOnOff()
    {
        if (exitWindow.activeSelf)
            ExitMenuOff();
        else
            ExitMenuOn();
    }
    public void ExitMenuOff()
    {
        exitWindow.SetActive(false);
        SC_GameMgr._gameMgr.isPopupFABar = false;
    }
    public void ExitMenuOn()
    {
        exitWindow.SetActive(true);
        SC_GameMgr._gameMgr.isPopupFABar = true;
    }
    public void HelpMenuOnOff()
    {
        if (helpWindow.activeSelf)
            helpWindow.SetActive(false);
        else
            helpWindow.SetActive(true);
    }
}
