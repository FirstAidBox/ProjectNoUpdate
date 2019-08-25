using UnityEngine;

public class SC_ActionSelButton : MonoBehaviour
{
    public int buttonIndex;
    public bool isPointerIn = false;

    public bool IsCanInteract()
    {
        if (SC_GameMgr._gameMgr.isFade
            || SC_GameMgr._gameMgr.isPopupFABar || SC_GameMgr._gameMgr.isPlayingText
            || SC_GameMgr._gameMgr.isPlayingText)
            return false;
        else
            return true;
    }

    private void OnMouseEnter()
    {
        isPointerIn = true;
    }

    private void OnMouseExit()
    {
        isPointerIn = false;
    }

    private void OnMouseDown()
    {
        if(IsCanInteract())
            SC_GameMgr._gameMgr.PrintTextBox((buttonIndex+1) + "턴의 행동을 정합니다.");
    }

    private void OnMouseUp()
    {
        if (IsCanInteract())
        {
            if (isPointerIn)
                 SC_FieldMgr._fieldMgr.MoveSelecter(buttonIndex);
            SC_GameMgr._gameMgr.PrintBaseBox();
        }
    }
}
