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
    [SerializeField, Header("プレイヤーのTransform取得")]
    private Transform _targetTra;
    //ターゲットのRigidbody2D
    [SerializeField, Header("プレイヤーのRigidBody取得")]
    private Rigidbody2D _targetRig;
    //プレイヤータグ取得用&引っ張り用
    [SerializeField, Header("プレイヤー")]
    private GameObject _marimo;
    //プレイヤー操作のスクリプト
    [SerializeField, Header("プレイヤーの操作スクリプト取得")]
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
    [SerializeField,Header("腕がプレイヤーに向かって伸びる速さ")]
    private float _handTime = 30;
    //腕ののびる速度：代入用
    private float _handLong = 0.5f;
    //腕ののびる速度：Collider代入用
    private float _handColSize = 0.4f;
    //手を向かせたい方向
    private Vector3 dir;
    //プレイヤーを今掴んでいるか
    private bool _chaching = false;
    //引き寄せられるプレイヤーの速さ
    [SerializeField,Header("引き寄せる速さ")]
    private float _attractionSpeed = 1f;

    /// <summary>
    /// 初期設定
    /// ・うでのSpriteRendererとBoxCollider2Dの取得
    /// </summary>
    private void Start()
    {
        _syuSpr = this.GetComponent<SpriteRenderer>();
        _armsCol = this.GetComponent<BoxCollider2D>();
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
        else if (_chaching==true)
        {
            HandShrink();
        }

        //bool確認
        Debug.Log("Playerが索敵範囲内にいる：" + _isHandsMove + "," + "掴んでいる：" + _chaching);

        //まずい匂いになった時
        if (_marimo.gameObject.tag == "Odor")
        {
            HandShrink();
            _targetRig.bodyType = RigidbodyType2D.Dynamic;
            _chaching = false;
            _marimoScr.enabled = true;
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
            Debug.Log("キャッチ");
            _chaching = true;
            _targetRig.bodyType = RigidbodyType2D.Kinematic;
            _marimoScr.enabled = false;
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

        //FromToRotation
        //Vector3 vector3 = _targetTra.position - this.transform.position;
        //Quaternion quaternion = Quaternion.LookRotation(vector3);
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, _targetTra.transform.rotation, _speed);
    }

    /// <summary>
    /// うで伸びる
    /// </summary>
    private void HandStretch()
    {
        if (_handLong>-_syuHandHeightRim)
        {
            _handLong += _handTime / 100;
            _handColSize += _handTime / 100;
            //腕を伸ばす
            _syuSpr.size = new Vector2(1f, -_handLong);
            _armsCol.offset = new Vector2(0, -_handColSize / 2);
            _armsCol.size = new Vector2(0.5f, _handColSize);
        }
    }

    /// <summary>
    /// うで縮む
    /// </summary>
    private void HandShrink()
    {
        if(_handLong >= 0.01f||(_handLong >= 0.01f&&_marimo.gameObject.tag == "Odor"))
        {
            _handLong -= _handTime / 100;
            _handColSize -= _handTime / 100;
            //腕を縮める
            _syuSpr.size = new Vector2(1f, -_handLong);
            _armsCol.offset = new Vector2(0, -_handColSize / 2);
            _armsCol.size = new Vector2(0.5f, _handColSize);
        }
        _marimo.transform.position 
            = Vector3.MoveTowards(_marimo.transform.position, this.transform.position, _attractionSpeed/100);
    }

}

/*
変更点
 private MARIMO _marimoScr;　から　private PlayerMovement _marimoScr;に変更
*/