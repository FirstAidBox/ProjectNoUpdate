using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_MenuBar : MonoBehaviour
    //메뉴바 관리. 메뉴바에 표시되는 내용들을 담당
{
    public GameObject playerMenuBar;

    public bool isNomalMenu;
    public bool isFieldMenu;
    public bool isInnMenu;

    public GameObject nomalButtons;
    public GameObject fieldButtons;

    public GameObject statsWindow;
    public Text titleText;
    public const string title_stat = "능력치";
    public const string title_skill = "기술";
    public const string title_itme = "소지품";
    public const string title_buy = "구매";
    public const string title_sell = "판매";
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
    public GameObject skillContent;
    public SC_SkillInMenu[] skillList;

    public GameObject fieldBSkillWindow;

    public GameObject battleBSkillWindow;

    public GameObject itemWindow;
    public GameObject itemContent;
    public GameObject[] itemList;

    public SC_PlayerMgr _playerMgr;

    private void Awake()
    {
        _playerMgr = GetComponent<SC_PlayerMgr>();
        skillList = skillContent.GetComponentsInChildren<SC_SkillInMenu>();
    }
    public void PopupPlayerMenu()
    {
        titleText.text = title_stat;
        SC_GameMgr._gameMgr.isPopupPlayerBar = true;
        playerMenuBar.SetActive(true);
        OpenStatsWindow();
    }
    public void ClosePlayerMenu()
    {
        SC_GameMgr._gameMgr.isPopupPlayerBar = false;
        playerMenuBar.SetActive(false);
    }
    public void RefreshStat()
    {
        statsImage.sprite = _playerMgr.Image;
        jobText.text = jobHead + _playerMgr.Job;
        hpText.text = hpHead + _playerMgr.CurrentHP + "/" + _playerMgr.MaxHP;
        atkText.text = atkHead + _playerMgr.ATK;
        defText.text = defHead + _playerMgr.DEF;
        spdText.text = spdHead + _playerMgr.SPD;
        moneyText.text = moneyHead + _playerMgr.Money;
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
        nomalButtons.SetActive(true);
        fieldButtons.SetActive(false);
    }
}
