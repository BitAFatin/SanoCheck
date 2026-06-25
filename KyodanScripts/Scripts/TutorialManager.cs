using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チュートリアル進行
/// </summary>
public class TutorialManager : MonoBehaviour
{
    #region 変数の宣言
    [Header("各ステージに昇べきに対応")]
    [SerializeField] List<Transform> playerSpawn; //プレイヤーのスポーン座標
    [SerializeField] List<Transform> bulletSpwan; //弾丸のスポーン座標
    [SerializeField] List<Transform> camPos; //トップカメラの設置座標
    [SerializeField] List<GameObject> panel; //チュートリアルパネル

    [Header("移動物")]
    [SerializeField] Camera topView; //トップカメラ
    [SerializeField] GameObject bullet; //弾丸生成オブジェクト
    [SerializeField] GameObject player; //プレイヤー

    [SerializeField] GameObject gameOverPanel; //ゲームオーバーパネル
    [SerializeField] GameObject gameOverRetry; //リトライボタン

    [SerializeField] int showPanelTime = 3; //パネル表示時間
    readonly int playerWait = 1; //移動時のプレイヤー待機時間

    //ステージ番号
    int stageNumber = 0;
    #endregion

    //インスタンス化する変数
    public static TutorialManager Instance { get; private set; }

    private void Awake()
    {
        //インスタンス化
        Instance = this;

        UpdateStage();
    }

    void Start()
    {
        //各パネル非表示
        foreach(GameObject go in panel)
        {
            go.SetActive(false);
        }
    }

    /// <summary>
    /// ステージ番号更新
    /// </summary>
    public void UpdateStageNumber()
    {
        panel[stageNumber].SetActive(false);

        stageNumber++;
        UpdateStage();
    }

    /// <summary>
    /// ステージ更新関数
    /// </summary>
    public void UpdateStage()
    {
        Debug.Log("呼ばれました");

        //ステージ番号がリストの要素数以下なら
        if (stageNumber < camPos.Count)
        {
            MovePos();

            StartCoroutine(MovePlayerPos());
            StartCoroutine(ShowSubject());
        }
        else
        {
            GameManager.Instance.ShowClearPanel();
        }
    }

    /// <summary>
    /// プレイヤーのステージ移動
    /// </summary>
    /// <returns></returns>
    IEnumerator MovePlayerPos()
    {  
        //キャラクターコントローラーを無効にする
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = playerSpawn[stageNumber].position; //プレイヤーの位置を動かす

        yield return new WaitForSeconds(playerWait); //ディレイを入れる

        //キャラクターコントローラーを有効にする
        player.GetComponent<CharacterController>().enabled = true;
    }

    /// <summary>
    /// ステージ目標表示関数
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowSubject()
    {
        //パネルの表示非表示
        panel[stageNumber].SetActive (true);
        yield return new WaitForSeconds (showPanelTime);
        panel[stageNumber].SetActive(false);
    }

    /// <summary>
    /// 各オブジェクトをステージに対応した場所に移動
    /// </summary>
    void MovePos()
    {
        topView.transform.position = camPos[stageNumber].position;
        bullet.transform.position = bulletSpwan[stageNumber].position;
        bullet.transform.rotation = bulletSpwan[stageNumber].rotation;
    }
}
