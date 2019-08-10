using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerIndi : SC_Indicator
{
    private bool IsCanInteract()
    {
        if (indicatorText == null || SC_GameMgr._gameMgr.isFade || SC_GameMgr._gameMgr.isPopupFABar || SC_GameMgr._gameMgr.isPopupPlayerBar)
            return false;
        else
            return true;
    }
    private void OnMouseEnter()
    {
        if (IsCanInteract())
            SC_GameMgr._gameMgr.PrintTextBox(indicatorRenderer.sprite, indicatorText, indicatorRenderer.color);
    }

    private void OnMouseExit()
    {
        if (IsCanInteract())
            SC_GameMgr._gameMgr.PrintBaseBox();
    }

    private void OnMouseDown()
    {
        if (IsCanInteract())
            SC_GameMgr._gameMgr.EventExecute(indicatorEventName, _FLAG);
    }
}
