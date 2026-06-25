using UnityEngine;

/// <summary>
/// 設定
/// </summary>
public class SettingsManager : MonoBehaviour
{
    #region 変数の宣言
    public static SettingsManager Instance; //インスタンス

    public bool invertY = false; //カメラ反転
    public float sensitivity = 100f; //視点感度
    public float bgmVolume = 1f; //BGM音量
    public float seVolume = 1f; //SE音量
    #endregion

    private void Awake()
    {
        //Instanceが空なら
        if (Instance == null)
        {
            Instance = this; //これを入れる
            DontDestroyOnLoad(gameObject); //ロードしても壊れない
        }
        //あるなら
        else
        {
            //これを消す
            Destroy(gameObject);
        }

        //LoadSettings();
    }

    /// <summary>
    /// 設定保存
    /// </summary>
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("InvertY", invertY ? 1 : 0); //カメラ反転をint型に変更して保存
        PlayerPrefs.SetFloat("Sensitivity", sensitivity); //感度保存
        PlayerPrefs.SetFloat("BGM", bgmVolume); //BGM音量を保存
        PlayerPrefs.SetFloat("SE", seVolume); //SE音量を保存
        PlayerPrefs.Save(); //書き込み
    }

    /// <summary>
    /// 設定読み込み
    /// </summary>
    void LoadSettings()
    {
        invertY = PlayerPrefs.GetInt("InvertY", 0) == 1; //カメラ反転をbool形に戻して読み取り
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f); //感度読み取り
        bgmVolume = PlayerPrefs.GetFloat("BGM", 1f); //BGM音量読み取り
        seVolume = PlayerPrefs.GetFloat("SE", 1f); //SE音量読み取り
    }
}