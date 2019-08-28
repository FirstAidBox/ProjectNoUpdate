using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum SCENEINFO { START=1, CHARSELECT, INN, AREASELECT, FIELD };

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
    public bool isBaseTextHide = false;

    public Button[] buttons;
    public EventTrigger[] eventTriggers;
    public GameObject offWhileEvent;
    public Button[] systemButtons;

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
    public SC_StringMgr _stringMgr;

    public GameObject finalAnswerBar;
    public bool isPopupFABar = false;
    public bool trigger_FA_Yes = false;
    public Action finalAnswerAction;

    public Image fadeImage;
	public bool isFade = false;
    public bool isFadeOut = false;
	public bool isFadeIn = false;
    public bool isPause = false;

    public SC_Indicator[] mainIndicator;

    public int areaNum = 0;
    public bool[] isAreaClear;

    public GameObject inn;
    public GameObject innMenu;
    public Vector2 innMasterPos = new Vector2(3f, 0f);
    public bool isRest = false;

    void Awake()
    {
        _gameMgr = this;
        _resourceMgr = GetComponent<SC_ResourceMgr>();
        _stringMgr = GetComponent<SC_StringMgr>();

        buttons = FindObjectsOfType<Button>();
        eventTriggers = new EventTrigger[buttons.Length];
        for (int n = 0; n < buttons.Length; n++)
        {
            eventTriggers[n] = buttons[n].gameObject.GetComponent<EventTrigger>();
        }
        for (int n = 0; n < systemButtons.Length; n++)
        {
            systemButtons[n].gameObject.SetActive(true);
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
    /// <summary>
    /// 하단 텍스트 박스에 문자열 표시.
    /// </summary>
    /// <param name="inputSprite">같이 표시할 이미지</param>
    /// <param name="inputText">표시할 문자열</param>
    public void PrintTextBox(Sprite inputSprite, string inputText)
    {
        mainText.text = inputText;
        mainSprite.sprite = inputSprite;
        mainSprite.color = baseColor;
    }
    /// <summary>
    /// 하단 텍스트 박스에 문자열 표시.
    /// </summary>
    /// <param name="inputSprite">같이 표시할 이미지</param>
    /// <param name="inputText">표시할 문자열</param>
    /// <param name="inputColor">이미지 색상</param>
    public void PrintTextBox(Sprite inputSprite ,string inputText, Color inputColor)
    {
        mainText.text = inputText;
        mainSprite.sprite = inputSprite;
        mainSprite.color = inputColor;
    }
    private IEnumerator _PrintClickTextBox(string inputText)
    {
        OffButtons();
        offWhileEvent.SetActive(false);
        isPlayingText = true;
        PrintTextBox(inputText);
        yield return waitEvent;
        trigger_Click = true;
        PrintTextBox(inputText + clickString);
        yield return waitClick;
        isPlayingText = false;
        PrintBaseBox();
        OnButtons();
        offWhileEvent.SetActive(true);
    }
    /// <summary>
    /// 하단 텍스트 박스를 클릭하기 전까지 문자열을 출력시켜준다. 활성중엔 텍스트 박스를 제외한 다른 게임오브젝트들과 상호작용 불가능.(시스템 버튼은 예외)
    /// waitEvent와 연계해서 특정 이벤트를 보기 전까진 텍스트박스 클릭 비활성화 가능.
    /// </summary>
    /// <param name="inputText">표시할 문자열</param>
    public void PrintClickTextBox(string inputText)
    {
        StartCoroutine("_PrintClickTextBox", inputText);
    }
    /// <summary>
    /// PrintBaseBox 함수 호출 시 출력될 문자열을 설정한다.
    /// </summary>
    /// <param name="inputText">출력될 문자열</param>
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
        if (!isBaseTextHide)
        {
            mainText.text = baseText;
            mainSprite.sprite = baseSprite;
            mainSprite.color = baseColor;
        }
    }
    /*구버전 이벤트 처리기. Action 사용버전이 문제 없으면 삭제할 것
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
                SC_MenuBar._menuBar.Invoke(inputEventName, 0f);
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
                SC_MenuBar._menuBar.Invoke(inputEventName, delayTime);
                break;
        }
    }*/
    public void OffMainIndicatorCollider()
    {
        for (int i = 0; i < mainIndicator.Length; i++)
            mainIndicator[i].ColliderOff();
    }
    public void OnMainIndicatorCollider()
    {
        for (int i = 0; i < mainIndicator.Length; i++)
            mainIndicator[i].ColliderOn();
    }
    public void OffMainIndicator()
    {
        for (int i = 0; i < mainIndicator.Length; i++)
            mainIndicator[i].gameObject.SetActive(false);
    }
    public void OnMainIndicator()
    {
        for (int i = 0; i < mainIndicator.Length; i++)
            mainIndicator[i].gameObject.SetActive(true);
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
    public void OffSysButtons()
    {
        for (int n = 0; n < systemButtons.Length; n++)
        {
            systemButtons[n].enabled = false;
        }
    }
    public void OnSysButtons()
    {
        for (int n = 0; n < systemButtons.Length; n++)
        {
            systemButtons[n].enabled = true;
        }
    }
    public void PopupFinalAnswerBar(Action eventName, string inputText)
    {
        finalAnswerBar.SetActive(true);
        trigger_FA_Yes = false;
        isPopupFABar = true;
        PrintTextBox(inputText);
        finalAnswerAction = eventName;
        OffButtons();
    }
    public void YesClickFinalAnswer()
    {
        trigger_FA_Yes = true;
        finalAnswerAction();
        finalAnswerBar.SetActive(false);
        isPopupFABar = false;
        OnButtons();
        SC_SoundMgr._soundMgr.SFX_ClickOK();
    }
    public void NoClickFinalAnswer()
    {
        if (isPause)
        {
            Time.timeScale = 1;
            isPause = false;
        }
        PrintBaseBox();
        finalAnswerBar.SetActive(false);
        isPopupFABar = false;
        OnButtons();
        SC_SoundMgr._soundMgr.SFX_ClickOK();
    }
    private IEnumerator PlayFadeOut()
    {
		isFade = true;
		isFadeOut = true;
        OffButtons();
        OffSysButtons();
        for (float f=0f; f<1.1f; f+=0.1f)
        {
            Color c = fadeImage.color;
            c.a = f;
            fadeImage.color = c;
            yield return delay100ms;
        }
		isFadeOut = false;
    }
    private IEnumerator PlayFadeIn()
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
        OnSysButtons();
		isFadeIn = false;
		isFade = false;
    }
    private IEnumerator _WaitFadeOut(string name)
    {
        yield return waitFadeOut;
        Invoke(name, 0f);
    }
    [Obsolete("메소드를 직접전달하는 함수로 바꿀 것.")]
    public void InvokeWaitFadeOut(string name)
    {
		StartCoroutine("_WaitFadeOut", name);
    }
	private IEnumerator _WaitFadeOut (Action method)
	{
		yield return waitFadeOut;
        method();
	}
    /// <summary>
    /// 해당 함수(메소드)를 페이드 아웃이 완전히 완료 된 이후에 실행합니다.
    /// </summary>
    /// <param name="method">실행시킬 함수(메소드)</param>
	public void InvokeWaitFadeOut(Action method)
	{
		StartCoroutine (_WaitFadeOut(method));
	}
    private IEnumerator _WaitEvent(Action method)
    {
        yield return waitEvent;
        method();
    }
    public void InvokeWaitEvent(Action method)
    {
        StartCoroutine(_WaitEvent(method));
    }
    private IEnumerator _WaitEvent(Action<int> method, int i)
    {
        yield return waitEvent;
        method(i);
    }
    public void InvokeWaitEvent(Action<int> method, int i)
    {
        StartCoroutine(_WaitEvent(method, i));
    }
    public void FadeOutAndIn()
    {
		StartCoroutine(PlayFadeOut());
		StartCoroutine(PlayFadeIn());
    }
    public void FadeOut()
    {
        StartCoroutine(PlayFadeOut());
    }
    public void FadeIn()
    {
        StartCoroutine(PlayFadeIn());
    }
    public void SelectChar()
    {
        Vector2 offset = new Vector2(2.5f, 0f);
        Vector2 indi0Pos = new Vector2(-2.5f, 0.5f);
        Vector2 indi1Pos = indi0Pos + offset;
        Vector2 indi2Pos = indi1Pos + offset;
        innMenu.SetActive(false);
        SC_MenuBar._menuBar.playerMenuBar.SetActive(false);
        SC_FieldMgr._fieldMgr.actionBar.SetActive(false);
        SC_FieldMgr._fieldMgr.executeButton.gameObject.SetActive(false);
        SC_PlayerMgr._playerMgr.PlayerInfoInit();
        baseText = "캐릭터를 선택해주세요.";
        PrintBaseBox();
        mainIndicator[0].gameObject.transform.position = indi0Pos;
        mainIndicator[0].IndicatorMakeup(_resourceMgr.sp_Warrior, _stringMgr.warriorPick, AnswerWarriorPick);
        mainIndicator[1].gameObject.transform.position = indi1Pos;
        mainIndicator[1].IndicatorMakeup(_resourceMgr.sp_Mage, _stringMgr.magePick, AnswerMagePick);
        mainIndicator[2].gameObject.transform.position = indi2Pos;
        mainIndicator[2].IndicatorMakeup(_resourceMgr.sp_Ranger, _stringMgr.rangerPick, AnswerRangerPick);
    }

    public void AnswerWarriorPick()
    {
        if (!trigger_FA_Yes)
        {
            PopupFinalAnswerBar(AnswerWarriorPick, "정말 전사를 선택하시겠습니까?");
        }
        else
        {
            SC_PlayerMgr._playerMgr.GetJob(Resources.Load<SBO_PlayerJobData>("player/jobdata/Warrior"));
            GameStart();
            trigger_FA_Yes = false;
        }
    }
    public void AnswerMagePick()
    {
        if (!trigger_FA_Yes)
        {
            PopupFinalAnswerBar(AnswerMagePick, "정말 마법사를 선택하시겠습니까?");
        }
        else
        {
            SC_PlayerMgr._playerMgr.GetJob(Resources.Load<SBO_PlayerJobData>("player/jobdata/Mage"));
            GameStart();
            trigger_FA_Yes = false;
        }
    }
    public void AnswerRangerPick()
    {
        if (!trigger_FA_Yes)
        {
            PopupFinalAnswerBar(AnswerRangerPick, "정말 순찰자를 선택하시겠습니까?");
        }
        else
        {
            SC_PlayerMgr._playerMgr.GetJob(Resources.Load<SBO_PlayerJobData>("player/jobdata/Ranger"));
            GameStart();
            trigger_FA_Yes = false;
        }
    }
    public void GameStart()
    {
        EnteringInn();
        PrintTextBox("게임을 시작합니다.");
		InvokeWaitFadeOut(SC_PlayerMgr._playerMgr.CharaMake);
    }
    
    public void EnteringInn()
    {
        PrintTextBox(_stringMgr.st_enterInnStart);
        SC_PlayerMgr._playerMgr.Refill_InnItem();
        SC_PlayerMgr._playerMgr.CurrentHP = SC_PlayerMgr._playerMgr.MaxHP;
        FadeOutAndIn();
		InvokeWaitFadeOut(VisibleInn);
		InvokeWaitFadeOut(OffMainIndicator);
        isRest = true;
        SC_SoundMgr._soundMgr.SFX_FootStepStart();
        InvokeWaitFadeOut(SC_SoundMgr._soundMgr.BGM_Inn);
        InvokeWaitFadeOut(SC_SoundMgr._soundMgr.SFX_InnDoor);
        InvokeWaitFadeOut(SC_SoundMgr._soundMgr.SFX_FootStepStop);
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
            PopupFinalAnswerBar(AnswerExitInn, _stringMgr.st_answerInnExit);
        else
        {
            PrintTextBox(_stringMgr.st_innExit);
            FadeOutAndIn();
			InvokeWaitFadeOut(InvisibleInn);
			InvokeWaitFadeOut(SC_MenuBar._menuBar.ClosePlayerMenu);
            InvokeWaitFadeOut(SC_PlayerMgr._playerMgr.InvisiblePlayer);
            InvokeWaitFadeOut(SC_EnemyMgr._enemyMgr.InvisibleEnemy);
            InvokeWaitFadeOut(SelectArea);
            isRest = false;
            trigger_FA_Yes = false;
            SC_SoundMgr._soundMgr.BGM_Stop();
            SC_SoundMgr._soundMgr.SFX_InnDoor();
            SC_SoundMgr._soundMgr.SFX_FootStepStart();
            InvokeWaitFadeOut(SC_SoundMgr._soundMgr.SFX_FootStepStop);
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
    }
    public void IndiSetupArea1()
    {
        if (isAreaClear[1])
        {
            mainIndicator[0].IndicatorMakeup(_resourceMgr.sp_48_Area1, "이미 우두머리를 물리친 지역입니다.");
            mainIndicator[0].indicatorRenderer.color = new Color(0.5f, 0.5f, 0.5f);
            mainIndicator[0].isbiff = true;
        }
        else
        {
            mainIndicator[0].IndicatorMakeup(_resourceMgr.sp_48_Area1, _stringMgr.st_area1 + _stringMgr.st_tomove, AnswerArea1);
        }
        mainIndicator[0].ResizeCollider(3f);
        mainIndicator[0].transform.position = new Vector2(-4.5f, 1.5f);
    }
    public void AnswerArea1()
    {
        if(!trigger_FA_Yes)
            PopupFinalAnswerBar(AnswerArea1, "정말 지역1로 떠납니까?");
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
		InvokeWaitFadeOut(OffMainIndicator);
		InvokeWaitFadeOut(SC_PlayerMgr._playerMgr.VisiblePlayer);
        SC_FieldMgr._fieldMgr.GetArea1Sprite();
		InvokeWaitFadeOut (SC_FieldMgr._fieldMgr.EnteringField);
        SC_EnemyMgr._enemyMgr.GetArea1Data();
        InvokeWaitFadeOut(SC_EnemyMgr._enemyMgr.SetPosition);
        InvokeWaitFadeOut(SC_EnemyMgr._enemyMgr.VisibleEnemy);
        SC_SoundMgr._soundMgr.SFX_FootStepStart();
        InvokeWaitFadeOut(SC_SoundMgr._soundMgr.BGM_Area1);
        InvokeWaitFadeOut(SC_SoundMgr._soundMgr.SFX_FootStepStop);
    }
    public void IndiSetupArea2()
    {
        if (isAreaClear[2])
        {
            mainIndicator[1].IndicatorMakeup(_resourceMgr.sp_48_Area2, "이미 우두머리를 물리친 지역입니다.");
            mainIndicator[1].indicatorRenderer.color = new Color(0.5f, 0.5f, 0.5f);
            mainIndicator[1].isbiff = true;
        }
        else
        {
            mainIndicator[1].IndicatorMakeup(_resourceMgr.sp_48_Area2, _stringMgr.st_area2 + _stringMgr.st_tomove, AnswerArea2);
        }
        mainIndicator[1].ResizeCollider(3f);
        mainIndicator[1].transform.position = new Vector2(0f, 1.5f);
    }
    public void AnswerArea2()
    {
        if(!trigger_FA_Yes)
            PopupFinalAnswerBar(AnswerArea2, "정말 지역2로 떠납니까?");
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
        InvokeWaitFadeOut(OffMainIndicator);
        InvokeWaitFadeOut(SC_PlayerMgr._playerMgr.VisiblePlayer);
        SC_FieldMgr._fieldMgr.GetArea2Sprite();
        InvokeWaitFadeOut(SC_FieldMgr._fieldMgr.EnteringField);
        SC_EnemyMgr._enemyMgr.GetArea2Data();
        InvokeWaitFadeOut(SC_EnemyMgr._enemyMgr.SetPosition);
        InvokeWaitFadeOut(SC_EnemyMgr._enemyMgr.VisibleEnemy);
        SC_SoundMgr._soundMgr.SFX_FootStepStart();
        InvokeWaitFadeOut(SC_SoundMgr._soundMgr.BGM_Area2);
        InvokeWaitFadeOut(SC_SoundMgr._soundMgr.SFX_FootStepStop);
    }
    public void IndiSetupArea3()
    {
        if (isAreaClear[3])
        {
            mainIndicator[2].IndicatorMakeup(_resourceMgr.sp_48_Area3, "이미 우두머리를 물리친 지역입니다.");
            mainIndicator[2].indicatorRenderer.color = new Color(0.5f, 0.5f, 0.5f);
            mainIndicator[2].isbiff = true;
        }
        else
        {
            mainIndicator[2].IndicatorMakeup(_resourceMgr.sp_48_Area3, _stringMgr.st_area3 + _stringMgr.st_tomove, AnswerArea3);
        }
        mainIndicator[2].ResizeCollider(3f);
        mainIndicator[2].transform.position = new Vector2(4.5f, 1.5f);
    }
    public void AnswerArea3()
    {
        if(!trigger_FA_Yes)
            PopupFinalAnswerBar(AnswerArea3, "정말 지역3로 떠납니까?");
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
        InvokeWaitFadeOut(OffMainIndicator);
        InvokeWaitFadeOut(SC_PlayerMgr._playerMgr.VisiblePlayer);
        SC_FieldMgr._fieldMgr.GetArea3Sprite();
        InvokeWaitFadeOut(SC_FieldMgr._fieldMgr.EnteringField);
        SC_EnemyMgr._enemyMgr.GetArea3Data();
        InvokeWaitFadeOut(SC_EnemyMgr._enemyMgr.SetPosition);
        InvokeWaitFadeOut(SC_EnemyMgr._enemyMgr.VisibleEnemy);
        SC_SoundMgr._soundMgr.SFX_FootStepStart();
        InvokeWaitFadeOut(SC_SoundMgr._soundMgr.BGM_Area3);
        InvokeWaitFadeOut(SC_SoundMgr._soundMgr.SFX_FootStepStop);
    }

    public void AnswerExitGame()
    {
        if (!trigger_FA_Yes)
        {
            PopupFinalAnswerBar(AnswerExitGame, "정말 종료하시겠습니까?");
            isPause = true;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            isPause = false;
            trigger_FA_Yes = false;
            PrintTextBox("종료합니다.");
            FadeOut();
            InvokeWaitFadeOut(Application.Quit);
        }
    }
    public bool IsGameClear()
    {
        if (isAreaClear[1] && isAreaClear[2] && isAreaClear[3])
            return true;
        else
            return false;
    }
}
