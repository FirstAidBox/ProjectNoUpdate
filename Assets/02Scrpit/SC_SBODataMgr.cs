using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SBODataMgr : MonoBehaviour
//SBO데이터들을 불러옵니다.
{
    public static SC_SBODataMgr _SBODataMgr;

    public List<SBO_SlotObject> itemData;
    public List<SBO_SlotObject> playerSkillData;

	public List<SBO_EnemyData> area1EnemyData;
	public List<SBO_EnemyData> area2EnemyData;
	public List<SBO_EnemyData> area3EnemyData;
	public List<SBO_SlotObject> enemySkillData;

	void Awake()
	{
		_SBODataMgr = this;

        itemData.AddRange(Resources.LoadAll<SBO_SlotObject>("itemdata"));
		SortByIndex (itemData);
        playerSkillData.AddRange(Resources.LoadAll<SBO_SlotObject>("player/skilldata"));
        SortByIndex(playerSkillData);

        area1EnemyData.AddRange(Resources.LoadAll<SBO_EnemyData>("enemy/statdata/area1"));
        SortByIndex(area1EnemyData);
        area2EnemyData.AddRange(Resources.LoadAll<SBO_EnemyData>("enemy/statdata/area2"));
        SortByIndex(area2EnemyData);
        area3EnemyData.AddRange(Resources.LoadAll<SBO_EnemyData>("enemy/statdata/area3"));
        SortByIndex(area3EnemyData);
        enemySkillData.AddRange(Resources.LoadAll<SBO_SlotObject>("enemy/skilldata"));
        SortByIndex(enemySkillData);
    }
	public void SortByIndex(List<SBO_SlotObject> inputList)
	{
		inputList.Sort (delegate (SBO_SlotObject A, SBO_SlotObject B) {
			if (A.Index > B.Index)
				return 1;
			else if (A.Index < B.Index)
				return -1;
			else if (A.Index == B.Index) {
				if (A.name != B.name)
					Debug.Log (string.Format ("{0} 와 {1} 의 인덱스가 같습니다.", A.name, B.name));
				return 0;
			}
			return 0;
		});
		for (int i = 0; i < inputList.Count; i++) {
			if (inputList [i].Index != i)
				Debug.Log (string.Format (
					"{0} 의 인덱스와 배열번호가 일치하지 않습니다. {0} 의 인덱스: {1}, 배열번호: {2}", inputList [i].name, inputList [i].Index, i));
		}
	}
	public void SortByIndex(List<SBO_EnemyData> inputList)
	{
		inputList.Sort (delegate (SBO_EnemyData A, SBO_EnemyData B) {
			if (A.Index > B.Index)
				return 1;
			else if (A.Index < B.Index)
				return -1;
			else if (A.Index == B.Index) {
				if (A.name != B.name)
					Debug.Log (string.Format ("{0} 와 {1} 의 인덱스가 같습니다.", A.name, B.name));
				return 0;
			}
			return 0;
		});
		for (int i = 0; i < inputList.Count; i++) {
			if (inputList [i].Index != i)
				Debug.Log (string.Format (
					"{0} 의 인덱스와 배열번호가 일치하지 않습니다. {0} 의 인덱스: {1}, 배열번호: {2}", inputList [i].name, inputList [i].Index, i));
		}
		if (inputList [0].KIND != KIND_ENEMY.BOSS)
			Debug.Log (string.Format ("인덱스 0번 데이터 {0} 의 종류가 우두머리가 아닙니다.", inputList[0].name));
	}
}
