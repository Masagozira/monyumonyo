using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyukuHands : MonoBehaviour
{
    //自身のTransform
    [SerializeField]
    private Transform _armsTra;
    //自身のCollider2D
    [SerializeField]
    private BoxCollider2D _armsCol;
    //ターゲットのTransform
    [SerializeField]
    private Transform _targetTra;
    //ターゲットのRigidbody2D
    [SerializeField]
    private Rigidbody2D _targetRig;
    //プレイヤータグ取得用&引っ張り用
    [SerializeField]
    private GameObject _marimo;
    //手を向かせたい方向
    private Vector3 dir;
    //腕が動く条件にあるか
    public bool _isHandsMove = false;
    //腕を伸ばすためのSpriteRenderer取得
    private SpriteRenderer _syuSpr;
    [SerializeField, Header("腕の長さ限界値")]
    private float _syuHandHeightRim = 10f;
    [SerializeField,Header("腕がプレイヤーに向かって伸びる速度")]
    private float _handTime = 30;
    private float _handLong;
    private float _handColSize;
    //プレイヤーを今掴んでいるか
    private bool _chaching = false;
    [SerializeField,Header("引き寄せスピード")]
    private float _attractionSpeed = 1f;
    //プレイヤー操作のスクリプト
    [SerializeField]
    private MARIMO _marimoScr;
    private bool _chachCoolTime = false;

    private void Start()
    {
        _syuSpr = this.GetComponent<SpriteRenderer>();
        _armsCol = this.GetComponent<BoxCollider2D>();
        _syuSpr.drawMode = SpriteDrawMode.Tiled;
        _handLong += 3.96f;
        _handColSize += 3.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_marimo.gameObject.tag == "Florus" && _isHandsMove == true && _chaching == false)
        {
            HandsMove();
            HandStretch();
        }
        //else if (_chaching == true && _handLong > 4
        //|| _marimo.gameObject.tag == "Odor"
        //|| _marimo.gameObject.tag == "Player")
        //{
        //    HandShrink();
        //}
        else if (_chaching==true)
        {
            HandShrink();
        }

        Debug.Log("Playerが索敵範囲内にいる：" + _isHandsMove + "," + "掴んでいる：" + _chaching);

        if (_marimo.gameObject.tag == "Odor")
        {
            //プレイヤー子オブジェクト解除、ツタから離す
            _marimo.gameObject.transform.parent = null;
            Debug.Log("触れない");
            _targetRig.bodyType = RigidbodyType2D.Dynamic;
            _chaching = false;
            _marimoScr.enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Florus" || collision.gameObject.tag == "Player")
        {
            //プレイヤーを子オブジェクトにする、ツタにくっつける
            collision.gameObject.transform.parent = this.gameObject.transform;
            Debug.Log("キャッチ");
            _chaching = true;
            _targetRig.bodyType = RigidbodyType2D.Kinematic;
            _marimoScr.enabled = false;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _targetRig.gravityScale = 1;
        _chaching = false;
    }

    private void HandsMove()
    {
        //向きたい方向を計算
        dir = (_targetTra.position - _armsTra.position);
        //向きたい方向に回転
        _armsTra.rotation = Quaternion.FromToRotation(Vector3.up, -dir);

        //Vector3 vector3 = _targetTra.position - this.transform.position;
        //Quaternion quaternion = Quaternion.LookRotation(vector3);
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, _targetTra.transform.rotation, _speed);
    }
    private void HandStretch()
    {
        if (_handLong>-_syuHandHeightRim)
        {
            _handLong += _handTime / 100;
            _handColSize += _handTime / 100;
            //腕を伸ばす
            _syuSpr.size = new Vector2(3.8f, -_handLong);
            _armsCol.offset = new Vector2(0, -_handColSize / 2);
            _armsCol.size = new Vector2(3.8f, _handColSize);
        }
    }
    private void HandShrink()
    {
        if(_handLong >= 3.96f)
        {
            _handLong -= _handTime / 100;
            _handColSize -= _handTime / 100;
            //腕を縮める
            _syuSpr.size = new Vector2(3.8f, -_handLong);
            _armsCol.offset = new Vector2(0, -_handColSize / 2);
            _armsCol.size = new Vector2(3.8f, _handColSize);
        }
        _marimo.transform.position 
            = Vector3.MoveTowards(_marimo.transform.position, this.transform.position, _attractionSpeed/100);
    }
    private void SwitchChanging()
    {
        _chaching = false;
        Debug.Log("Yobareta:Changing");
    }
    //private void ChachCoolTime()
    //{
    //    _chachCoolTime = false;
    //    Debug.Log("Yobareta:CoolTime");
    //}
}
