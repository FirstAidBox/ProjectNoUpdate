﻿using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "SBO/Create Enemy Data", order = 2)]
public class SBO_EnemyData : ScriptableObject
{
    public int Index;
    public KIND_ENEMY KIND;
    public string EnemyName;
    public Sprite Image;
    public Color Color = Color.white;
    public int Hp;
    public int Atk;
    public int Def;
    public int Spd;
    public string text;
    public SBO_SlotObject[] Skills;
}
