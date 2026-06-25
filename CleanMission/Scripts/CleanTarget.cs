using UnityEngine;
using System.Collections;

/// <summary>
/// 複数汚れの処理管理
/// </summary>
public class CleanTarget : MonoBehaviour
{
    //変数の宣言
    Dirt[] dirts; //汚れの配列

    private void Awake()
    {
        //オブジェクトや子についているDirtを入れる
        dirts = GetComponentsInChildren<Dirt>();
    }

    /// <summary>
    /// 汚れを返すゲッター
    /// </summary>
    /// <returns></returns>
    public Dirt GetNextDirt()
    {
        // 未掃除の汚れを返す
        foreach (var dirt in dirts)
        {
            if (!dirt.IsCleaned())
                return dirt;
        }

        return null; // 全部終わり
    }

    /// <summary>
    /// インタラクト時に呼ばれる
    /// </summary>
    public void OnSelected()
    {
        //未掃除の汚れを取得
        Dirt targetDirt = GetNextDirt();

        if (targetDirt == null)
        {
            Debug.Log("すべて掃除済み");
            return;
        }

        //汚れのクイズを開始
        QuizUIController.Instance.ShowQuiz(targetDirt);
    }
}