using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// シュクシュクのうでの動きのスクリプト
/// </summary>
public class SyukuHands : MonoBehaviour
{
    //自身のTransform
    [SerializeField]
    private Transform _armsTra;
    //自身のCollider2D
    [SerializeField]
    private BoxCollider2D _armsCol;
    //ターゲットのTransform
    private Transform _targetTra;
    //ターゲットのRigidbody2D
    private Rigidbody2D _targetRig;
    //プレイヤータグ取得用
    private GameObject _marimo;
    //プレイヤー親取得
    private GameObject _marimoPa;
    //プレイヤー引っ張り用
    [SerializeField]
    private GameObject _leader;
    //プレイヤー操作のスクリプト
    private PlayerMovement _marimoScr;
    //腕が動く条件にあるか
    [Header("索敵範囲内にプレイヤーがいるか、居る：true")]
    public bool _isHandsMove = false;
    //腕を伸ばす(スプライトを伸ばす)ためのSpriteRenderer取得
    private SpriteRenderer _syuSpr;
    //腕をどれくらいのばすか
    [SerializeField, Header("腕の長さ限界値、どのくらい伸びるか")]
    private float _syuHandHeightRim = 10f;
    //腕ののびる速度
    [SerializeField, Header("腕がプレイヤーに向かって伸びる速さ")]
    private float _handSpeed = 30;
    //腕ののびる速度：代入用
    private float _handLong = 0.5f;
    //腕ののびる速度：Collider代入用
    private float _handColSize = 0.4f;
    //手を向かせたい方向
    private Vector3 dir;
    //プレイヤーを今掴んでいるか
    private bool _chaching = false;
    //引き寄せられるプレイヤーの速さ
    [SerializeField, Header("引き寄せる速さ")]
    private float _attractionSpeed = 1f;

    /// <summary>
    /// 初期設定
    /// ・うでのSpriteRendererとBoxCollider2Dの取得
    /// </summary>
    private void Start()
    {
        PlayerSet();
    }
    private void PlayerSet()
    {
        _syuSpr = this.GetComponent<SpriteRenderer>();
        _armsCol = this.GetComponent<BoxCollider2D>();
        _marimo = GameObject.Find("bone_11");
        _marimoPa = GameObject.Find("Cha_MonyuSmile1");
        _targetRig = _marimo.GetComponent<Rigidbody2D>();
        _marimoScr = _marimo.GetComponent<PlayerMovement>();
        _targetTra = _marimo.GetComponent<Transform>();
    }

