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

    public Vector2 frontPoint = new Vector2(16f, 0f);
    public Vector2 backMove = new Vector2(-0.1f, 0f);
    public int moveCount = 0;

    public GameObject actionBar;
    public SpriteRenderer[] actionIcons;
    public bool isPopupFieldMenu = false;
    public GameObject selecter;
    public SC_ExecuteButton executeButton;
    public int actionIndex;
    public int actionCounter = 0;

    public bool isPlaying;

    public SC_SlotBase[] playerFieldActionSlot;
    public int FieldActionRemember;

    public bool isInBattle = false;
    public SC_SlotBase[] playerBattleActionSlot;
    public SC_SlotBase[] enemyBattleActionSlot;
    public BATTLEPHASE currentBattlePhase;
    public SC_SlotBase[] playerBattlePhase;
    public SC_SlotBase[] enemyBattlePhase;

    private void Awake()
    {
        _fieldMgr = this;
        fieldBacks = fieldBackGroup.GetComponentsInChildren<SC_FieldBack>();
        DisableFieldTiles();
        playerFieldActionSlot = new SC_SlotBase[3];
        playerBattleActionSlot = new SC_SlotBase[3];
        enemyBattleActionSlot = new SC_SlotBase[3];
        playerBattlePhase = new SC_SlotBase[5];
        enemyBattlePhase = new SC_SlotBase[5];
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

    public void Test()
    {
        EnableFieldTiles();
        FieldTilesInit();
        StartCoroutine("MoveTiles", 2);
    }
    public void Execute()
    {
        StartCoroutine(_execute());
    }
    IEnumerator _execute()
    {
        SC_MenuBar._menuBar.ClosePlayerMenu();
        for (int i = 0; i < 3; i++)
        {
            MoveSelecterOnly(i);
            StartCoroutine("MoveTiles", 1);
            SC_GameMgr._gameMgr.PrintClickTextBox("이동합니다");
            yield return SC_GameMgr._gameMgr.waitText;
            playerFieldActionSlot[i].UseObject();
            yield return SC_GameMgr._gameMgr.waitText;
        }
        RoundInit();
    }
    public void EnteringField()
    {
        EnableFieldTiles();
        FieldTilesInit();
        actionBar.SetActive(true);
        SC_GameMgr._gameMgr.SetBaseText("3턴간 할 행동을 선택해주세요.");
        SC_GameMgr._gameMgr.PrintClickTextBox("도착했습니다.");
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
    public void MoveSelecter(int input)
    {
        if(!SC_GameMgr._gameMgr.isPopupPlayerBar)
        {
            
        }
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
        playerFieldActionSlot[actionIndex] = inputSlot;
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
    public void RoundInit()
    {
        IconReset();
        actionCounter = 0;
        executeButton.UnableButton();
        executeButton.gameObject.SetActive(true);
    }
    IEnumerator MoveTiles(int moveRange)
    {
        SC_GameMgr._gameMgr.isEventPlaying = true;
        for (int i = 0; i < moveRange*10; i++)
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
                yield return SC_GameMgr._gameMgr.delay100ms;
            }
        }
        moveCount += moveRange;
        SC_GameMgr._gameMgr.isEventPlaying = false;
        yield return null;
    }
}
