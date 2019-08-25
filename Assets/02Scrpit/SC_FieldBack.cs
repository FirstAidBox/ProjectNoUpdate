using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FieldBack : MonoBehaviour
{
    public SpriteRenderer[] renderers;
    public Transform tr;
    private void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        tr = GetComponent<Transform>();
    }

    public void SpriteChange(Sprite top, Sprite mid, Sprite bottom)
    {
        renderers[0].sprite = top;
        renderers[1].sprite = mid;
        renderers[2].sprite = bottom;
    }
}
