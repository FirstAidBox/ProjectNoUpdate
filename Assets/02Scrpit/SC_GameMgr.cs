using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum EVENT_FLAG { EVENT=1, ITEM, ENEMY, MENU };
public enum CURRENT_PAGE { START=1, CHARSELECT, INN, AREASELECT, FIELD };

public static class MonoExtension
{
    public static void Invoke(this MonoBehaviour m, Action method, float time)
    {
        m.Invoke(method.Method.Name, time);
    }
    public static void Invoke(this MonoBehaviour m, Action method)
    {
        method.Invoke();
    }
}
public class SC_GameMgr : MonoBehaviour
{
    //게임의 주요 흐름을 담당하는 스크립트입니다.
    public static SC_GameMgr _gameMgr;
    public GameObject mainTextBar;
    public Text mainText;
    public Image mainSprite;
    public string baseText;
    public Sprite baseSprite;
    public Sprite nullSprite; //투명한 스프라이트
    public Color baseColor = Color.white;

    [SerializeField] private Button[] buttons;
    [SerializeField] private EventTrigger[] eventTriggers;

    public bool isEventPlaying = false;
    public bool isPlayingText = false;
    public bool trigger_Click = false;
    public WaitForSeconds delay100ms = new WaitForSeconds(0.1f); // 0.1초
    public WaitForSeconds delay2s = new WaitForSeconds(2.0f);
    public WaitWhile waitFadeOut;
	public WaitWhile waitFadeIn;
    public WaitWhile waitEvent;
    public WaitWhile waitText;
    public WaitWhile waitClick;
    public const string clickString = " ▶Click";

    public SC_ResourceMgr _resourceMgr;
    public SC_PlayerMgr _playerMgr;
    public SC_ItemMgr _itemMgr;
    public SC_EnemyMgr _enemyMgr;
    public SC_StringMgr _stringMgr;
    public SC_MenuBar _menuBar;

    public GameObject finalAnswerBar;
    public bool isPopupFABar = false;
    public bool trigger_FA_Yes = false;
    public string finalAnswerName;
    public EVENT_FLAG _FLAG_finalAnswer;

    public Image fadeImage;
	public bool isFade = false;
    public bool isFadeOut = false;
	public bool isFadeIn = false;

    public SC_Indicator mainIndicator1;
    public SC_Indicator mainIndicator2;
    public SC_Indicator mainIndicator3;

    public SC_PlayerIndi playerIndicator;
    public Vector2 currentPlayerPos = new Vector2(-4f, 0f);
    public bool isPopupPlayerBar = false;

    public SC_Indicator monsterIndicator;
    public int areaNum = 0;
    public bool[] isAreaClear;

    public GameObject inn;
    public GameObject innMenu;
    public Vector2 innMasterPos = new Vector2(3f, 0f);
    public bool isInInn = false;

