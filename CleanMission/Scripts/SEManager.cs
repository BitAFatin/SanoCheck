using UnityEngine;

/// <summary>
/// SE管理
/// </summary>
public class SEManager : MonoBehaviour
{
    #region 変数の宣言
    public static SEManager Instance; //インスタンス

    AudioSource audioSource; //AudioSourceを入れる変数

    [SerializeField] AudioClip cleanSE; //成功SE
    [SerializeField] AudioClip failSE; //失敗SE
    #endregion

    private void Start()
    {
        Instance = this; //Instanceに入れる

        //オブジェクトについているAudioSourceを入れる
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //音量を設定からとってくる
        audioSource.volume = SettingsManager.Instance.seVolume;
    }

    /// <summary>
    /// SE再生 0:掃除成功 1:掃除失敗
    /// </summary>
    /// <param name="num"></param>
    public void SEPlay(int num)
    {
        if (num == 0)
        {
            audioSource.PlayOneShot(cleanSE);
        }
        else if(num == 1)
        {
            audioSource.PlayOneShot(failSE);
        }
    }
}
