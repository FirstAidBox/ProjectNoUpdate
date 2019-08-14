using UnityEngine;

public abstract class SBO_InstantObject : SBO_SlotObject, I_Instant
    //획득과 동시에 효과가 있는 오브잭트(얻자마자 사용되는 아이템, 패시브 스킬)에 쓰입니다.
{
    public virtual void GetEffect()
    {

    }
}
