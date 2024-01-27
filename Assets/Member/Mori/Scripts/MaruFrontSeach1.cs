using UnityEngine;
/// <summary>
/// 索敵範囲のBool管理
/// </summary>
public class MaruFrontSeach1 : MonoBehaviour
{
    //プレイヤーのコライダー取得、識別用
    private CircleCollider2D _marimo;
    private GameObject _marimoG;
    //プレイヤーのコライダー取得、識別用
    [SerializeField]
    private StopMaru _maru;
    private void Start()
    {
        _marimoG = GameObject.Find("bone_11");
        _marimo = _marimoG.GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// 索敵範囲内に入った時
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == _marimo)
        {
            _maru._playerFrontHere = true;
        }

    }
    /// <summary>
    /// 索敵範囲内から出た時
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == _marimo)
        {
            _maru._playerFrontHere = false;
        }
    }
}
