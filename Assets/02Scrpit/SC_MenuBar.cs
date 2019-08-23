using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MENUPAGE { STAT=1, SKILL, ITEM, BF_SKILL, BB_SKILL, BUY, SELL };

public class SC_MenuBar : MonoBehaviour
    //메뉴바 관리. 메뉴바에 표시되는 내용들을 담당
{
    public static SC_MenuBar _menuBar;
    public GameObject playerMenuBar;

    public bool isNomalMenu;
    public bool isFieldMenu;
    public bool isInnMenu;

    public MENUPAGE CurrentPage;

    public GameObject buttons;
    public GameObject statButton;
    public GameObject skillButton;
    public GameObject itemButton;
    public GameObject fieldBSkillButton;
    public GameObject battelBSkillButton;

    public GameObject statsWindow;
    public Text titleText;
    public const string title_stat = "능력치";
    public const string title_skill = "기술";
    public const string title_itme = "소지품";
    public const string title_buy = "구매";
    public const string title_sell = "판매";
    public const string title_fieldBS = "기본기술-탐험";
    public const string title_battleBS = "기본기술-전투";
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

    private void Awake()
    {
        _menuBar = this;
    }
    public void PopupPlayerMenu()
    {
        titleText.text = title_stat;
        SC_GameMgr._gameMgr.isPopupPlayerBar = true;
        playerMenuBar.SetActive(true);
        OpenStatsWindow();
        CurrentPage = MENUPAGE.STAT;
    }
    public void ClosePlayerMenu()
    {
        SC_GameMgr._gameMgr.isPopupPlayerBar = false;
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
    public void OpenStatsWindow()
    {
        RefreshStat();
        titleText.text = title_stat;
        statsWindow.SetActive(true);
        skillWindow.SetActive(false);
        fieldBSkillWindow.SetActive(false);
        battleBSkillWindow.SetActive(false);
        itemWindow.SetActive(false);
        buttons.SetActive(true);
        statButton.SetActive(true);
        fieldBSkillButton.SetActive(false);
        battelBSkillButton.SetActive(false);
        CurrentPage = MENUPAGE.STAT;
    }
    public void OpenSkillWindow()
    {
        titleText.text = title_skill;
        statsWindow.SetActive(false);
        skillWindow.SetActive(true);
        fieldBSkillWindow.SetActive(false);
        battleBSkillWindow.SetActive(false);
        itemWindow.SetActive(false);
        buttons.SetActive(true);
        statButton.SetActive(true);
        fieldBSkillButton.SetActive(false);
        battelBSkillButton.SetActive(false);
        CurrentPage = MENUPAGE.SKILL;
    }
    public void OpenItemWindow()
    {
        titleText.text = title_itme;
        statsWindow.SetActive(false);
        skillWindow.SetActive(false);
        fieldBSkillWindow.SetActive(false);
        battleBSkillWindow.SetActive(false);
        itemWindow.SetActive(true);
        buttons.SetActive(true);
        statButton.SetActive(true);
        fieldBSkillButton.SetActive(false);
        battelBSkillButton.SetActive(false);
        CurrentPage = MENUPAGE.ITEM;
    }
}
