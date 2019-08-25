using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KIND_ENEMY { BOSS=1, ENEMY, TRAP, BOX}

public class SC_EnemyMgr : MonoBehaviour
{
    //몬스터나 함정의 능력치와 배치 등을 담당하는 스크립트입니다.
    public static SC_EnemyMgr _enemyMgr;

    public bool isArea1Clear = false;
    public bool isArea2Clear = false;
    public bool isArea3Clear = false;

    public KIND_ENEMY KIND;
    public string Name;
    public Sprite Image;
    public Color Color;
    public int MaxHP;
    public int CurrentHP;
    public int ATK;
    public int DEF;
    public int SPD;

    public int[] SkillIndex;
    public int SkillCount;

    public SC_EnemyIndi EnemyIndicator;
    public Vector2 InInnPos = new Vector2(2.5f, 0.5f);
    public Vector2 FieldStartPos = new Vector2(7.5f, 0.5f);

    public SBO_EnemyData[] enemyData;
    public int MaxEnemyCount;
    public int AddStatValue;

    void Awake()
    {
        _enemyMgr = this;
        SkillIndex = new int[3];
        enemyData = new SBO_EnemyData[10];//지역당 보스1 + 그 외3*n
        Init();
    }
    public void Init()
    {
        MaxEnemyCount = 4;
        AddStatValue = 0;
    }
    public void AreaLevelUp()
    {
        MaxEnemyCount += 3;
        AddStatValue += 1;
    }
    public void GetArea1Data()
    {
        enemyData[0] = SC_SBODataMgr._SBODataMgr.area1EnemyData[0];
        for(int i=1; i<MaxEnemyCount;i++)
        {
            enemyData[i] = SC_SBODataMgr._SBODataMgr.area1EnemyData[Random.Range(1, SC_SBODataMgr._SBODataMgr.area1EnemyData.Count)];
        }
    }
    public void InputEnemyData(int i)
    {
        KIND = enemyData[i].KIND;
        Name = enemyData[i].EnemyName;
        Image = enemyData[i].Image;
        Color = enemyData[i].Color;
        MaxHP = CurrentHP = enemyData[i].Hp + AddStatValue;
        ATK = enemyData[i].Atk + AddStatValue;
        DEF = enemyData[i].Def + AddStatValue;
        SPD = enemyData[i].Spd + AddStatValue;
        SkillCount = enemyData[i].SkillIndex.Length;
        for (int n = 0; n < SkillCount; n++)
            SkillIndex[n] = enemyData[i].SkillIndex[n];
        EnemyIndicator.IndicatorMakeup(enemyData[i].Image, enemyData[i].EnemyName);
    }
    public void VisibleEnemy()
    {
        EnemyIndicator.gameObject.SetActive(true);
    }
    public void InvisibleEnemy()
    {
        EnemyIndicator.gameObject.SetActive(false);
    }
    public void SetPosition()
    {
        EnemyIndicator.gameObject.transform.position = FieldStartPos;
    }
    public void EnemyMove()
    {
        if(KIND == KIND_ENEMY.ENEMY)
        {
            if(SC_PlayerMgr._playerMgr.IsHide)
            {
                EnemyIndicator.MoveOutWindow();
                SC_GameMgr._gameMgr.PrintClickTextBox(Name + " 이(가) 당신을 발견하지 못했습니다.");
            }
            else
            {
                SC_EffectMgr._effectMgr.isEvent = true;
            }
        }
        else if(KIND == KIND_ENEMY.TRAP)
        {
            if(SC_PlayerMgr._playerMgr.IsDash)
            {
                EnemyIndicator.IndiFadeOut();
                SC_GameMgr._gameMgr.PrintClickTextBox(Name + " 이(가) 작동되었지만 피했습니다.");
            }
            else
            {
                SC_GameMgr._gameMgr.isPlayingText = true;
                SC_GameMgr._gameMgr.PrintTextBox(Name + " 이 작동되었습니다.");
                SC_EffectMgr._effectMgr.EffectSimpleHit(SC_PlayerMgr._playerMgr.playerIndicator.gameObject.transform.position);
                EnemyIndicator.IndiFadeOut();
                SC_GameMgr._gameMgr.InvokeWaitEvent(SC_PlayerMgr._playerMgr.ApplyDamage, ATK);
            }
        }
        else if(KIND == KIND_ENEMY.BOX)
        {
            SC_GameMgr._gameMgr.isPlayingText = true;
            SC_GameMgr._gameMgr.PrintTextBox("상자를 발견해 열어봤습니다.");
            EnemyIndicator.IndiFadeOut();
            SC_GameMgr._gameMgr.InvokeWaitEvent(SC_PlayerMgr._playerMgr.GetRandomItem);
        }
    }
    
}
