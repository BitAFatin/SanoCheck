using UnityEngine;
using TMPro;

/// <summary>
/// 時間制限UI管理
/// </summary>
public class TimerUIController : MonoBehaviour
{
    //変数の宣言
    [SerializeField] TextMeshProUGUI timerText; //制限時間テキスト

    void Update()
    {
        //ゲームマネージャーインスタンスがなければ処理しない
        if (GameManager.Instance == null) return;

        float time = GameManager.Instance.GetRemainingTime(); //現在時間を取得

        int minutes = Mathf.FloorToInt(time / 60); //分単位
        int seconds = Mathf.FloorToInt(time % 60); //秒単位

        //時間表示
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}