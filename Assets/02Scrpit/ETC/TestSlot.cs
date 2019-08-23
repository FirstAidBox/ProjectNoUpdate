using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSlot : MonoBehaviour
{
    private SBO_SlotObject item;
    public Image icon;
    public Text text;

    public void AddItem(SBO_SlotObject GetItem)
    {
        item = GetItem;
        icon.sprite = GetItem.Image;
        icon.color = GetItem.Color;
        text.text = GetItem.Name;
    }
    public void RemoveItem()
    {
        item = null;
    }
    private void Start()
    {
        AddItem(SC_SBODataMgr._SBODataMgr.itemData[1]);
    }
    public void UseItem()
    {
        if(item is I_CanUse)
        {
            (item as I_CanUse).UseEffect();
        }
    }
    public void ClickItem()
    {
        Debug.Log("버튼누름");
    }
}