    void Awake()
    {
        _gameMgr = this;
        _resourceMgr = GetComponent<SC_ResourceMgr>();
        _playerMgr = GetComponent<SC_PlayerMgr>();
        _itemMgr = GetComponent<SC_ItemMgr>();
        _enemyMgr = GetComponent<SC_EnemyMgr>();
        _stringMgr = GetComponent<SC_StringMgr>();
        _menuBar = GetComponent<SC_MenuBar>();

        buttons = FindObjectsOfType<Button>();
        eventTriggers = new EventTrigger[buttons.Length];
        for (int n = 0; n < buttons.Length; n++)
        {
            eventTriggers[n] = buttons[n].gameObject.GetComponent<EventTrigger>();
        }
        baseText = null;
        baseSprite = nullSprite;

        isAreaClear = new bool[4];//가독성을 위해 [0]부터[3]까지 할당

		waitFadeOut = new WaitWhile(() => isFadeOut); //IEnumerator용 isFadeOut이 false 될 때 까지 대기.(페이드아웃 중 True)
		waitFadeIn = new WaitWhile(() => isFadeIn); //IEnumerator용 isFadeIn이 false 될 때 까지 대기.(페이드인 중 True)
        waitEvent = new WaitWhile(() => isEventPlaying); //IEnumerator용 isEventPlaying가 false 될 때 까지 대기.
        waitText = new WaitWhile(() => isPlayingText); //IEnumerator용 isPlayingText가 false 될 때 까지 대기.
        waitClick = new WaitWhile(() => trigger_Click); //IEnumerator용 trigger_Click가 false 될 때 까지 대기.
    }
    private void Start()
    {
        SelectChar();
    }
    /// <summary>
    /// 하단 텍스트 박스에 문자열 표시. 왼쪽의 이미지 박스는 공백.
    /// </summary>
    /// <param name="inputText">표시할 문자열</param>
    public void PrintTextBox(string inputText)
    {
        mainText.text = inputText;
        mainSprite.sprite = nullSprite;
        mainSprite.color = baseColor;
    }
    public void PrintTextBox(Sprite inputSprite, string inputText)
    {
        mainText.text = inputText;
        mainSprite.sprite = inputSprite;
        mainSprite.color = baseColor;
    }
    public void PrintTextBox(Sprite inputSprite ,string inputText, Color inputColor)
    {
        mainText.text = inputText;
        mainSprite.sprite = inputSprite;
        mainSprite.color = inputColor;
    }
    private IEnumerator _PrintClickTextBox(string inputText)
    {
        isPlayingText = true;
        PrintTextBox(inputText);
        yield return delay2s;
        trigger_Click = true;
        PrintTextBox(inputText + clickString);
        yield return waitClick;
        isPlayingText = false;
        PrintBaseBox();
    }
    public void PrintClickTextBox(string inputText)
    {
        StartCoroutine("_PrintClickTextBox", inputText);
    }
    public void SetBaseText(string inputText)
    {
        baseSprite = nullSprite;
        baseText = inputText;
    }
    public void SetBaseText(Sprite inputSprite, string inputText)
    {
        baseSprite = inputSprite;
        baseText = inputText;
    }
    /// <summary>
    /// baseText와 baseSprite로 하단 박스 내용 표시.
    /// </summary>
    public void PrintBaseBox()
    {
        mainText.text = baseText;
        mainSprite.sprite = baseSprite;
        mainSprite.color = baseColor;
    }
    /// <summary>
    /// 이벤트처리기. 입력된 프로퍼티에 따라 이벤트(함수)를 실행시킵니다.
    /// </summary>
    /// <param name="inputEventName">실행할 이벤트(함수)의 이름</param>
    /// <param name="inputFLAG">이벤트의 종류</param>
    public void EventExecute(string inputEventName, EVENT_FLAG inputFLAG)
    {
        switch (inputFLAG)
        {
            case EVENT_FLAG.EVENT:
                Invoke(inputEventName, 0f);
                break;
            case EVENT_FLAG.ITEM:
                _itemMgr.Invoke(inputEventName, 0f);
                break;
            case EVENT_FLAG.ENEMY:
                _enemyMgr.Invoke(inputEventName, 0f);
                break;
            case EVENT_FLAG.MENU:
                _menuBar.Invoke(inputEventName, 0f);
                break;
        }
    }
    /// <summary>
    /// 이벤트처리기. 입력된 프로퍼티에 따라 이벤트(함수)를 실행시킵니다.
    /// </summary>
    /// <param name="inputEventName">실행할 이벤트(함수)의 이름</param>
    /// <param name="inputFLAG">이벤트의 종류</param>
    /// <param name="delayTime">실행 지연시간</param>
    public void EventExecute(string inputEventName, EVENT_FLAG inputFLAG, float delayTime)
    {
        switch (inputFLAG)
        {
            case EVENT_FLAG.EVENT:
                Invoke(inputEventName, delayTime);
                break;
            case EVENT_FLAG.ITEM:
                _itemMgr.Invoke(inputEventName, delayTime);
                break;
            case EVENT_FLAG.ENEMY:
                _enemyMgr.Invoke(inputEventName, delayTime);
                break;
            case EVENT_FLAG.MENU:
                _menuBar.Invoke(inputEventName, delayTime);
                break;
        }
    }
    public void OffMainIndicatorCollider()
    {
        mainIndicator1.indicatorCollider.enabled = false;
        mainIndicator2.indicatorCollider.enabled = false;
        mainIndicator3.indicatorCollider.enabled = false;
    }
    public void OnMainIndicatorCollider()
    {
        mainIndicator1.indicatorCollider.enabled = true;
        mainIndicator2.indicatorCollider.enabled = true;
        mainIndicator3.indicatorCollider.enabled = true;
    }
    public void OffMainIndicator()
    {
        mainIndicator1.gameObject.SetActive(false);
        mainIndicator2.gameObject.SetActive(false);
        mainIndicator3.gameObject.SetActive(false);
    }
    public void OnMainIndicator()
    {
        mainIndicator1.gameObject.SetActive(true);
        mainIndicator2.gameObject.SetActive(true);
        mainIndicator3.gameObject.SetActive(true);
    }
    /// <summary>
    /// 화면 중앙에 마지막으로 묻는 질문버튼 출력
    /// </summary>
    /// <param name="eventName">Yes 버튼을 누르면 실행될 이벤트(함수)이름</param>
    /// <param name="inputText">팝업 되고있는 동안 출력될 텍스트</param>
    /// <param name="inputFLAG">실행될 이벤트의 종류</param>
    public void PopupFinalAnswerBar(string eventName, string inputText, EVENT_FLAG inputFLAG)
    {
        finalAnswerBar.SetActive(true);
        trigger_FA_Yes = false;
        isPopupFABar = true;
        PrintTextBox(inputText);
        finalAnswerName = eventName;
        _FLAG_finalAnswer = inputFLAG;
        OffButtons();
    }
    public void PopupFinalAnswerBar(Action eventName, string inputText, EVENT_FLAG inputFLAG)
    {
        finalAnswerBar.SetActive(true);
        trigger_FA_Yes = false;
        isPopupFABar = true;
        PrintTextBox(inputText);
        finalAnswerName = eventName.Method.Name;
        _FLAG_finalAnswer = inputFLAG;
        OffButtons();
    }
    public void YesClickFinalAnswer()
    {
        trigger_FA_Yes = true;
        EventExecute(finalAnswerName, _FLAG_finalAnswer);
        finalAnswerBar.SetActive(false);
        isPopupFABar = false;
        OnButtons();
    }
    public void NoClickFinalAnswer()
    {
        PrintBaseBox();
        finalAnswerBar.SetActive(false);
        isPopupFABar = false;
        OnButtons();
    }
    public IEnumerator PlayFadeOut()
    {
		isFade = true;
		isFadeOut = true;
        OffButtons();
        for (float f=0f; f<1.1f; f+=0.1f)
        {
            Color c = fadeImage.color;
            c.a = f;
            fadeImage.color = c;
            yield return delay100ms;
        }
		isFadeOut = false;
    }
    public IEnumerator PlayFadeIn()
    {
        yield return waitFadeOut;
		isFadeIn = true;
        for (float f = 1f; f > -0.1f; f -= 0.1f)
        {
            Color c = fadeImage.color;
            c.a = f;
            fadeImage.color = c;
            yield return delay100ms;
        }
        OnButtons();
		isFadeIn = false;
		isFade = false;
    }
    public IEnumerator _WaitFadeOut(string name)
    {
        yield return waitFadeOut;
        Invoke(name, 0f);
    }
    public void InvokeWaitFadeOut(string name)
    {
		StartCoroutine("_WaitFadeOut", name);
    }
	public IEnumerator _WaitFadeOut (Action method)
	{
		yield return waitFadeOut;
		method.Invoke ();
	}
	public void InvokeWaitFadeOut(Action method)
	{
		StartCoroutine ("_WaitFadeOut", method);
	}
    public void OffButtons()
    {
        for (int n = 0; n < buttons.Length; n++)
        {
            buttons[n].interactable = false;
            eventTriggers[n].enabled = false;
        }
    }
    public void OnButtons()
    {
        for (int n = 0; n < buttons.Length; n++)
        {
            buttons[n].interactable = true;
            eventTriggers[n].enabled = true;
        }
    }
    public void FadeOutAndIn()
    {
		StartCoroutine(PlayFadeOut());
		StartCoroutine(PlayFadeIn());
    }
    public void SelectChar()
    {
        innMenu.SetActive(false);
        _menuBar.playerMenuBar.SetActive(false);
        SC_FieldMgr._fieldMgr.actionBar.SetActive(false);
        baseText = "캐릭터를 선택해주세요.";
        PrintBaseBox();
        mainIndicator1.IndicatorMakeup(_resourceMgr.sp_Warrior, _stringMgr.warriorPick, "AnswerWarriorPick", EVENT_FLAG.EVENT);
        mainIndicator2.IndicatorMakeup(_resourceMgr.sp_Mage, _stringMgr.magePick, "AnswerMagePick", EVENT_FLAG.EVENT);
        mainIndicator3.IndicatorMakeup(_resourceMgr.sp_Ranger, _stringMgr.rangerPick, "AnswerRangerPick", EVENT_FLAG.EVENT);
    }

