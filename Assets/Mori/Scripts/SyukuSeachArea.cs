using UnityEngine;
/// <summary>
/// õGÍÍÌBoolÇ
/// </summary>
public class SyukuSeachArea : MonoBehaviour
{
    //¤ÅXNvgæ¾
    [SerializeField]
    private SyukuHands _syukuHands;

    /// <summary>
    /// õGÍÍàÉüÁ½
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _syukuHands._isHandsMove = true;
    }
    /// <summary>
    /// õGÍÍà©ço½
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        _syukuHands._isHandsMove = false;
    }
}
