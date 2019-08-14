using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ItemMgr : MonoBehaviour
    //아이템들의 정보와 효과 그리고 인벤토리를 구현하는 스크립트입니다.
    //상점의 구매, 판매도 이쪽에서 담당합니다.
{
    public static SC_ItemMgr _itemMgr;
    public List<SBO_SlotObject> ItemList;

    void Awake()
    {
        _itemMgr = this;

        ItemList.AddRange(Resources.LoadAll<SBO_SlotObject>("itemdata"));
        ItemList.Sort(delegate (SBO_SlotObject A, SBO_SlotObject B)
        {
            if (A.Index > B.Index) return 1;
            else if (A.Index < B.Index) return -1;
            else if (A.Index == B.Index)
            {
                if (A.name != B.name)
                    Debug.Log(string.Format("{0} 와 {1} 의 인덱스가 같습니다.", A.name, B.name));
                return 0;
            }
            return 0;
        });
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].Index != i)
                Debug.Log(string.Format(
                    "{0} 의 인덱스와 배열번호가 일치하지 않습니다. {0} 의 인덱스: {1}, 배열번호: {2}", ItemList[i].name, ItemList[i].Index, i));
        }
    }
}
