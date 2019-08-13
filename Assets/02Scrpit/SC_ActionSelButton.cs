using UnityEngine;

public class SC_ActionSelButton : MonoBehaviour
{
    public int buttonIndex;
    public bool isPointerIn = false;

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
        SC_GameMgr._gameMgr.PrintTextBox((buttonIndex+1) + "턴의 행동을 정합니다.");
    }

    private void OnMouseUp()
    {
        if (isPointerIn)
        {
            if (SC_FieldMgr._fieldMgr.actionIndex != buttonIndex)
            SC_FieldMgr._fieldMgr.MoveSelecter(buttonIndex);
        }
        SC_GameMgr._gameMgr.PrintBaseBox();
    }
}
