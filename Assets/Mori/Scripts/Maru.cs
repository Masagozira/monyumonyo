using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//まずい匂いの時プレイヤーをスルーする処理
//レイヤーで判定、Odorはすり抜ける
//gameObject.layer = LayerMask.NameToLayer("Odor");

/// <summary>
/// マルモルの歩行、追跡のスクリプト
/// </summary>
public class Maru : MonoBehaviour
{
    //追いかけたいgameobject
    [SerializeField,Header("プレイヤー")]
    private GameObject _player;
    //通常歩行スピード
    [SerializeField,Header("通常時の歩行スピード")]
    private float _maruspeed = 2;
    //通常歩行範囲
    [SerializeField, Header("通常時の歩行範囲")]
    private float _maruArea = 5;
    //歩行用、増加させて_maruAreaの値になったら引き返す
    private float _maruAreaAdd = 0;
    //追いかけるスピード
    [SerializeField,Header("プレイヤー発見時の歩行スピード")]
    private float _angrySpeed = 5f;
    //プレイヤーが索敵範囲内にいるか：全体
    [Header("索敵範囲内にプレイヤーがいるか、居る：true")]
    public bool _playerHere = false;
    //プレイヤーが索敵範囲内にいるか：前方
    [Header("前方索敵範囲内にプレイヤーがいるか、居る：true")]
    public bool _playerFrontHere = false;

    //向きの変更
    private int _direction = 1;
    private Vector3 _scare;

    private bool _playerDeath;
    private CircleCollider2D _playerCol;

    [SerializeField, Header("プレイヤーが死ぬときのSE")]
    private AudioClip _deathSe;
    AudioSource audioSource;

    private void Start()
    {
        //初期設定：向き
        _scare = this.transform.localScale;
        _playerCol = _player.GetComponent<CircleCollider2D>();
        _playerDeath = false;
        audioSource = GetComponent<AudioSource>();
    }
    
    /// <summary>
    /// 匂いや索敵範囲で判定し追跡または移動する
    /// </summary>
    private void Update()
    {
        //プレイヤーが索敵範囲内に存在してたら追いかける
        //・美味しそうな匂いかつ索敵範囲内
        //・通常時かつ前方索敵範囲内
        if (_player.tag == "Florus" && (_playerHere == true || _playerFrontHere == true)
            || _player.tag == "Player" && _playerFrontHere == true)
        {
            ChaseCase();
        }
        //通常時、まずそうな匂い、それ以外
        else
        {
            NonCase();
        }

        Debug.Log("プレイヤーが近くにいる : " + _playerHere +" " + "プレイヤーが前にいる : " + _playerFrontHere);
        Debug.Log(_playerDeath);
    }

    /// <summary>
    /// 追尾歩行、プレイヤーを追いかける
    /// </summary>
    private void ChaseCase()
    {
        Debug.Log("Case1:Chase");

        //プレイヤーに向かって移動
        transform.position
            = Vector2.MoveTowards(this.transform.position
            , _player.transform.position
            , _angrySpeed * Time.deltaTime);
        //もしプレイヤー.xが自分より+だったら
        if(this.transform.position.x < _player.transform.position.x)
        {
            //右向き
            _scare.x = -1;
            this.transform.localScale = _scare;
        }
        //もしプレイヤー.xが自分より-だったら
        else
        {
            //左向き
            _scare.x = 1;
            this.transform.localScale = _scare;
        }
    }

    /// <summary>
    /// 通常歩行
    /// </summary>
    private void NonCase()
    {
        Debug.Log("Case2:NotFind");
        //レイヤーで判定、Odorはすり抜ける
        //gameObject.layer = LayerMask.NameToLayer("Odor");

        //基本歩行
        //変数に値を入れ続ける
        _maruAreaAdd += Time.deltaTime * 1.4f;
        //変数が一定値に達したらリセット、向き反転
        if(_maruAreaAdd >= _maruArea)
        {
            _maruAreaAdd = 0;
            _direction *= -1;
            //歩行向きが+なら右向き
            if(_direction == 1)
            {
                _scare.x = -1;
                this.transform.localScale = _scare;
            }
            //歩行向きが-なら左向き
            else
            {
                _scare.x = 1;
                this.transform.localScale = _scare;
            }
        }
        //歩き続ける
        transform.position 
            = new Vector3(transform.position.x + _maruspeed * Time.fixedDeltaTime * _direction
            , this.transform.position.y
            , 0);
    }

    ///// <summary>
    ///// デバッグ用
    ///// </summary>
    ///// <param name="collision"></param>
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "Player") Debug.LogWarning("Debug : Hit,OK");
    //    if (collision.collider.tag == "Odor") Debug.LogError("Debug : Hit,ERROR");
    //}

    /// <summary>
    /// プレイヤーが当たった時
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_playerDeath == false && collision.collider == _playerCol)
        {
            Debug.LogWarning("HitBody");
            _playerDeath = true;
            audioSource.PlayOneShot(_deathSe);
            Invoke("ReDeath", 10f);
        }
    }

    private void ReDeath()
    {
        _playerDeath = false;
        Debug.Log("ReDeath");
    }
}
