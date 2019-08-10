using UnityEngine;

//[CreateAssetMenu(fileName = "TestObject", menuName = "Create New TestObject", order = 1111)]
public class TestSBO : ScriptableObject  
{
    public new string name;
    public string description;

    public Sprite artwork;

    public int ATK;
    public int HP;

    public void Print()
    {
        Debug.Log(name + ": " + description + " 공격력: " + ATK + " 생명력: " + HP);
    }
}
