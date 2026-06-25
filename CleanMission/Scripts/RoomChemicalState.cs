using UnityEngine;
using static Enums;

/// <summary>
/// 部屋状態クラス
/// </summary>
public class RoomChemicalState
{
    public DetergentType lastType = DetergentType.Chlorine; //最終使用洗剤
    public float lastUseTime = -100; //最終洗剤使用時刻
}
