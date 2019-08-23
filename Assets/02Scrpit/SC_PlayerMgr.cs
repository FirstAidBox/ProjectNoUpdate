using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SC_PlayerMgr : MonoBehaviour
//플레이어에 관한 정보들이 모여있는 스크립트입니다.
{
    public static SC_PlayerMgr _playerMgr;

    public string Job;
    public Sprite Image;
    public int Level = 1;
    public int MaxHP;
    public int CurrentHP;
    public int ATK;
    public int DEF;
    public int SPD;
    public int Money = 0;
    public int[] HaveSkillIndex;
    public int TotalSkillNum;

    public bool IsHide;
    public bool IsDash;
    public bool IsGuard;
    public bool IsDown;

    public int SightRange;
    public int ProphecyRange;

    public GameObject SkillContent;
    public SC_SkillSlot[] SkillSlots;
    public int SkillCount;
    public GameObject ItemContent;
    public SC_ItemSlot[] ItemSlots;
    public int ItemCount;

    //옛날 데이터 형식. 스크립터블 오브잭트로 교체되었다. 완전히 필요 없어지면 제거할 것.
    public struct Jobdefalut
    {
        public string jobName;
        public Sprite jobImage;
        public int hp;
        public int atk;
        public int def;
        public int spd;
    }

    /*
    public Jobdefalut Warrior;
    public Jobdefalut Mage;
    public Jobdefalut Ranger;
    */

    void Awake()
    {
        _playerMgr = this;
        SkillSlots = SkillContent.GetComponentsInChildren<SC_SkillSlot>();
        ItemSlots = ItemContent.GetComponentsInChildren<SC_ItemSlot>();
        /*옛날 데이터. 완전히 필요 없어지면 제거할 것.
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
        */
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

    public void PlayerInfoInit()
    {
        Level = 1;
        IsHide = false;
        IsDash = false;
        IsDown = false;
        IsGuard = false;
        SightRange = 1;
        ProphecyRange = 1;
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].SlotInit();
        }
        ItemCount = 0;
        for (int i = 0; i < SkillSlots.Length; i++)
        {
            SkillSlots[i].SlotInit();
        }
        SkillCount = 0;
    }
    public SBO_SlotObject RandomSkill()//이미 플레이어가 배운 스킬을 제외하고 리턴
        //스킬데이터의 수가 스킬슬롯 수(6개)보다 적으면 무한 반복의 위험이 있다.
    {
        bool already;
        int index;
        for(; ; )
        {
            index = UnityEngine.Random.Range(0, SC_SBODataMgr._SBODataMgr.playerSkillData.Count);
            already = false;
            for (int i = 0; i < SkillCount; i++)//스킬슬롯에 있는 스킬인덱스를 대조하여 같지 않으면 리턴, 하나라도 같으면 인덱스를 다시뽑아 반복
                if (SkillSlots[i].slotObject.Index == index)
                    already = true;
            if (!already)
                return SC_SBODataMgr._SBODataMgr.playerSkillData[index];
        }
    }
    public void GetSkill(SBO_SlotObject skill)
    {
        if(SkillCount >= 6)
        {
            SC_GameMgr._gameMgr.PrintClickTextBox("이미 기술 칸이 가득찼습니다.");
            //기본기술2 + 진행에 따라 습득하는 기술3이라 테스터콘솔을 사용하는게 아니면 이 상황은 볼 수 없다.
        }
        else
        {
            SkillSlots[SkillCount].AddObject(skill);
            if(!(skill is I_Instant))
                SC_GameMgr._gameMgr.PrintClickTextBox("새 기술 "+ skill.Name + " 을(를) 배웠다.");
        }
    }
    public void GetRandomSkill()
    {
        GetSkill(RandomSkill());
    }
    public SBO_SlotObject RandomItem()//0번의 빈 아이템을 제외하고 리턴
    {
        return SC_SBODataMgr._SBODataMgr.itemData[UnityEngine.Random.Range(1, SC_SBODataMgr._SBODataMgr.itemData.Count)];
    }
    public void GetItem(SBO_SlotObject Item)
    {
        if (ItemCount >= 9)
        {
            SC_GameMgr._gameMgr.PrintClickTextBox(Item.Name+" 을(를) 찾았지만 가방이 가득찼다.");
        }
        else
        {
            int index = 0;
            bool notget = true;
            while(notget)
            {
                if(ItemSlots[index].slotObject.Index == 0)
                {
                    ItemSlots[index].AddObject(Item);
                    notget = false;
                    SC_GameMgr._gameMgr.PrintClickTextBox(Item.Name + " 을(를) 찾아서 가방에 넣었다.");
                }
                else
                {
                    index++;
                }
            }
        }
    }
    public void GetRandomItem()
    {
        GetItem(RandomItem());
    }
}
