using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SC_PlayerMgr : MonoBehaviour
//플레이어의 상태의 정보와 관련된 행동들을 처리하는 스크립트입니다.
{
    public SC_ResourceMgr _resourceMgr;
    
    public string Job;
    public Sprite Image;
    public int Level = 1;
    public int MaxHP;
    public int CurrentHP;
    public int ATK;
    public int DEF;
    public int SPD;
    public int Money = 0;
    public int[] SkillIndex;
    public int TotalSkillNum;

    public struct Jobdefalut
    {
        public string jobName;
        public Sprite jobImage;
        public int hp;
        public int atk;
        public int def;
        public int spd;
    }

    public Jobdefalut Warrior;
    public Jobdefalut Mage;
    public Jobdefalut Ranger;

    void Awake()
    {
        _resourceMgr = GetComponent<SC_ResourceMgr>();
        //기본스텟 5, 2, 1, 1
        Warrior.jobName = "전사";
        Warrior.jobImage = _resourceMgr.sp_Warrior;
        Warrior.hp = 6;
        Warrior.atk = 3;
        Warrior.def = 2;
        Warrior.spd = 1;

        Mage.jobName = "마법사";
        Mage.jobImage = Resources.Load<Sprite>("sprite/mage");//_resourceMgr.GetSprite("sp_Mage");
        Mage.hp = 4;
        Mage.atk = 5;
        Mage.def = 1;
        Mage.spd = 2;

        Ranger.jobName = "순찰자";
        Ranger.jobImage = _resourceMgr.sp_Ranger;
        Ranger.hp = 5;
        Ranger.atk = 2;
        Ranger.def = 1;
        Ranger.spd = 4;

    }

    [Obsolete("구버전 함수. 스크립터블 오브잭트를 사용하는 함수로 바꿀 것.")]
    public void GetJob(Jobdefalut jobData)
    {
        Job = jobData.jobName;
        Image = jobData.jobImage;
        MaxHP = CurrentHP = jobData.hp;
        ATK = jobData.atk;
        DEF = jobData.def;
        SPD = jobData.spd;
    }

    public void GetJob(SBO_PlayerJobData jobData)
    {
        Job = jobData.jobName;
        Image = jobData.image;
        MaxHP = CurrentHP = jobData.hp;
        ATK = jobData.atk;
        DEF = jobData.def;
        SPD = jobData.spd;
    }
}
