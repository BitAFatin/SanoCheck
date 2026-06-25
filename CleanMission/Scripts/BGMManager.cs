using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// BGM管理
/// </summary>
public class BGMManager : MonoBehaviour
{
    #region 変数の宣言
    public static BGMManager Instance; //インスタンス

    AudioSource audioSource; //AudioSourceを入れる変数

    //
    [System.Serializable]
    public class BGMData
    {
        public int sceneNumber; //シーン番号
        public AudioClip bgm; //BGMの入れる変数
    }

    [SerializeField] List<BGMData> bgmList; //

    int currentScene = -1; //現在のシーン　タイトルでも呼ぶため初期値はマイナス
    #endregion

    void Awake()
    {
        // シングルトン
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //オブジェクトについているAudioSourceを入れる
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; //ループ有効化
    }

    void OnEnable()
    {
        //シーンが変わったときに呼び出されるようにする
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        //シーンが変わったときに呼び出される対象から外す
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        //音量を設定からとってくる
        audioSource.volume = SettingsManager.Instance.bgmVolume;
    }

    /// <summary>
    /// シーン変更時に呼び出す処理
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 同じシーンなら何もしない
        if (scene.buildIndex == currentScene) return;

        //シーン番号を保存
        currentScene = scene.buildIndex;

        //シーンに合わせたBGMを再生
        BGMPlay(scene.buildIndex);
    }

    /// <summary>
    /// BGM再生
    /// </summary>
    /// <param name="num"></param>
    public void BGMPlay(int index)
    {
        foreach (var data in bgmList)
        {
            //シーンが同じなら
            if (data.sceneNumber == index)
            {
                //再生しているBGMが同じときは処理を止める
                if (audioSource.clip == data.bgm) return;

                //シーンに合わせたBGMにする
                audioSource.clip = data.bgm;
                audioSource.Play();
                return;
            }
        }

        // 見つからなかった場合
        audioSource.Stop();
    }
}
