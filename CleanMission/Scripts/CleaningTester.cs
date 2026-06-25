using UnityEngine;
using static Enums;

/// <summary>
/// ボタンで洗剤を使うテスト
/// </summary>
public class CleaningTester : MonoBehaviour
{
    //変数の宣言
    public CleaningSystem cleaningSystem; //CleaningSystemを入れる変数
    public Dirt targetDirt; //対象の汚れ

    // 塩素ボタン
    public void UseChlorine()
    {
        cleaningSystem.UseDetergent(targetDirt, DetergentType.Chlorine);
    }

    // 酸性ボタン
    public void UseAcid()
    {
        cleaningSystem.UseDetergent(targetDirt, DetergentType.Acid);
    }

    //アルカリボタン
    public void UseAlkali()
    {
        cleaningSystem.UseDetergent(targetDirt, DetergentType.Alkalis);
    }

    //有機溶剤ボタン
    public void UseOrganicSolvent()
    {
        cleaningSystem.UseDetergent(targetDirt, DetergentType.OrganicSolvent);
    }
}