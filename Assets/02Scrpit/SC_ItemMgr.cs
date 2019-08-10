using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ItemMgr : MonoBehaviour
    //아이템들의 정보와 효과 그리고 인벤토리를 구현하는 스크립트입니다.
{
    public SC_ResourceMgr _resourceMgr;

    void Awake()
    {
        _resourceMgr = GetComponent<SC_ResourceMgr>();
    }
}
