using UnityEngine;
/// <summary>
/// õGÍÍÌBoolÇ
/// </summary>
public class SyukuSeachArea : MonoBehaviour
{
    //¤ÅXNvgæ¾
    [SerializeField]
    private SyukuHands _syukuHands;
    //vC[ÌRC_[æ¾A¯Êp
    private CircleCollider2D _marimoC;
    private GameObject _marimoG;

    private void Start()
    {
        _marimoG = GameObject.Find("bone_11");
        _marimoC = _marimoG.GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// õGÍÍàÉüÁ½
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision ==_marimoC)
        {
            _syukuHands._isHandsMove = true;
        }

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
