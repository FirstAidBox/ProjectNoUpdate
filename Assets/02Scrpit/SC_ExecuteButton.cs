using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ExecuteButton : MonoBehaviour
{
    public Sprite UnableImage;
    public Sprite EnableImage;

    public SpriteRenderer ButtonRenderer;

    public bool isPointerIn = false;
    public bool isCanExecute = false;

    public bool IsCanInteract()
    {
        if (SC_GameMgr._gameMgr.isFade
            || SC_GameMgr._gameMgr.isPopupFABar || SC_GameMgr._gameMgr.isPlayingText
            || SC_GameMgr._gameMgr.isEventPlaying)
            return false;
        else
            return true;
    }

    public void UnableButton()
    {
        ButtonRenderer.sprite = UnableImage;
        isCanExecute = false;
    }
    public void EnableButton()
    {
        ButtonRenderer.sprite = EnableImage;
        isCanExecute = true;
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
        if (IsCanInteract())
        {
            if (SC_GameMgr._gameMgr.isRest)
                SC_GameMgr._gameMgr.PrintTextBox("우두머리와의 전투를 시작합니다.");
            else
                SC_GameMgr._gameMgr.PrintTextBox("턴 행동을 실행합니다.");
        }
    }
    private void OnMouseUp()
    {
        if (IsCanInteract())
        {
            if (isPointerIn)
            {
                if (isCanExecute)
                {
                    if (SC_FieldMgr._fieldMgr.isInBattle)
                    {
                        gameObject.SetActive(false);
                        SC_FieldMgr._fieldMgr.ExecuteBattle();
                    }
                    else if(SC_GameMgr._gameMgr.isRest)
                    {
                        SC_FieldMgr._fieldMgr.AnswerBossBattle();
                    }
                    else
                    {
                        gameObject.SetActive(false);
                        SC_FieldMgr._fieldMgr.ExecuteField();
                    }
                }
                else
                    SC_GameMgr._gameMgr.PrintTextBox("아직 턴 행동을 전부 정하지 않았습니다.");
            }
            else
                SC_GameMgr._gameMgr.PrintBaseBox();
        }
    }
}
