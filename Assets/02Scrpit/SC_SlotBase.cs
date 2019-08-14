using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SC_SlotBase : MonoBehaviour
{
    public bool isStackInAction;
    public SBO_SlotObject slotObject;
    public Image icon;
    public Text text;
    public Image mask;

    public virtual void AddObject(SBO_SlotObject GetObject)
    {
        slotObject = GetObject;
        icon.sprite = GetObject.Image;
        icon.color = GetObject.Color;
        text.text = GetObject.Name;
    }
    public virtual void RemoveObject()
    {
    }
    public virtual void OutByActionBar()
    {
        isStackInAction = false;
        mask.color = new Color(0, 0, 0, 0);
    }
    public virtual void UseObject()
    {

    }
    public virtual void PointerDown()
    {
        SC_GameMgr._gameMgr.PrintTextBox(slotObject.Image, slotObject.Text, slotObject.Color);
    }
    public virtual void PointerUp()
    {
        SC_GameMgr._gameMgr.PrintBaseBox();
    }
    public virtual void ButtonClick()
    {
    }
}
