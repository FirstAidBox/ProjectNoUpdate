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
}
