using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemyIndi : SC_Indicator
{
    private Vector2 backMove = new Vector2(-0.5f, 0f);
    public bool isInnMaster = false;

    public override bool IsCanInteract()
    {
        if (indicatorText == null || SC_GameMgr._gameMgr.isFade
            || SC_GameMgr._gameMgr.isPopupFABar
            || SC_GameMgr._gameMgr.isPlayingText)
            return false;
        else
            return true;
    }
    private IEnumerator _moveOutWindow()
    {
        SC_GameMgr._gameMgr.isEventPlaying = true;
        SC_SoundMgr._soundMgr.SFX_FootStepStart();
        for(int i=0; i<20; i++)
        {
            gameObject.transform.position = (Vector2)gameObject.transform.position + backMove;
            yield return SC_GameMgr._gameMgr.delay50ms;
        }
        SC_GameMgr._gameMgr.isEventPlaying = false;
        SC_SoundMgr._soundMgr.SFX_FootStepStop();
    }
    public void MoveOutWindow()
    {
        StartCoroutine(_moveOutWindow());
    }
}
