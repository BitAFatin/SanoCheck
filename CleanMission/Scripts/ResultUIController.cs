using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using System.Drawing;

/// <summary>
/// リザルト表示
/// </summary>
public class ResultUIController : MonoBehaviour
{
    #region 変数の宣言
    public static ResultUIController Instance; //インスタンス

    [Header("Panel")]
    [SerializeField] GameObject resultPanel; //リザルトパネル

    [Header("Text")]
    [SerializeField] TextMeshProUGUI scoreText; //スコアテキスト
    [SerializeField] TextMeshProUGUI evaluationText; //評価テキスト

    [Header("Image")]
    [SerializeField] Image image; //お客さん表示用Image
    [SerializeField] Sprite customer; //お客さんイラスト
    #endregion

    private void Awake()
    {
        Instance = this; //インスタンス化
        resultPanel.SetActive(false); //パネル非表示
    }

    /// <summary>
    /// リザルト表示反映
    /// </summary>
    /// <param name="score"></param>
    /// <param name="cleaned"></param>
    /// <param name="totalDirt"></param>
    /// <param name="time"></param>
    public void ShowResult(int score, int cleaned, int totalDirt, float time)
    {
        resultPanel.SetActive(true); //パネル表示

        image.sprite = customer; //お客さんの画像にする
        image.SetNativeSize();
        image.transform.localScale = new Vector2(0.5f, 0.5f);

        evaluationText.color = Color.black; //文字を黒色に

        float rate = cleaned / totalDirt; //正解率

        Debug.Log(rate);

        //成功率に応じて評価
        if (rate >= 1)
        {
            evaluationText.text = "ありがとう！ 見違えるぐらいとってもきれいになったよ！\n" +
                                    "また次もよろしく!";
        }
        else if(rate >= 0.6)
        {
            evaluationText.text = "おかげさまできれいになったよ！ ありがとう！\n" +
                                    "またお願いしようかな...！";
        }
        else if(rate > 0)
        {
            evaluationText.text = "まだ残ってる汚れは次のときにお願いしようかな！\n" +
                                    "きれいにしてくれてありがとう";
        }
        else if(rate <= 0)
        {
            evaluationText.text = "汚れはたくさんあるからね　これからに期待だね";
        }

        scoreText.color = Color.black; //文字を黒色に

        //スコア表示
        scoreText.text =
                "成功数: " + cleaned + "/" + totalDirt + "\n" +
                "残り時間: " + Mathf.FloorToInt(time) + "\n\n" +
                "Score : " + score;
    }
}