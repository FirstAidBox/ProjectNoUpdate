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
    public bool isbiff = false;

    protected virtual void Awake()
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
        isbiff = false;
    }
    public void IndicatorMakeup(Sprite inputSprite, Color inputColor)
    {
        IndicatorInit();
        indicatorRenderer.sprite = inputSprite;
        indicatorRenderer.color = inputColor;
        isbiff = false;
    }
    public void IndicatorMakeup(Sprite inputSprite, string inputText)
    {
        IndicatorInit();
        indicatorRenderer.sprite = inputSprite;
        indicatorText = inputText;
        isbiff = false;
    }
    public void IndicatorMakeup(Sprite inputSprite, string inputText, Color inputColor)
    {
        IndicatorInit();
        indicatorRenderer.sprite = inputSprite;
        indicatorText = inputText;
        indicatorRenderer.color = inputColor;
        isbiff = false;
    }
    public void IndicatorMakeup(Sprite inputSprite,string inputText, Action eventName)
    {
        IndicatorInit();
        indicatorRenderer.sprite = inputSprite;
        indicatorText = inputText;
        indicatorEvent = eventName;
        isbiff = false;
    }
    public void IndicatorMakeup(Sprite inputSprite, Color inputColor, string inputText, Action eventName)
    {
        IndicatorInit();
        indicatorRenderer.sprite = inputSprite;
        indicatorRenderer.color = inputColor;
        indicatorText = inputText;
        indicatorEvent = eventName;
        isbiff = false;
    }
    public void ResizeCollider(float inputsize)
    {
        Vector2 size = new Vector2(inputsize, inputsize);
        indicatorCollider.size = size;
    }
    public void ColliderOff()
    {
        indicatorCollider.enabled = false;
    }
    public void ColliderOn()
    {
        indicatorCollider.enabled = true;
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
        if(IsCanInteract() && isPointerIn && isbiff)
            SC_SoundMgr._soundMgr.SFX_ClickBiff();
        else if (IsCanInteract() && indicatorEvent != null && isPointerIn)
        {
            indicatorEvent();
            SC_SoundMgr._soundMgr.SFX_ClickOK();
        }
        else if (IsCanInteract())
            SC_GameMgr._gameMgr.PrintBaseBox();
    }
    private IEnumerator _IndiFadeOut()
    {
        Color o;
        Color c;
        o = c = indicatorRenderer.color;
        SC_GameMgr._gameMgr.isEventPlaying = true;
        for (float f = 0f; f < 1.1f; f += 0.2f)
        {
            c.a = Mathf.Lerp(o.a, 0f, f);
            indicatorRenderer.color = c;
            yield return SC_GameMgr._gameMgr.delay100ms;
        }
        SC_GameMgr._gameMgr.isEventPlaying = false;
    }
    public void IndiFadeOut()
    {
        StartCoroutine(_IndiFadeOut());
    }
    private IEnumerator _IndiBlink()
    {
        Color o = indicatorRenderer.color;
        Color c = new Color(0, 0, 0, 0);
        SC_GameMgr._gameMgr.isEventPlaying = true;
        for (int i = 0; i < 5; i++)
        {
            indicatorRenderer.color = c;
            yield return SC_GameMgr._gameMgr.delay100ms;
            indicatorRenderer.color = o;
            yield return SC_GameMgr._gameMgr.delay100ms;
        }
        SC_GameMgr._gameMgr.isEventPlaying = false;
    }
    public void IndiBlink() { StartCoroutine(_IndiBlink()); }
}
