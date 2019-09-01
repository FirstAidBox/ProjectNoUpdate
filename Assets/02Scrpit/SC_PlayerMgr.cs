using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerMgr : MonoBehaviour
//플레이어에 관한 정보들이 모여있는 스크립트입니다.
//여관 상점도 여기에서 처리합니다.
{
    public static SC_PlayerMgr _playerMgr;

    public string Job;
    public Sprite Image;
    public int Level;
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
    public bool IsDmg;
    public bool IsDown;
    public bool IsCounter;

    public int SightRange;
    public int ProphecyRange;

    public SC_PlayerIndi playerIndicator;
    public Vector2 currentPlayerPos;

    public GameObject SkillContent;
    public SC_SkillSlot[] SkillSlots;
    public int SkillCount;
    public GameObject ItemContent;
    public SC_ItemSlot[] ItemSlots;
    public int ItemCount;
    public GameObject InnContent;
    public SC_InnSlot[] InnSlots;

    void Awake()
    {
        _playerMgr = this;
        SkillSlots = SkillContent.GetComponentsInChildren<SC_SkillSlot>();
        ItemSlots = ItemContent.GetComponentsInChildren<SC_ItemSlot>();
        InnSlots = InnContent.GetComponentsInChildren<SC_InnSlot>();
    }

    public void GetJob(SBO_PlayerJobData jobData)
    {
        Job = jobData.jobName;
        Image = jobData.image;
        MaxHP = CurrentHP = jobData.hp;
        ATK = jobData.atk;
        DEF = jobData.def;
        SPD = jobData.spd;
        Money = 20;
        if (jobData.defaultSkills != null)
        {
            for (int i = 0; i < jobData.defaultSkills.Length; i++)
                SkillSlots[i].AddObject(jobData.defaultSkills[i]);
        }
        ItemSlots[0].AddObject(SC_SBODataMgr._SBODataMgr.itemData[1]);
        ItemSlots[1].AddObject(SC_SBODataMgr._SBODataMgr.itemData[1]);
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
    {
        bool already = true;
        int index = 0;
        while (already)
        {
            already = false;
            index = Random.Range(0, SC_SBODataMgr._SBODataMgr.playerSkillData.Count);
            for (int i = 0; i < SkillCount; i++)
            {
                if (SkillSlots[i].slotObject != null)
                {
                    if (SkillSlots[i].slotObject.Index == index)
                        already = true;
                }
            }
        }     
        return SC_SBODataMgr._SBODataMgr.playerSkillData[index];
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
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectGetSlotObject(skill);
            SC_GameMgr._gameMgr.PrintClickTextBox("새 기술 "+ skill.Name + " 을(를) 배웠습니다.");
            SC_SoundMgr._soundMgr.SFX_SkillGet();
        }
    }
    public void GetRandomSkill()
    {
        GetSkill(RandomSkill());
    }
    public SBO_SlotObject RandomItem()//0번의 빈 아이템을 제외하고 리턴
    {
        return SC_SBODataMgr._SBODataMgr.itemData[Random.Range(1, SC_SBODataMgr._SBODataMgr.itemData.Count)];
    }
    public void GetItem(SBO_SlotObject Item)
    {
        if(Item is SBO_InstantObject)
        {
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectGetSlotObject(Item);
            SC_GameMgr._gameMgr.PrintClickTextBox(Item.Name + " 을(를) 찾았습니다. "+(Item as SBO_InstantObject).EffectText);
            (Item as SBO_InstantObject).GetEffect();
            SC_SoundMgr._soundMgr.SFX_PutinBag();
        }
        else if (ItemCount >= 9)
        {
            SC_GameMgr._gameMgr.PrintClickTextBox(Item.Name+" 을(를) 찾았지만 가방이 가득찼습니다.");
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
                    SC_EffectMgr._effectMgr.isEvent = true;
                    SC_EffectMgr._effectMgr.EffectGetSlotObject(Item);
                    SC_GameMgr._gameMgr.PrintClickTextBox(Item.Name + " 을(를) 찾아서 가방에 넣었습니다.");
                    SC_SoundMgr._soundMgr.SFX_PutinBag();
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
    public void BuyItem(SBO_SlotObject Item)
    {
        if (Item is SBO_InstantObject)
        {
            SC_EffectMgr._effectMgr.isEvent = true;
            SC_EffectMgr._effectMgr.EffectGetSlotObject(Item);
            Money -= Item.Price;
            SC_MenuBar._menuBar.RefreshMoneyIndi();
            SC_GameMgr._gameMgr.PrintClickTextBox(Item.Name + " 을(를) 구매했습니다. " + (Item as SBO_InstantObject).EffectText);
            (Item as SBO_InstantObject).GetEffect();
        }
        else
        {
            int index = 0;
            bool notget = true;
            while (notget)
            {
                if (ItemSlots[index].slotObject.Index == 0)
                {
                    ItemSlots[index].AddObject(Item);
                    notget = false;
                    SC_EffectMgr._effectMgr.isEvent = true;
                    SC_EffectMgr._effectMgr.EffectGetSlotObject(Item);
                    Money -= Item.Price;
                    SC_MenuBar._menuBar.RefreshMoneyIndi();
                    SC_GameMgr._gameMgr.PrintClickTextBox("구매한 "+Item.Name + " 을(를) 가방에 넣었습니다.");
                }
                else
                {
                    index++;
                }
            }
        }
    }
    public void SellItem(SBO_SlotObject Item)
    {
        SC_EffectMgr._effectMgr.isEvent = true;
        SC_EffectMgr._effectMgr.EffectGetCoin();
        Money += Item.Price;
        SC_MenuBar._menuBar.RefreshMoneyIndi();
        SC_GameMgr._gameMgr.PrintClickTextBox(Item.Name + " 을(를) 팔고 돈을 " + Item.Price + " 만큼 얻었습니다.");
    }
    public void Refill_InnItem()
    {
        for(int i=0; i<InnSlots.Length;i++)
        {
            InnSlots[i].AddObject(RandomItem());
        }
    }
    public void LevelUp()
    {
        SC_GameMgr._gameMgr.PrintClickTextBox("레벨이 올랐습니다. 모든 능력치가 1 상승했습니다.");
        Level++;
        MaxHP++;
        CurrentHP++;
        ATK++;
        DEF++;
        SPD++;
    }
    public void CharaMake()
    {
        playerIndicator.IndicatorMakeup(Image, "캐릭터의 정보를 확인합니다.", SC_MenuBar._menuBar.PopupStatMenu);
        playerIndicator.gameObject.transform.position = currentPlayerPos;
        VisiblePlayer();
    }
    public void VisiblePlayer()
    {
        playerIndicator.gameObject.SetActive(true);
    }
    public void InvisiblePlayer()
    {
        playerIndicator.gameObject.SetActive(false);
    }
    public void TurnInit()
    {
        IsHide = false;
        IsDash = false;
        IsGuard = false;
        IsDown = false;
        IsCounter = false;
        IsDmg = false;
    }
    /// <summary>
    /// 플레이어에게 방어력에 의해 감소되는 데미지를 입힌다.
    /// </summary>
    /// <param name="DmgValue">데미지 값</param>
    public void ApplyDamage(int DmgValue)
    {
        int finDmg = Mathf.Clamp(DmgValue - DEF, 0, DmgValue);
        if (finDmg > 0)
        {
            SC_EffectMgr._effectMgr.CameraShake(finDmg);
            SC_SoundMgr._soundMgr.SFX_Dmg();
        }
        else
            SC_SoundMgr._soundMgr.SFX_NoDmg();
        playerIndicator.IndiBlink();
        CurrentHP -= finDmg;
        if (CurrentHP <= 0)
            PlayerDie();
        SC_GameMgr._gameMgr.PrintClickTextBox(finDmg + " 피해를 입었습니다.");
    }
    /// <summary>
    /// 플레이어에게 방어력을 무시하고 데미지를 입힌다.
    /// </summary>
    /// <param name="DmgValue">데미지 값</param>
    public void ApplyDamagePure(int DmgValue)
    {
        if (DmgValue > 0)
        {
            SC_EffectMgr._effectMgr.CameraShake(DmgValue);
            SC_SoundMgr._soundMgr.SFX_Dmg();
        }
        else
            SC_SoundMgr._soundMgr.SFX_NoDmg();
        playerIndicator.IndiBlink();
        CurrentHP -= DmgValue;
        if (CurrentHP <= 0)
            PlayerDie();
        SC_GameMgr._gameMgr.PrintClickTextBox(DmgValue + " 피해를 입었습니다.");
    }
    public void PlayerDie()
    {
        SC_FieldMgr._fieldMgr.isInBattle = false;
        SC_FieldMgr._fieldMgr.StopAllCoroutines();
        SC_FieldMgr._fieldMgr.BattleEndInit();
        StartCoroutine(_PlayerDie());
    }
    private IEnumerator _PlayerDie()
    {
        yield return SC_GameMgr._gameMgr.waitText;
        SC_GameMgr._gameMgr.PrintTextBox("의식이 희미해진다...");
        SC_GameMgr._gameMgr.FadeOutAndIn();
        yield return SC_GameMgr._gameMgr.waitFadeOut;
        SC_FieldMgr._fieldMgr.ExitField();
        SC_GameMgr._gameMgr.PlayerDie();
    }
}
