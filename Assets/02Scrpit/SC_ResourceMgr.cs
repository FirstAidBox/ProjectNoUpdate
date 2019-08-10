using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class SC_ResourceMgr : MonoBehaviour
//여러가지 리소스들을 불러오기 쉽게 모아놓은 스크립트입니다.
{
    public Sprite _tempSprite;

    public Sprite sp_Null;//투명한 스프라이트. SC_GameMgr에도 nullSprite란 이름으로 있다.
    public Sprite sp_Warrior;
    public Sprite sp_Mage;
    public Sprite sp_Ranger;

    public Sprite sp_bg_Tavern;
    public Sprite sp_npc_Mr;

    public Sprite sp_48_Area1;
    public Sprite sp_48_Area2;
    public Sprite sp_48_Area3;

    public Sprite sp_npc_Monster1;
    public Sprite sp_npc_Monster2;
    public Sprite sp_npc_Monster3;
    public Sprite sp_npc_Monster4;
    public Sprite sp_npc_MonsterBoss;

    public Sprite sp_sk_Attack;
    public Sprite sp_sk_Gurad;
    public Sprite sp_sk_Smash;

    /// <summary>
    /// 스프라이트를 문자열로 불러옵니다.
    /// </summary>
    /// <param name="spritename">스프라이트 이름</param>
    /// <returns></returns>
    public Sprite GetSprite(string spritename)
    {
        FieldInfo fld = typeof(SC_ResourceMgr).GetField(spritename);
        _tempSprite = (Sprite)fld.GetValue(this);
        return _tempSprite;
    }
}
