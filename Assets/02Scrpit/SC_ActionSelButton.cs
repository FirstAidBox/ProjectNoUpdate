using UnityEngine;

public class SC_ActionSelButton : MonoBehaviour
{
    public int buttonIndex;

    private void OnMouseDown()
    {
        if (SC_FieldMgr._fieldMgr.actionIndex == buttonIndex)
            return;
        SC_FieldMgr._fieldMgr.MoveSelecter(buttonIndex);
    }
}
