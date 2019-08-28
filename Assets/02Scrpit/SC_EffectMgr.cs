using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EffectMgr : MonoBehaviour
{
    public static SC_EffectMgr _effectMgr;

    public Transform indiTr;
    public Transform camTr;
    public Vector3 camOriPos;
    public SpriteRenderer spriteRen;
    public Vector2 playerPos;
    public Vector2 enemyPos;
    public bool isEvent = false;

    private void Awake()
    {
        _effectMgr = this;
        camTr = Camera.main.transform;
        camOriPos = Camera.main.transform.position;
    }

    private void EventSwitchOn()
    {
        if (isEvent)
            SC_GameMgr._gameMgr.isEventPlaying = true;
    }
    private void EventSwitchOff()
    {
        if (isEvent)
            SC_GameMgr._gameMgr.isEventPlaying = false;
    }

    public void Clear()
    {
        spriteRen.sprite = SC_GameMgr._gameMgr.nullSprite;
        spriteRen.color = SC_GameMgr._gameMgr.baseColor;
        spriteRen.flipX = false;
        spriteRen.flipY = false;
    }
    private IEnumerator _Shake(int n)
    {
        float timer = 0;
        while (timer <= 0.6f)
        {
            Vector3 randV = Random.insideUnitCircle * (n * 0.1f);
            camTr.position = camOriPos + randV;
            timer += Time.deltaTime;
            yield return null;
        }
        camTr.position = camOriPos;
    }
    public void CameraShake(int shakePower) { StartCoroutine(_Shake(shakePower)); }

    private IEnumerator _EffectGetSlotObject(Sprite image, Color color)
    {
        EventSwitchOn();
        Vector2 originPos = playerPos;
        Vector2 targetpos = playerPos + new Vector2(0f, 1f);
        Color c;
        indiTr.position = originPos;
        spriteRen.sprite = image;
        spriteRen.color = c = color;
        for (float f = 0f; f < 1.1f; f += 0.2f)
        {
            indiTr.position = Vector2.Lerp(originPos, targetpos, f);
            c.a = Mathf.Lerp(color.a, 0f, f);
            spriteRen.color = c;
            yield return SC_GameMgr._gameMgr.delay100ms;
        }
        Clear();
        EventSwitchOff();
        isEvent = false;
    }
    public void EffectGetSlotObject(SBO_SlotObject slotObject)
    {
        StartCoroutine(_EffectGetSlotObject(slotObject.Image, slotObject.Color));
    }
    private IEnumerator _EffectGetSlotObject(Sprite image, Color color, Vector2 pos)
    {
        EventSwitchOn();
        Vector2 originPos = pos;
        Vector2 targetpos = pos + new Vector2(0f, 1f);
        Color c;
        indiTr.position = originPos;
        spriteRen.sprite = image;
        spriteRen.color = c = color;
        for (float f = 0f; f < 1.1f; f += 0.2f)
        {
            indiTr.position = Vector2.Lerp(originPos, targetpos, f);
            c.a = Mathf.Lerp(color.a, 0f, f);
            spriteRen.color = c;
            yield return SC_GameMgr._gameMgr.delay100ms;
        }
        Clear();
        EventSwitchOff();
        isEvent = false;
    }
    public void EffectGetSlotObject(SBO_SlotObject slotObject, Vector2 pos)
    {
        StartCoroutine(_EffectGetSlotObject(slotObject.Image, slotObject.Color, pos));
    }

    public Sprite e_Coin;
    private IEnumerator _EffectGetCoin()
    {
        EventSwitchOn();
        Vector2 originPos = playerPos;
        Vector2 targetpos = originPos + new Vector2(0f, 1f);
        Color c = Color.white;
        indiTr.position = originPos;
        spriteRen.sprite = e_Coin;
        for (float f = 0f; f < 1.1f; f += 0.2f)
        {
            indiTr.position = Vector2.Lerp(originPos, targetpos, f);
            c.a = Mathf.Lerp(Color.white.a, 0f, f);
            spriteRen.color = c;
            yield return SC_GameMgr._gameMgr.delay100ms;
        }
        Clear();
        EventSwitchOff();
        isEvent = false;
    }
    public void EffectGetCoin()
    {
        StartCoroutine(_EffectGetCoin());
    }

    public Sprite e_find;
    private IEnumerator _EffectFind()
    {
        EventSwitchOn();
        SC_SoundMgr._soundMgr.SFX_Find();
        Vector2 originPos = playerPos + new Vector2(1f, 0f);
        Color c = new Color(0, 0, 0, 0);
        indiTr.position = originPos;
        spriteRen.sprite = e_find;
        spriteRen.color = Color.yellow;
        for (int i = 0; i < 5; i++)
        {
            spriteRen.color = c;
            yield return SC_GameMgr._gameMgr.delay100ms;
            spriteRen.color = Color.yellow;
            yield return SC_GameMgr._gameMgr.delay100ms;
        }
        Clear();
        EventSwitchOff();
        isEvent = false;
    }
    public void EffectFind()
    {
        StartCoroutine(_EffectFind());
    }
    public Sprite e_simpleHit;
    private IEnumerator _EffectSimpleHit(Vector2 pos)
    {
        EventSwitchOn();
        indiTr.position = pos;
        spriteRen.sprite = e_simpleHit;
        yield return SC_GameMgr._gameMgr.delay100ms;
        spriteRen.flipX = true;
        yield return SC_GameMgr._gameMgr.delay100ms;
        spriteRen.flipY = true;
        yield return SC_GameMgr._gameMgr.delay100ms;
        spriteRen.flipX = false;
        yield return SC_GameMgr._gameMgr.delay100ms;
        spriteRen.flipY = false;
        yield return SC_GameMgr._gameMgr.delay100ms;
        Clear();
        EventSwitchOff();
        isEvent = false;
    }
    public void EffectSimpleHit(Vector2 pos) { StartCoroutine(_EffectSimpleHit(pos)); }

    public Sprite e_spot;
    private IEnumerator _EffectSpot()
    {
        EventSwitchOn();
        SC_SoundMgr._soundMgr.SFX_Spot();
        indiTr.position = enemyPos + new Vector2(0f, 1f);
        spriteRen.sprite = e_spot;
        for (int i = 0; i < 10; i++)
            yield return SC_GameMgr._gameMgr.delay100ms;
        Clear();
        EventSwitchOff();
        isEvent = false;
    }
    public void EffectSpot() { StartCoroutine(_EffectSpot()); }

    public Sprite e_down;
    private IEnumerator _EffectDown(Vector2 pos)
    {
        EventSwitchOn();
        indiTr.position = pos;
        spriteRen.sprite = e_down;
        for (int i = 0; i < 5; i++)
        {
            spriteRen.flipX = true;
            yield return SC_GameMgr._gameMgr.delay100ms;
            spriteRen.flipX = false;
            yield return SC_GameMgr._gameMgr.delay100ms;
        }
        Clear();
        EventSwitchOff();
        isEvent = false;
    }
    public void EffectDown(Vector2 pos) { StartCoroutine(_EffectDown(pos)); }

    public Sprite e_potion;
    private IEnumerator _EffectPotion(Color color)
    {
        EventSwitchOn();
        indiTr.position = playerPos;
        spriteRen.sprite = e_potion;
        spriteRen.color = color;
        for (int i = 0; i < 5; i++)
        {
            spriteRen.flipX = true;
            yield return SC_GameMgr._gameMgr.delay100ms;
            spriteRen.flipX = false;
            yield return SC_GameMgr._gameMgr.delay100ms;
        }
        Clear();
        EventSwitchOff();
        isEvent = false;
    }
    public void EffectPotion(Color color) { StartCoroutine(_EffectPotion(color)); }

    public Sprite e_bome;
    private IEnumerator _EffectBome()
    {
        EventSwitchOn();
        indiTr.position = enemyPos;
        spriteRen.sprite = e_bome;
        spriteRen.color = new Color(1, 1, 1, 0);
        yield return SC_GameMgr._gameMgr.delay100ms;
        spriteRen.color = new Color(1, 1, 1, 0.5f);
        yield return SC_GameMgr._gameMgr.delay100ms;
        spriteRen.color = Color.white;
        yield return SC_GameMgr._gameMgr.delay100ms;
        spriteRen.color = new Color(1, 1, 1, 0.9f);
        yield return SC_GameMgr._gameMgr.delay100ms;
        spriteRen.color = new Color(1, 1, 1, 0.6f);
        yield return SC_GameMgr._gameMgr.delay100ms;
        spriteRen.color = new Color(1, 1, 1, 0.3f);
        yield return SC_GameMgr._gameMgr.delay100ms;
        Clear();
        EventSwitchOff();
        isEvent = false;
    }
    public void EffectBome() { StartCoroutine(_EffectBome()); }

    public Sprite e_guard;
    private IEnumerator _EffectGuard(Vector2 pos)
    {
        EventSwitchOn();
        Color c = Color.white;
        indiTr.position = pos;
        spriteRen.sprite = e_guard;
        for (float f = 1f; f > -0.1f; f -= 0.2f)
        {
            c.a = f;
            spriteRen.color = c;
            yield return SC_GameMgr._gameMgr.delay100ms;
        }
        Clear();
        EventSwitchOff();
        isEvent = false;
    }
    public void EffectGuard(Vector2 pos) { StartCoroutine(_EffectGuard(pos)); }

    public Sprite e_counter;
    private IEnumerator _EffectCounter()
    {
        EventSwitchOn();
        indiTr.position = playerPos;
        spriteRen.sprite = e_counter;
        for (int i = 0; i < 8; i++)
            yield return SC_GameMgr._gameMgr.delay100ms;
        Clear();
        EventSwitchOff();
        isEvent = false;
    }
    public void EffectCounter() { StartCoroutine(_EffectCounter()); }

    private IEnumerator _EffectActiveCounter()
    {
        EventSwitchOn();
        indiTr.position = playerPos;
        spriteRen.sprite = e_counter;
        for (int i = 0; i < 8; i++)
            yield return SC_GameMgr._gameMgr.delay100ms;
        indiTr.position = enemyPos;
        spriteRen.sprite = e_simpleHit;
        yield return SC_GameMgr._gameMgr.delay100ms;
        spriteRen.flipX = true;
        yield return SC_GameMgr._gameMgr.delay100ms;
        spriteRen.flipY = true;
        yield return SC_GameMgr._gameMgr.delay100ms;
        spriteRen.flipX = false;
        yield return SC_GameMgr._gameMgr.delay100ms;
        spriteRen.flipY = false;
        yield return SC_GameMgr._gameMgr.delay100ms;
        Clear();
        EventSwitchOff();
        isEvent = false;
    }
    public void EffectActiveCounter() { StartCoroutine(_EffectActiveCounter()); }
}
