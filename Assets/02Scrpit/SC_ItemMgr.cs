using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ItemMgr : MonoBehaviour
    //아이템들의 정보와 효과 그리고 인벤토리를 구현하는 스크립트입니다.
{
    public static SC_ItemMgr _itemMgr;
    public List<SBO_ItemBase> testItem;
    public SC_ResourceMgr _resourceMgr;

    void Awake()
    {
        _itemMgr = this;
        _resourceMgr = GetComponent<SC_ResourceMgr>();

        testItem.AddRange(Resources.LoadAll<SBO_ItemBase>("itemdata"));
    }
}