    /// <summary>
    /// 匂いで判定してうでが動く
    /// </summary>
    void Update()
    {
        //いい匂いで、索敵範囲内で、掴まれていない時
        if (_marimo.gameObject.tag == "Florus" && _isHandsMove == true && _chaching == false)
        {
            HandsMove();
            HandStretch();
        }
        //掴まれた時
        else if (_chaching == true)
        {
            HandShrink();
            if (_handLong <= 1f)
            {
                Debug.LogWarning("Player:Die");
            }
        }

        //動作確認
        Debug.Log("Playerが索敵範囲内にいる：" + _isHandsMove + " , "
            + "掴んでいる：" + _chaching + " , "
            + "プレイヤーのタグ：" + _marimo.gameObject.tag);

        //まずい匂いになった時
        if (_marimo.gameObject.tag == "Odor")
        {
            HandShrink();
            _targetRig.bodyType = RigidbodyType2D.Dynamic;
            _marimoPa.transform.parent = null;
            _chaching = false;
            _marimoScr.enabled = true;

            //追加箇所
            // プレイヤーとその子オブジェクトのRigidbodyType2Dを切り替える
            Rigidbody2D[] childRigidbodies = _marimoPa.GetComponentsInChildren<Rigidbody2D>();
            foreach (Rigidbody2D childRigidbody in childRigidbodies)
            {
                childRigidbody.bodyType = RigidbodyType2D.Dynamic;
            }
            // Disable Collider for each child object in _marimoPa
            Collider2D[] childColliders = _marimoPa.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D childCollider in childColliders)
            {
                if (childCollider.gameObject != _marimoPa)
                {
                    childCollider.enabled = true;
                }
            }
        }
    }

    /// <summary>
    /// プレイヤーとうでが当たった時、プレイヤーを動けなくする（匂い切り替え除く）
    /// ・プレイヤーの操作スクリプトを無効にする
    /// ・プレイヤーの重力を無くす
    /// ・掴んでいるか判定するboolをtrueにする
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Florus" || collision.gameObject.tag == "Player")
        {
            Debug.Log("キャッチT");
            _chaching = true;
            // _targetRig.bodyType = RigidbodyType2D.Kinematic;
            _marimoPa.transform.parent = _leader.transform;
            _marimoScr.enabled = false;

            // プレイヤーとその子オブジェクトのRigidbodyType2Dを切り替える
            Rigidbody2D[] childRigidbodies = _marimoPa.GetComponentsInChildren<Rigidbody2D>();
            foreach (Rigidbody2D childRigidbody in childRigidbodies)
            {
                if (childRigidbody.gameObject != _marimoPa)
                {
                    childRigidbody.bodyType = RigidbodyType2D.Kinematic;
                }
            }
            // Disable Collider for each child object in _marimoPa
            Collider2D[] childColliders = _marimoPa.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D childCollider in childColliders)
            {
                if (childCollider.gameObject != _marimoPa)
                {
                    childCollider.enabled = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Florus" || collision.gameObject.tag == "Player")
        {
            Debug.Log("キャッチC");
            _chaching = true;
            //_targetRig.bodyType = RigidbodyType2D.Kinematic;
            _marimoScr.enabled = false;

            // プレイヤーとその子オブジェクトのRigidbodyType2Dを切り替える
            Rigidbody2D[] childRigidbodies = _marimoPa.GetComponentsInChildren<Rigidbody2D>();
            foreach (Rigidbody2D childRigidbody in childRigidbodies)
            {
                if (childRigidbody.gameObject != _marimoPa)
                {
                    childRigidbody.bodyType = RigidbodyType2D.Kinematic;
                }
            }
            // Disable Collider for each child object in _marimoPa
            Collider2D[] childColliders = _marimoPa.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D childCollider in childColliders)
            {
                if (childCollider.gameObject != _marimoPa)
                {
                    childCollider.enabled = true;
                }
            }
        }
    }
    /// <summary>
    /// プレイヤーに向かってうでを伸ばす
    /// </summary>
    private void HandsMove()
    {
        //向きたい方向を計算
        dir = (_targetTra.position - _armsTra.position);
        //向きたい方向に回転
        _armsTra.rotation = Quaternion.FromToRotation(Vector3.up, -dir);
    }

    /// <summary>
    /// うで伸びる
    /// </summary>
    private void HandStretch()
    {
        if (_handLong > -_syuHandHeightRim)
        {
            _handLong += _handSpeed / 100;
            _handColSize += _handSpeed / 100;
            //腕を伸ばす
            _syuSpr.size = new Vector2(1f, -_handLong);
            _armsCol.offset = new Vector2(0, -_handColSize / 2);
            _armsCol.size = new Vector2(0.5f, _handColSize);
        }
        _leader.transform.position
            = Vector2.MoveTowards(_leader.transform.position, _marimo.transform.position, _attractionSpeed / 100);
    }

    /// <summary>
    /// うで縮む
    /// </summary>
    private void HandShrink()
    {
        if (_handLong >= 0.01f || (_handLong >= 0.01f && _marimo.gameObject.tag == "Odor"))
        {
            _handLong -= _handSpeed / 100;
            _handColSize -= _handSpeed / 100;
            //腕を縮める
            _syuSpr.size = new Vector2(1f, -_handLong);
            _armsCol.offset = new Vector2(0, -_handColSize / 2);
            _armsCol.size = new Vector2(0.5f, _handColSize);
        }
        _leader.transform.position
            = Vector2.MoveTowards(_leader.transform.position, this.transform.position, _attractionSpeed / 100);
    }
}
