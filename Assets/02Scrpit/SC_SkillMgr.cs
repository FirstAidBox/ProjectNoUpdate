using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SkillMgr : MonoBehaviour
{
    public static SC_SkillMgr _skillMgr;

    public List<TestSkill> testSkills;

    private void Awake()
    {
        _skillMgr = this;

        testSkills.AddRange(Resources.LoadAll<TestSkill>("player/skilldata"));
        testSkills.Sort(delegate (TestSkill A, TestSkill B)
        {
            if (A.Index > B.Index) return 1;
            else if (A.Index < B.Index) return -1;
            else if (A.Index == B.Index)
            {
                if(A.name != B.name)
                    Debug.Log(string.Format("{0} 와 {1} 의 인덱스가 같습니다.", A.name, B.name));
                return 0;
            }
            return 0;
        });
        for(int i=0; i<testSkills.Count; i++)
        {
            if (testSkills[i].Index != i)
                Debug.Log(string.Format(
                    "{0} 의 인덱스와 배열번호가 일치하지 않습니다. {0} 의 인덱스: {1}, 배열번호: {2}", testSkills[i].name, testSkills[i].Index, i));
        }
    }
}
