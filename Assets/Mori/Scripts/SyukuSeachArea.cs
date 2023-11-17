using UnityEngine;
/// <summary>
/// 索敵範囲のBool管理
/// </summary>
public class SyukuSeachArea : MonoBehaviour
{
    //うでスクリプト取得
    [SerializeField]
    private SyukuHands _syukuHands;

    /// <summary>
    /// 索敵範囲内に入った時
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _syukuHands._isHandsMove = true;
    }
    /// <summary>
    /// 索敵範囲内から出た時
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        _syukuHands._isHandsMove = false;
    }
}
