using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerJobData", menuName = "SBO/Create New Player Job Data", order = 1)]
public class SBO_PlayerJobData : ScriptableObject
{
    public string jobName;
    public Sprite image;
    public int hp;
    public int atk;
    public int def;
    public int spd;
}
