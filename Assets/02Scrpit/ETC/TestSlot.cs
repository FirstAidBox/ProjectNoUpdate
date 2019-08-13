using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSlot : MonoBehaviour
{
    public SBO_ItemBase item;
    public Image icon;
    public Text text;

    public void AddItem(SBO_ItemBase GetItem)
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
        AddItem(SC_ItemMgr._itemMgr.testItem[0]);
    }
    public void UseItem()
    {
        if(item is I_CanUse)
        {
            (item as I_CanUse).UseEffect();
        }
    }
}
