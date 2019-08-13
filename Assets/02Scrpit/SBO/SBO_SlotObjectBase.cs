using UnityEngine;

public abstract class SBO_SlotObjectBase : ScriptableObject
{
    public int Index;
    public string Name;
    public string Text;
    public string Tooltip;
    public Sprite Image;
    public Color Color = Color.white;
}
