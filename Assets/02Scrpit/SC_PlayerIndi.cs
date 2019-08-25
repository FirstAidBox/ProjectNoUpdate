using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerIndi : SC_Indicator
{
    public override bool IsCanInteract()
    {
        if (indicatorText == null || SC_GameMgr._gameMgr.isFade 
            || SC_GameMgr._gameMgr.isPopupFABar || SC_MenuBar._menuBar.isPopupPlayerBar
            || SC_GameMgr._gameMgr.isPlayingText)
            return false;
        else
            return true;
    }
}