    public void AnswerWarriorPick()
    {
        if (!trigger_FA_Yes)
        {
            string eventText = "정말 전사를 선택하시겠습니까?";
            EVENT_FLAG _FLAG = EVENT_FLAG.EVENT;
            PopupFinalAnswerBar(AnswerWarriorPick, eventText, _FLAG);
        }
        else
        {
            _playerMgr.GetJob(Resources.Load<SBO_PlayerJobData>("player/jobdata/Warrior"));
            GameStart();
            trigger_FA_Yes = false;
        }
    }
    public void AnswerMagePick()
    {
        if (!trigger_FA_Yes)
        {
            string eventText = "정말 마법사를 선택하시겠습니까?";
            EVENT_FLAG _FLAG = EVENT_FLAG.EVENT;
            PopupFinalAnswerBar(AnswerMagePick, eventText, _FLAG);
        }
        else
        {
            _playerMgr.GetJob(Resources.Load<SBO_PlayerJobData>("player/jobdata/Mage"));
            GameStart();
            trigger_FA_Yes = false;
        }
    }
    public void AnswerRangerPick()
    {
        if (!trigger_FA_Yes)
        {
            string eventText = "정말 순찰자를 선택하시겠습니까?";
            EVENT_FLAG _FLAG = EVENT_FLAG.EVENT;
            PopupFinalAnswerBar(AnswerRangerPick, eventText, _FLAG);
        }
        else
        {
            _playerMgr.GetJob(Resources.Load<SBO_PlayerJobData>("player/jobdata/Ranger"));
            GameStart();
            trigger_FA_Yes = false;
        }
    }
    public void GameStart()
    {
        EnteringInn();
        PrintTextBox("게임을 시작합니다.");
		InvokeWaitFadeOut("CharaMake");
    }
    public void CharaMake()
    {
        playerIndicator.IndicatorMakeup(_playerMgr.Image, "캐릭터의 정보를 확인합니다.", "PopupPlayerMenu", EVENT_FLAG.MENU);
        playerIndicator.gameObject.transform.position = currentPlayerPos;
    }
    public void VisiblePlayer()
    {
        playerIndicator.gameObject.SetActive(true);
    }
    public void InvisiblePlayer()
    {
        playerIndicator.gameObject.SetActive(false);
    }
    public void EnteringInn()
    {
        PrintTextBox(_stringMgr.st_enterInnStart);
        FadeOutAndIn();
		InvokeWaitFadeOut("VisibleInn");
		InvokeWaitFadeOut("OffMainIndicator");
        isInInn = true;
    }
    public void VisibleInn()
    {
        inn.SetActive(true);
        innMenu.SetActive(true);
        baseText = _stringMgr.st_enterInnEnd;
        PrintBaseBox();
    }
    public void InvisibleInn()
    {
        inn.SetActive(false);
        innMenu.SetActive(false);
    }
    public void AnswerExitInn()
    {
        if(!trigger_FA_Yes)
            PopupFinalAnswerBar(AnswerExitInn, _stringMgr.st_answerInnExit, EVENT_FLAG.EVENT);
        else
        {
            PrintTextBox(_stringMgr.st_innExit);
            FadeOutAndIn();
			InvokeWaitFadeOut(InvisibleInn);
			InvokeWaitFadeOut(_menuBar.ClosePlayerMenu);
			InvokeWaitFadeOut("SelectArea");
            isInInn = false;
            trigger_FA_Yes = false;
        }
    }
    public void SelectArea()
    {
        baseText = _stringMgr.st_selectArea;
        PrintBaseBox();
        IndiSetupArea1();
        IndiSetupArea2();
        IndiSetupArea3();
        OnMainIndicator();
        playerIndicator.gameObject.SetActive(false);
    }
    public void IndiSetupArea1()
    {
        if (isAreaClear[1])
        {
            mainIndicator1.IndicatorMakeup(_resourceMgr.sp_48_Area1, "이미 우두머리를 물리친 지역입니다.");
            mainIndicator1.indicatorRenderer.color = new Color(0.5f, 0.5f, 0.5f);   
        }
        else
        {
            mainIndicator1.IndicatorMakeup(_resourceMgr.sp_48_Area1, _stringMgr.st_area1 + _stringMgr.st_tomove, "AnswerArea1", EVENT_FLAG.EVENT);
        }
        mainIndicator1.ResizeCollider(3f);
        mainIndicator1.transform.position = new Vector2(-6f, 0f);
    }
    public void AnswerArea1()
    {
        if(!trigger_FA_Yes)
            PopupFinalAnswerBar(AnswerArea1, "정말 지역1로 떠납니까?", EVENT_FLAG.EVENT);
        else
        {
            GoToArea1();
            trigger_FA_Yes = false;
        }
    }
    public void GoToArea1()
    {
        areaNum = 1;
        PrintTextBox(_stringMgr.st_area1 + _stringMgr.st_moving);
        FadeOutAndIn();
		InvokeWaitFadeOut("OffMainIndicator");
		InvokeWaitFadeOut("VisiblePlayer");
        SC_FieldMgr._fieldMgr.GetArea1Sprite();
		InvokeWaitFadeOut (SC_FieldMgr._fieldMgr.EnteringField);
    }
    public void IndiSetupArea2()
    {
        mainIndicator2.IndicatorMakeup(_resourceMgr.sp_48_Area2, _stringMgr.st_area2 + _stringMgr.st_tomove, "AnswerArea2", EVENT_FLAG.EVENT);
        mainIndicator2.ResizeCollider(3f);
        mainIndicator2.transform.position = new Vector2(-1.5f, 0f);
    }
    public void AnswerArea2()
    {
        if(!trigger_FA_Yes)
            PopupFinalAnswerBar(AnswerArea2, "정말 지역2로 떠납니까?", EVENT_FLAG.EVENT);
        else
        {
            GoToArea2();
            trigger_FA_Yes = false; 
        }
    }
    public void GoToArea2()
    {
        areaNum = 2;
        PrintTextBox(_stringMgr.st_area2 + _stringMgr.st_moving);
        FadeOutAndIn();
        Invoke("OffMainIndicator", 1f);
    }
    public void IndiSetupArea3()
    {
        mainIndicator3.IndicatorMakeup(_resourceMgr.sp_48_Area3, _stringMgr.st_area3 + _stringMgr.st_tomove, "AnswerArea3", EVENT_FLAG.EVENT);
        mainIndicator3.ResizeCollider(3f);
        mainIndicator3.transform.position = new Vector2(3f, 0f);
    }
    public void AnswerArea3()
    {
        if(!trigger_FA_Yes)
            PopupFinalAnswerBar(AnswerArea3, "정말 지역3로 떠납니까?", EVENT_FLAG.EVENT);
        else
        {
            GoToArea3();
            trigger_FA_Yes = false;
        }
    }
    public void GoToArea3()
    {
        areaNum = 3;
        PrintTextBox(_stringMgr.st_area3 + _stringMgr.st_moving);
        FadeOutAndIn();
        Invoke("OffMainIndicator", 1f);
    }
}
