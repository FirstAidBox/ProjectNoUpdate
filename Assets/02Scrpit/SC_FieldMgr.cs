﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KIND_ACTION {  }

public class SC_FieldMgr : MonoBehaviour
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

    public struct ActionData
    {
        public KIND_ACTION _ACTION;
        public int index;
    }

    public GameObject selecter;
    public int actionIndex;

    public ActionData[] playerFieldAction;
    public ActionData[] enemyFieldAction;
    public ActionData[] playerBattleAction;
    public ActionData[] enemyBattleAction;

    private void Awake()
    {
        _fieldMgr = this;
        fieldBacks = fieldBackGroup.GetComponentsInChildren<SC_FieldBack>();
        DisableFieldTiles();
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
        SC_GameMgr._gameMgr.StartCoroutine("PrintClickTextBox", "지역1에 도착했습니다.");
        SC_GameMgr._gameMgr.StartCoroutine("PrintClickTextBox", "이동합니다.");
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
        actionIndex = input;
        selecter.transform.localPosition = new Vector2(input, 0);
    }
    IEnumerator MoveTiles(int moveRange)
    {
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
        yield return null;
    }
}