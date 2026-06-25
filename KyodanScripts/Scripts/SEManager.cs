using UnityEngine;
using static Enums;

/// <summary>
/// SE再生
/// </summary>
public class SEManager : MonoBehaviour
{
    #region　変数の宣言
    [SerializeField] AudioSource audioSource; //audioSaurceを入れる変数
    [SerializeField] AudioClip hitCharacter; //人に当たった時のSE
    [SerializeField] AudioClip hitVase; //花瓶に当たった時のSE
    [SerializeField] AudioClip hitMirror; //鏡に当たった時のSE
    #endregion

    //インスタンス化する変数
    public static SEManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //SE音量を設定から取ってくる
        audioSource.volume = Settings.Instance.SeVolume;
    }

    /// <summary>
    /// SEの再生関数
    /// </summary>
    /// <param name="a"></param>
    public void PlaySE(SE se)
    {
        if (se == SE.hitVase)
        {
            //花瓶ヒットSE
            audioSource.PlayOneShot(hitVase);
        }
        else if (se == SE.hitCharacter)
        {
            //キャラクターヒットSE
            audioSource.PlayOneShot(hitCharacter);
        }
        else if (se == SE.hitMirror)
        {
            //鏡ヒットSE
            audioSource.PlayOneShot(hitMirror);
        }
    }
}
