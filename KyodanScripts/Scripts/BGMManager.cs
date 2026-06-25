using UnityEngine;

public class BGMManager : MonoBehaviour
{
    AudioSource audioSource; //audioSaurce‚ğ“ü‚ê‚é•Ï”

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; //ƒ‹[ƒv—LŒø‰»
    }

    void Update()
    {
        //BGM‰¹—Ê‚ğİ’è‚©‚çæ‚Á‚Ä‚­‚é
        audioSource.volume = Settings.Instance.BgmVolume;
    }
}
