using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SC_Indicator : MonoBehaviour
{
    //계속 재활용되는 주요 게임 오브잭트인 Indicator들의 스크립트입니다.
    public string indicatorEventName; //인디케이터 클릭시 실행되는 이벤트의 이름
    public string indicatorText;//인디케이터에 마우스 오버시 나타나는 텍스트
    public EVENT_FLAG _FLAG;
    public SpriteRenderer indicatorRenderer;
    public BoxCollider2D indicatorCollider;
    public bool isPointerIn = false;

    private void Awake()
    {
        indicatorRenderer = GetComponent<SpriteRenderer>();
        indicatorCollider = GetComponent<BoxCollider2D>();
        indicatorEventName = null;
        indicatorText = null;
        _FLAG = EVENT_FLAG.EVENT;
    }
    public void IndicatorMakeup(Sprite inputSprite)
    {
        IndicatorInit();
        indicatorRenderer.sprite = inputSprite;
    }
    public void IndicatorMakeup(Sprite inputSprite, string inputText)
    {
        IndicatorInit();
        indicatorRenderer.sprite = inputSprite;
        indicatorText = inputText;
    }
    public void IndicatorMakeup(Sprite inputSprite,string inputText, string eventName, EVENT_FLAG inputFLAG)
    {
        indicatorRenderer.sprite = inputSprite;
        indicatorText = inputText;
        indicatorEventName = eventName;
        _FLAG = inputFLAG;
    }
    public void ResizeCollider(float inputsize)
    {
        Vector2 size = new Vector2(inputsize, inputsize);
        indicatorCollider.size = size;
        indicatorCollider.offset = size * 0.5f;
    }
    /// <summary>
    /// 인디케이터 초기화
    /// </summary>
    public void IndicatorInit()
    {
        indicatorRenderer.sprite = SC_GameMgr._gameMgr.nullSprite;
        indicatorEventName = null;
        indicatorText = null;
        _FLAG = EVENT_FLAG.EVENT;
        ResizeCollider(1f);
        indicatorRenderer.color = SC_GameMgr._gameMgr.baseColor;
    }
    private bool IsCanInteract()
    {
        if (indicatorText == null || SC_GameMgr._gameMgr.isFade || SC_GameMgr._gameMgr.isPopupFABar || SC_GameMgr._gameMgr.isPlayingText)
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
        if (IsCanInteract())
        {
            SC_GameMgr._gameMgr.PrintTextBox(indicatorRenderer.sprite, indicatorText, indicatorRenderer.color);
        }
    }
    private void OnMouseUp()
    {
        if (IsCanInteract() && indicatorEventName != null && isPointerIn)
            SC_GameMgr._gameMgr.EventExecute(indicatorEventName, _FLAG);
        SC_GameMgr._gameMgr.PrintBaseBox();
    }
}
