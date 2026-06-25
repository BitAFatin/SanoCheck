using System.Collections;
using UnityEngine;

/// <summary>
/// ”j•Ğ‚ª”ò‚ÑU‚éƒXƒNƒŠƒvƒg
/// </summary>
public class BreakVaseAnimation : MonoBehaviour
{
    #region •Ï”‚ÌéŒ¾
    [SerializeField] GameObject normalVase; //’Êíó‘Ô‚Ì‰Ô•r‚ğ“ü‚ê‚é•Ï”
    [SerializeField] GameObject brokenVase; //‰ó‚ê‚½ó‘Ô‚Ì‰Ô•r‚ğ“ü‚ê‚é•Ï”
    [SerializeField] float explosionForce = 200f; //”š”­—Í
    [SerializeField] float explosionRadius = 2f; //”š”­”¼Œa

    readonly float waitTime = 3f;
    #endregion

    void Start()
    {
        Initialization();  
    }

    /// <summary>
    /// ‰Šú‰»
    /// </summary>
    void Initialization()
    {
        //‰Šú‰»
        normalVase.SetActive(true); //’Êíó‘Ô‚Ì‰Ô•r‚ğ•\¦‚·‚é
        brokenVase.SetActive(false); //‰ó‚ê‚½ó‘Ô‚Ì‰Ô•r‚ğ”ñ•\¦‚É‚·‚é
    }

    #region ”šUˆ—
    /// <summary>
    /// ”j•Ğ‚ğ”ò‚Î‚·ŠÖ”
    /// </summary>
    public void Break()
    {
        normalVase.SetActive(false); //’Êíó‘Ô‚Ì‰Ô•r‚ğ”ñ•\¦‚É‚·‚é
        brokenVase.SetActive(true); //‰ó‚ê‚½ó‘Ô‚Ì‰Ô•r‚ğ•\¦‚·‚é

        //‰ó‚ê‚½‰Ô•r‚Ì”j•Ğ‚ÌRigidbody‚ğ•Ï”rb‚É“ü‚ê‚é
        foreach (Rigidbody rb in brokenVase.GetComponentsInChildren<Rigidbody>())
        {
            //Œ»İ‚ÌÀ•W‚©‚ç”¼ŒaexplosionRadius‚Ì”ÍˆÍ‚É‚ ‚é•Ï”rb‚ğexplosionForce‚Ì‹­‚³‚Å‚Á”ò‚Î‚·
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        StartCoroutine(RemovePiece()); //”j•Ğ‚Ì•¨—‰‰Z‚ğ~‚ß‚éƒRƒ‹[ƒ`ƒ“‚ğŒÄ‚Ño‚·
    }

    /// <summary>
    /// ”j•Ğ‚Ì•¨—‰‰Z‚ğ~‚ß‚éƒRƒ‹[ƒ`ƒ“
    /// </summary>
    /// <returns></returns>

    IEnumerator RemovePiece()
    {

        #if UNITY_EDITOR
        //ƒfƒoƒbƒO—p
        Debug.Log("RemovePieceƒRƒ‹[ƒ`ƒ“‚ªŒÄ‚Î‚ê‚Ü‚µ‚½");
        #endif

        yield return new WaitForSeconds(waitTime); //3•b‘Ò‚Â

        //‰ó‚ê‚½‰Ô•r‚Ì”j•Ğ‚ÌRigidbody‚ğ•Ï”rb‚É“ü‚ê‚é
        foreach (Rigidbody rb in brokenVase.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true; //•¨—‰‰Z’â~
        }
    }
    #endregion
}
