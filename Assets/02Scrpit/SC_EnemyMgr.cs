using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum KIND_ENEMY { BOSS=1, ENEMY, TRAP, BOX}

public class SC_EnemyMgr : MonoBehaviour
    //몬스터나 함정의 능력치와 배치 등을 담당하는 스크립트입니다.
{
    public SC_ResourceMgr _resourceMgr;

    public bool isArea1Clear = false;
    public bool isArea2Clear = false;
    public bool isArea3Clear = false;

    public List<SBO_EnemyData> enemyData;
    public List<TestSkill> testSkills;

    void Awake()
    {
        _resourceMgr = GetComponent<SC_ResourceMgr>();
    }

    public void GetArea1Data()
    {

    }
}
