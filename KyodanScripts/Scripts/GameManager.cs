using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using static Enums;

/// <summary>
/// ゲーム管理
/// </summary>
public class GameManager : MonoBehaviour
{
    #region 変数の宣言
    [SerializeField] GameObject clearPanel; //クリアパネル
    [SerializeField] GameObject gameOverPanel; //ゲームオーバーパネル
    [SerializeField] TextMeshProUGUI ammoText; //残弾数を表示するテキスト
    [SerializeField] TextMeshProUGUI mirrorText; //鏡設置可能数を表示するテキスト
    [SerializeField] GameObject modeText; //CreateMode表示用テキスト
    [SerializeField] TextMeshProUGUI operationText; //操作説明表示テキスト
    [SerializeField] GameObject pausePanel; //設定パネルを入れる変数

    [SerializeField] CreateModeManager createModeManager; //CreateModeManagerを入れる変数

    [Header("コントローラーボタン対応用")]
    [SerializeField] GameObject clearNext; //クリア時のNextボタン
    [SerializeField] GameObject gameOverRetry; //ゲームオーバー時のRetryボタン
    [SerializeField] GameObject pauseSetting; //ポーズ時の設定ボタン
    [SerializeField] GameObject settingsClose; //設定時のCloseボタン
    [SerializeField] GameObject controllerClose; //操作説明時のCloseボタン

    PlayerInput playerInput; //PlayerInputを入れる変数
    MirrorPreview mirrorPreview; //MirrorPreviewを入れる変数

    //設定パネル表示フラグ
    bool showPanel = false;
    GameState gameState; //ゲームステータス
    #endregion

    //インスタンス化する変数
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        //Instanceがあるかつこれじゃないなら
        if(Instance != null && Instance != this)
        {
            //Instanceを壊す
            Destroy(Instance);
            return;
        }

        //このスクリプトをインスタンスに入れる
        Instance = this;
    }

    void Start()
    {
        //クリア画面を非表示にする
        clearPanel.SetActive(false);
        //ゲームオーバー画面を非表示にする
        gameOverPanel.SetActive(false);
        //ポーズ画面を非表示
        pausePanel.SetActive(false);

        //PlayerInputを入れる
        playerInput = FindAnyObjectByType<PlayerInput>();

        //ゲームステートをPlayingにする
        ChangeGameState(GameState.Playing);

        StartCoroutine(ReativePreview());
    }

    /// <summary>
    /// MirrorPreviewの再アクティブ
    /// Stage2のPreviewMirrorのバグ防止用
    /// </summary>
    /// <returns></returns>
    IEnumerator ReativePreview()
    {
        //MirrorPreviewを探し非アクティブ化
        mirrorPreview = FindAnyObjectByType<MirrorPreview>();
        mirrorPreview.enabled = false;

        yield return null; //1フレーム待つ

        mirrorPreview.enabled = true; //アクティブ化
    }

    void Update()
    {
        //CreateModeなら
        if (createModeManager.IsCreateMode)
        {
            //CreateMode用操作表示
            operationText.text = "Tab:ポーズ\n" +
                                 "Shift:モード切替\n" +
                                 "F:俯瞰視点\n" +
                                 "Q:表示階数変更\n" +
                                 "左クリック:鏡設置\n" +
                                 "右クリック:鏡回収\n" +
                                 "R:鏡の全回収\n" +
                                 "ホイール:鏡回転";
        }
        else
        {
            //通常Mode用操作表示
            operationText.text = "Tab:ポーズ\n" +
                                 "Shift:モード切替\n" +
                                 "F:俯瞰視点\n" +
                                 "Q:表示階数変更\n" +
                                 "V:発射";
        }

        //Pauseボタンが押されたら
        if (playerInput.actions["Pause"].triggered)
        {
                ShowPausePanelUI();
        }
    }

    /// <summary>
    /// ActionMaps切り替え
    /// </summary>
    /// <param name="maps"></param>
    public void SwitchActionMaps(string maps)
    {
        if (maps == "UI")
        {
            //マウスカーソル表示
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            //マウスカーソル非表示
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if(playerInput != null)
        {
          playerInput.SwitchCurrentActionMap(maps);
        }
    
    }

    /// <summary>
    /// ボタンを選択状態にする関数
    /// </summary>
    /// <param name="button"></param>
    public void SetCurrentButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(button);
    }

    /// <summary>
    /// ゲームステートの変更
    /// </summary>
    public void ChangeGameState(GameState states)
    {

        if (states == GameState.Playing)
        {
            gameState = GameState.Playing;
        }
        else if (states == GameState.Stop)
        {
            gameState = GameState.Stop;
        }
    }

    public bool IsPlaying()
    {
        return gameState == GameState.Playing;
    }
    
    #region UI管理
    /// <summary>
    /// ゲームクリア画面を表示する関数
    /// </summary>
    public void ShowClearPanel()
    {
        ChangeGameState(GameState.Stop);
        clearPanel.SetActive(true); 
        SwitchActionMaps("UI");
        SetCurrentButton(clearNext);
    }

    /// <summary>
    /// ゲームオーバー画面を表示する関数
    /// </summary>
    public void ShowGameOverPanel()
    {
        ChangeGameState(GameState.Stop);
        Debug.Log("GameStateをStopに変更しました");

        gameOverPanel.SetActive(true);
        Debug.Log("GameOverPanelを表示しました");

        SwitchActionMaps("UI");
        Debug.Log("PlayerInputのActionMapsをUIに変更しました");

        SetCurrentButton(gameOverRetry);
    }

    /// <summary>
    /// 残弾数を表示する関数
    /// </summary>
    /// <param name="ammo"></param>
    public void UpdateAmmoText(int ammo)
    {
        if (ammo >= 0)
        {
            ammoText.text = "残弾数×" + ammo;
        }
        else
        {
            ammoText.text = "残弾数×∞";
        }
    }

    /// <summary>
    /// 鏡設置可能数を表示する関数
    /// </summary>
    /// <param name="mirrorNumber"></param>
    public void UpdateSetMirrorNumber(int mirrorNumber)
    {
        if (mirrorNumber >= 0)
        {
            mirrorText.text = "鏡設置可能数×" + mirrorNumber;
        }
        else
        {
            mirrorText.text = "鏡設置可能数×∞";
        }
    }

    /// <summary>
    /// Mode表示
    /// </summary>
    /// <param name="createModeFlag"></param>
    public void ShowModeText(bool createModeFlag)
    {
        if (createModeFlag)
        {
            modeText.SetActive(true);
        }
        else
        {
            modeText.SetActive(false);
        }
    }

/// <summary>
/// ゲームオーバー処理
/// </summary>
    public void GameOverProcess()
    {   
            ShowGameOverPanel();
            SwitchActionMaps("UI");
    }

    /// <summary>
    /// 設定パネルの表示非表示関数
    /// </summary>
    public void ShowPausePanelUI()
    {
        //フラグのトグル
        showPanel = !showPanel;

        //trueなら
        if (showPanel)
        {
            //ゲームステートをStopに変更
            ChangeGameState(GameState.Stop);

            //設定パネルの表示
            pausePanel.SetActive(true);

            //カーソル表示
            SwitchActionMaps("UI");

            //設定ボタンを選択状態にする
            SetCurrentButton(pauseSetting);
        }
        else
        {
            //ゲームステートをPlayingに変更
            ChangeGameState(GameState.Playing);

            //設定パネルの非表示
            pausePanel.SetActive(false);

            //カーソル非表示
            SwitchActionMaps("PlayerAction");

            //選択状態解除
            SetCurrentButton(null);
        }
    }
    #endregion
}
