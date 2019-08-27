using UnityEngine;

[CreateAssetMenu(fileName = "Item_Elixir", menuName = "SBO/Item/Elixir", order = 3)]
public class SBO_Elixir : SBO_InstantObject
{
    public enum STAT { ATK = 1, DEF, SPD}

    public STAT increaseStat;
    public int increaseValue;

    public override void GetEffect()
    {
        if (increaseStat == STAT.ATK)
            SC_PlayerMgr._playerMgr.ATK += increaseValue;
        else if (increaseStat == STAT.DEF)
            SC_PlayerMgr._playerMgr.DEF += increaseValue;
        else if (increaseStat == STAT.SPD)
            SC_PlayerMgr._playerMgr.SPD += increaseValue;
    }
}
