using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_StringMgr : MonoBehaviour
//게임 내 대화나 설명 같은 길이가 길거나 자주쓰이는 텍스트들을 모아놓은 스크립트입니다.
//PrintTextBox나 BaseText에 자주 쓰이는 문자열들은 여기로
//네임스페이스를 사용하는 NS_StringData로 대체 예정.
{
    public readonly string warriorPick = "힘세고 좋은 전사입니다.\n유용한 기술은 조금 부족하지만, 강인한 체력과 방어력을 가지고있습니다.";
    public readonly string magePick = "총명한 마법사입니다.\n체력은 약하지만 적의 행동을 예측하고 강력한 마법을 사용할 수 있습니다.";
    public readonly string rangerPick = "기민한 순찰자입니다.\n직접적인 전투력은 부족하지만, 위험요소들을 쉽게 돌파할 수 있습니다.";

    public readonly string st_inn = "여관";
    public readonly string st_real = "정말 ";

    public readonly string st_enterInnStart = "여관으로 귀환합니다.";
    public readonly string st_enterInnEnd = "여관에 도착했습니다.";
    public readonly string st_answerInnExit = "정말 여관을 떠납니까?";
    public readonly string st_innExit = "여관을 떠납니다.";

    public readonly string st_selectArea = "목적지를 선택해주세요.";
    public readonly string st_area1 = "녹빛 숲. 흉포한 야수들과 그들을 저지하기 위한 덫들이 많이 깔려있습니다.";
    public readonly string st_area2 = "버려진 묘지. 행동은 느리지만 생명력이 질긴 언데드들이 있습니다.";
    public readonly string st_area3 = "설산. 기후에 적응에 털가죽이 두꺼워진 생물들이 살고 있습니다.";
    public readonly string st_tomove = "으로 이동합니다.";
    public readonly string st_moving = "으로 떠납니다.";
}

