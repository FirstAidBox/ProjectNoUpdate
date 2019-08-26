using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BATTLEPHASE { ITEM = 0, GUARD, ATTACK, SMASH, EXTRA }

public class SC_FieldMgr : MonoBehaviour
    //필드(여관 밖 지역)에서 일어나는 일들을 담당.
{
    public static SC_FieldMgr _fieldMgr;
    public GameObject fieldBackGroup;
    public SC_FieldBack[] fieldBacks;

    public Sprite[] topSprites;
    public Sprite[] midSprites;
    public Sprite[] bottomSprites;
    public Sprite imageUnknown;

    private Vector2 frontPoint = new Vector2(16f, 0f);
    private Vector2 backMove = new Vector2(-0.1f, 0f);
    public int moveCount = 0;

    public GameObject actionBar;
    public SpriteRenderer[] actionIcons;
    public bool isPopupFieldMenu = false;
    public GameObject selecter;
    public SC_ExecuteButton executeButton;
    public int actionIndex;
    public int actionCounter = 0;

    public SC_SlotBase[] playerFieldActionSlot;
    public int CurrentTurn;
    public int TotalTurn;
    public int CurrentRound;

    public bool isInBattle = false;
    public SC_SlotBase[] playerBattleActionSlot;
    public SBO_SlotObject[] enemyBattleActionSlot;
    public BATTLEPHASE currentBattlePhase;
    public SBO_SlotObject[] playerPhaseAction;
    public SBO_SlotObject[] enemyPhaseAction;

    private void Awake()
    {
        _fieldMgr = this;
        fieldBacks = fieldBackGroup.GetComponentsInChildren<SC_FieldBack>();
        DisableFieldTiles();
        playerFieldActionSlot = new SC_SlotBase[3];
        playerBattleActionSlot = new SC_SlotBase[3];
        enemyBattleActionSlot = new SBO_SlotObject[3];
        playerPhaseAction = new SBO_SlotObject[5];
        enemyPhaseAction = new SBO_SlotObject[5];
    }

    public void GetArea1Sprite()
    {
        topSprites = Resources.LoadAll<Sprite>("sprite/field/area1/top");
        midSprites = Resources.LoadAll<Sprite>("sprite/field/area1/mid");
        bottomSprites = Resources.LoadAll<Sprite>("sprite/field/area1/bottom");
    }

    public Sprite GetRandomSprite(Sprite[] inputArray)
    {
        return inputArray[Random.Range(0, inputArray.Length)];
    }
    public void ExecuteField()
    {
        StartCoroutine(_executeField());
    }
    private IEnumerator _executeField()
    {
        SC_MenuBar._menuBar.ClosePlayerMenu();
        IndiIconField();
        IconReset();
        while (CurrentTurn < 3)
        {
            TotalTurn++;
            CurrentTurn++;
            SC_PlayerMgr._playerMgr.TurnInit();
            SC_EnemyMgr._enemyMgr.SetPosition();
            SC_EnemyMgr._enemyMgr.InputEnemyData(TotalTurn);
            MoveSelecterOnly(CurrentTurn-1);
            MoveTiles(1);
            SC_GameMgr._gameMgr.PrintClickTextBox("이동합니다");
            yield return SC_GameMgr._gameMgr.waitText;
            playerFieldActionSlot[CurrentTurn-1].UseObject();
            yield return SC_GameMgr._gameMgr.waitText;
            SC_EnemyMgr._enemyMgr.EnemyMove();
            yield return SC_GameMgr._gameMgr.waitText;
        }
        CurrentRound++;
        FieldRoundInit();
    }
    public void EnteringField()
    {
        EnableFieldTiles();
        FieldTilesInit();
        FieldRoundInit();
        TotalTurn = 0;
        CurrentRound = 0;
        actionBar.SetActive(true);
        SC_GameMgr._gameMgr.SetBaseText("3턴간 할 행동들을 선택해주세요.");
        SC_GameMgr._gameMgr.PrintClickTextBox("도착했습니다.");
    }
    public void FieldRoundInit()
    {
        for (int i = 0; i < playerFieldActionSlot.Length; i++)
            playerFieldActionSlot[i] = null;
        IconReset();
        actionCounter = 0;
        CurrentTurn = 0;
        SelecterInit();
        executeButton.UnableButton();
        executeButton.gameObject.SetActive(true);
        if (TotalTurn + 1 == SC_EnemyMgr._enemyMgr.MaxEnemyCount)
            ReadyToBossBattle();
        else
            IndiIconField();
    }
    public void FieldTilesInit()
    {
        for (int j = 0; j < fieldBacks.Length; j++)
            fieldBacks[j].SpriteChange(GetRandomSprite(topSprites), GetRandomSprite(midSprites), GetRandomSprite(bottomSprites));
    }
    public void EnableFieldTiles()
    {
        for (int j = 0; j < fieldBacks.Length; j++)
            fieldBacks[j].gameObject.SetActive(true);
    }
    public void DisableFieldTiles()
    {
        for (int j = 0; j < fieldBacks.Length; j++)
            fieldBacks[j].gameObject.SetActive(false);
    }
    public void IndiIconField()
    {
        for (int i = 0; i < SC_GameMgr._gameMgr.mainIndicator.Length; i++)
        {
            Vector2 basePos = (Vector2)actionBar.transform.position + new Vector2(i, 1f);
            SC_GameMgr._gameMgr.mainIndicator[i].gameObject.transform.position = basePos;
        }
        for (int i = 0; i < SC_PlayerMgr._playerMgr.SightRange; i++)
            SC_GameMgr._gameMgr.mainIndicator[i].IndicatorMakeup
                (SC_EnemyMgr._enemyMgr.enemyData[CurrentRound + i + 1].Image,
                SC_EnemyMgr._enemyMgr.enemyData[CurrentRound + i + 1].EnemyName);
        for (int i = SC_PlayerMgr._playerMgr.SightRange; i < 3; i++)
            SC_GameMgr._gameMgr.mainIndicator[i].IndicatorMakeup(imageUnknown, "알수없음");
        SC_GameMgr._gameMgr.OnMainIndicator();
    }
    public void MoveSelecter(int input)
    {
        if (!isInBattle)
            SC_MenuBar._menuBar.PopupFBSMenu();
        else
            SC_MenuBar._menuBar.PopupBBSMenu();
        actionIndex = input;
        selecter.transform.localPosition = new Vector2(input, 0);
        OutFromActionSlot();
        IconReset();
    }
    public void MoveSelecterOnly(int input)
    {
        actionIndex = input;
        selecter.transform.localPosition = new Vector2(input, 0);
    }
    public void SelecterInit()
    {
        actionIndex = 0;
        selecter.transform.localPosition = new Vector2(0, 0);
    }
    public void OutFromActionSlot()
    {
        if (isInBattle)
        {
            if (playerBattleActionSlot[actionIndex] != null)
            {
                playerBattleActionSlot[actionIndex].SlotCanUse();
                playerBattleActionSlot[actionIndex] = null;
                actionCounter--;
            }
        }
        else
        {
            if (playerFieldActionSlot[actionIndex] != null)
            {
                playerFieldActionSlot[actionIndex].SlotCanUse();
                playerFieldActionSlot[actionIndex] = null;
                actionCounter--;
            }
        }
        executeButton.UnableButton();
    }
    public void PLActionInputInField(SC_SlotBase inputSlot)
    {
        OutFromActionSlot();
        playerFieldActionSlot[actionIndex] = inputSlot;
        actionIcons[actionIndex].sprite = inputSlot.icon.sprite;
        actionIcons[actionIndex].color = inputSlot.icon.color;
        actionCounter++;
        if (actionCounter >= 3)
            executeButton.EnableButton();
    }
    public void PLActionInputInBattle(SC_SlotBase inputSlot)
    {
        OutFromActionSlot();
        playerBattleActionSlot[actionIndex] = inputSlot;
        actionIcons[actionIndex].sprite = inputSlot.icon.sprite;
        actionIcons[actionIndex].color = inputSlot.icon.color;
        actionCounter++;
        if (actionCounter >= 3)
            executeButton.EnableButton();
    }
    public void IconReset()
    {
		if (isInBattle) 
		{
			for (int i = 0; i < actionIcons.Length; i++) 
			{
				if (playerBattleActionSlot [i] != null) 
				{
					actionIcons [i].sprite = playerBattleActionSlot [i].slotObject.Image;
					actionIcons [i].color = playerBattleActionSlot [i].slotObject.Color;
				} 
				else
					actionIcons [i].sprite = null;
			}
		} 
		else 
		{
			for (int i = 0; i < actionIcons.Length; i++) 
			{
                if (playerFieldActionSlot[i] != null)
                {
                    actionIcons[i].sprite = playerFieldActionSlot[i].slotObject.Image;
                    actionIcons[i].color = playerFieldActionSlot[i].slotObject.Color;
                }
                else
                    actionIcons[i].sprite = null;
			}
		}
    }
    private IEnumerator _MoveTiles(int moveRange)
    {
        SC_GameMgr._gameMgr.isEventPlaying = true;
        for (int i = 0; i < moveRange*5; i++)
        {
            for (int j = 0; j < fieldBacks.Length; j++)//화면 뒤 밖에 있을 경우 앞으로 옮기고 스프라이트 변경
            {
                if (fieldBacks[j].gameObject.transform.localPosition.x <= -7.9f)
                {
                    fieldBacks[j].gameObject.transform.localPosition = (Vector2)fieldBacks[j].gameObject.transform.localPosition + frontPoint;
                    fieldBacks[j].SpriteChange(GetRandomSprite(topSprites), GetRandomSprite(midSprites), GetRandomSprite(bottomSprites));
                }
            }

            for (int k = 0; k < 10; k++)//0.1초마다 0.1f씩 뒤로 이동(-0.1f)
            {
                for (int j = 0; j < fieldBacks.Length; j++)
                {
                    fieldBacks[j].gameObject.transform.localPosition = (Vector2)fieldBacks[j].gameObject.transform.localPosition + backMove;
                }
                SC_EnemyMgr._enemyMgr.EnemyIndicator.gameObject.transform.position = (Vector2)SC_EnemyMgr._enemyMgr.EnemyIndicator.gameObject.transform.position + backMove;
                yield return SC_GameMgr._gameMgr.delay100ms;
            }
        }
        SC_GameMgr._gameMgr.isEventPlaying = false;
        yield return null;
    }
    public void MoveTiles(int moveRange)    {   StartCoroutine(_MoveTiles(moveRange));  }

    public void ReadyToBossBattle()
    {
        actionBar.SetActive(false);
        SC_GameMgr._gameMgr.OffMainIndicator();
        SC_GameMgr._gameMgr.SetBaseText("보스전투 준비중");
        SC_GameMgr._gameMgr.PrintBaseBox();
    }

    private IEnumerator _BattleSetup()
    {
        yield return SC_GameMgr._gameMgr.waitText;
        isInBattle = true;
        BattleRoundInit();
    }
    public void BattleRoundInit()
    {
        for (int i = 0; i < 3; i++)
        {
            playerBattleActionSlot[i] = null;
            enemyBattleActionSlot[i] = null;
        }
        IconReset();
        SC_EnemyMgr._enemyMgr.BattleActionInput();
        IndiIconBattle();
        SelecterInit();
        actionCounter = 0;
        executeButton.UnableButton();
        executeButton.gameObject.SetActive(true);
    }
    public void BattleSetup() { StartCoroutine(_BattleSetup()); }
    public void IndiIconBattle()
    {
        for (int i = 0; i < SC_GameMgr._gameMgr.mainIndicator.Length; i++)
        {
            Vector2 basePos = (Vector2)actionBar.transform.position + new Vector2(i, 1f);
            SC_GameMgr._gameMgr.mainIndicator[i].gameObject.transform.position = basePos;
        }
        for (int i = 0; i < SC_PlayerMgr._playerMgr.ProphecyRange; i++)
            SC_GameMgr._gameMgr.mainIndicator[i].IndicatorMakeup(enemyBattleActionSlot[i].Image, enemyBattleActionSlot[i].Text);
        for (int i = SC_PlayerMgr._playerMgr.ProphecyRange; i < 3; i++)
            SC_GameMgr._gameMgr.mainIndicator[i].IndicatorMakeup(imageUnknown, "알수없음");
        SC_GameMgr._gameMgr.OnMainIndicator();
    }
    private IEnumerator _executeBattle()
    {
        SC_MenuBar._menuBar.ClosePlayerMenu();
        for (int i = 0; i < 3; i++)
        {
            SC_PlayerMgr._playerMgr.TurnInit();
            SC_EnemyMgr._enemyMgr.TurnInit();
            for (int n = 0; n < 5; n++)
            {
                playerPhaseAction[n] = null;
                enemyPhaseAction[n] = null;
            }
            MoveSelecterOnly(i);
            (playerBattleActionSlot[i].slotObject as I_BattleStack).WhenIsUse();
            playerBattleActionSlot[i].SlotCanUse();
            (enemyBattleActionSlot[i] as I_BattleStack).WhenIsUse();
            for (currentBattlePhase = 0; (int)currentBattlePhase < 5; currentBattlePhase++)
            {
                if (SC_PlayerMgr._playerMgr.SPD >= SC_EnemyMgr._enemyMgr.SPD)
                {
                    if (playerPhaseAction[(int)currentBattlePhase] != null)
                    {
                        (playerPhaseAction[(int)currentBattlePhase] as I_CanUse).UseEffect();
                        yield return SC_GameMgr._gameMgr.waitText;
                    }
                    if (enemyPhaseAction[(int)currentBattlePhase] != null)
                    {
                        (enemyPhaseAction[(int)currentBattlePhase] as I_CanUse).UseEffect();
                        yield return SC_GameMgr._gameMgr.waitText;
                    }
                }
                else
                {
                    if (enemyPhaseAction[(int)currentBattlePhase] != null)
                    {
                        (enemyPhaseAction[(int)currentBattlePhase] as I_CanUse).UseEffect();
                        yield return SC_GameMgr._gameMgr.waitText;
                    }
                    if (playerPhaseAction[(int)currentBattlePhase] != null)
                    {
                        (playerPhaseAction[(int)currentBattlePhase] as I_CanUse).UseEffect();
                        yield return SC_GameMgr._gameMgr.waitText;
                    }
                }
            }
        }
        BattleRoundInit();
    }
    public void ExecuteBattle() { StartCoroutine(_executeBattle()); }
}
