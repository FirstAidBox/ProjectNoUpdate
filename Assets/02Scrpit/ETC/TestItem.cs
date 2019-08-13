using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestItemData", menuName = "MakeTestItem")]
public class TestItem : SBO_ItemBase, I_CanUse
{
    public void UseEffect()
    {
        Debug.Log("사용됨");
    }
}
