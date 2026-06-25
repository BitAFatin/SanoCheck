using System.Security.Cryptography;
using UnityEngine;
using static Enums;

/// <summary>
/// ôÜ‚Æ‰˜‚ê‚Ì³‰ğ”»’è‚ğs‚¤
/// </summary>
public class CleaningSystem : MonoBehaviour
{
    /// <summary>
    /// ôÜ‚ğg‚¤ˆ—
    /// </summary>
    /// <param name="dirt"></param>
    /// <param name="detergent"></param>
    public void UseDetergent(Dirt dirt, DetergentType detergent)
    {
        //ôÜ‚ª³‚µ‚©‚Á‚½
        if (dirt.correctType == detergent)
        {
            dirt.Clean(); //³‰ğˆ—
        }
        //ŠÔˆá‚¢‚Ì
        else
        {
            dirt.Fail(); //¸”sˆ—
            Debug.Log("•s³‰ğ‚ÌôÜ‚Å‚·");
        }
    }
}