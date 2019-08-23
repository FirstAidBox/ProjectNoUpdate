using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SC_Indicator : MonoBehaviour
{
    //계속 재활용되는 주요 게임 오브잭트인 Indicator들의 스크립트입니다.
    public Action indicatorEvent;
    public string indicatorText;//인디케이터에 마우스 오버시 나타나는 텍스트
    public SpriteRenderer indicatorRenderer;
    public BoxCollider2D indicatorCollider;
    public bool isPointerIn = false;

    private void Awake()
    {
        indicatorRenderer = GetComponent<SpriteRenderer>();
        indicatorCollider = GetComponent<BoxCollider2D>();
        indicatorEvent = null;
        indicatorText = null;
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
    public void IndicatorMakeup(Sprite inputSprite,string inputText, Action eventName)
    {
        IndicatorInit();
        indicatorRenderer.sprite = inputSprite;
        indicatorText = inputText;
        indicatorEvent = eventName;
    }
    public void IndicatorMakeup(Sprite inputSprite, Color inputColor, string inputText, Action eventName)
    {
        IndicatorInit();
        indicatorRenderer.sprite = inputSprite;
        indicatorRenderer.color = inputColor;
        indicatorText = inputText;
        indicatorEvent = eventName;
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
        indicatorEvent = null;
        indicatorText = null;
        ResizeCollider(1f);
        indicatorRenderer.color = SC_GameMgr._gameMgr.baseColor;
    }
    public virtual bool IsCanInteract()
    {
        if (indicatorText == null || SC_GameMgr._gameMgr.isFade 
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
        if (IsCanInteract())
            SC_GameMgr._gameMgr.PrintTextBox(indicatorRenderer.sprite, indicatorText, indicatorRenderer.color);
    }
    private void OnMouseUp()
    {
        if (IsCanInteract() && indicatorEvent != null && isPointerIn)
            indicatorEvent();
        else if (IsCanInteract())
            SC_GameMgr._gameMgr.PrintBaseBox();
    }
}
