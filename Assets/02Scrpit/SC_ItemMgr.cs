using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ItemMgr : MonoBehaviour
    //아이템들의 정보와 효과 그리고 인벤토리를 구현하는 스크립트입니다.
    //상점의 구매, 판매도 이쪽에서 담당합니다.
    //아이템 자체의 능력치는 SBO메니저행
    //PlayerMgr에 병합예정
{
    public static SC_ItemMgr _itemMgr;

    void Awake()
    {
        _itemMgr = this;
    }
}
