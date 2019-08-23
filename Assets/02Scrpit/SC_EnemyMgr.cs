using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum KIND_ENEMY { BOSS=1, ENEMY, TRAP, BOX}

public class SC_EnemyMgr : MonoBehaviour
{
    //몬스터나 함정의 능력치와 배치 등을 담당하는 스크립트입니다.

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

    public SBO_EnemyData[] enemyData;
    public int MaxEnemyCount;

    void Awake()
    {
        SkillIndex = new int[3];
        enemyData = new SBO_EnemyData[10];//지역당 보스1 + 그 외3*n
    }

    public void GetArea1Data()
    {

    }
}
