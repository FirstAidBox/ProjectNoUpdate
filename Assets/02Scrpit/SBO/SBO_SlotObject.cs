using UnityEngine;

public abstract class SBO_SlotObject : ScriptableObject
{
    public int Index;
    public string Name;
    public string Text;
    public Sprite Image;
    public Color Color = Color.white;
    public int Price;
}
