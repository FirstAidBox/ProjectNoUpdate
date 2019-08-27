using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_InnSlot : MonoBehaviour
{
    public SBO_SlotObject slotObject;
    public Image icon;
    public Text text;

    public void AddObject(SBO_SlotObject GetObject)
    {
        slotObject = GetObject;
        icon.sprite = GetObject.Image;
        icon.color = GetObject.Color;
        text.text = GetObject.Name;
    }
    public void RemoveObject()
    {
        AddObject(SC_SBODataMgr._SBODataMgr.itemData[0]);
    }
    public void PointerDown()
    {
        if (slotObject.Index == 0)
            SC_GameMgr._gameMgr.PrintTextBox("이미 구매한 물건입니다.");
        else
            SC_GameMgr._gameMgr.PrintTextBox(slotObject.Image,slotObject.Name + " 가격: " + slotObject.Price + " " + slotObject.Text , slotObject.Color);
    }
    public void PointerUp()
    {
        SC_GameMgr._gameMgr.PrintBaseBox();
    }
    public void ButtonClick()
    {
        if (slotObject.Index == 0)
            SC_GameMgr._gameMgr.PrintTextBox("이미 구매한 물건입니다.");
        else if (SC_PlayerMgr._playerMgr.ItemCount >= 9)
            SC_GameMgr._gameMgr.PrintTextBox("가방이 가득차 구매할 수 없습니다.");
        else if (slotObject.Price > SC_PlayerMgr._playerMgr.Money)
            SC_GameMgr._gameMgr.PrintTextBox("돈이 부족해 구매할 수 없습니다.");
        else
        {
            SC_PlayerMgr._playerMgr.BuyItem(slotObject);
            RemoveObject();
        }
    }
}
