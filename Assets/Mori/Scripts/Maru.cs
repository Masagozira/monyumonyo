using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maru : MonoBehaviour
{
    // 追いかけたいgameobject
    [SerializeField,Header("プレイヤー")]
    private GameObject _player;
    // 通常歩行スピード
    [SerializeField,Header("通常時の歩行スピード")]
    private float _maruspeed = 2;
    // 追いかけるスピード
    [SerializeField,Header("プレイヤー発見時の歩行スピード")]
    private float _angrySpeed = 5f;
    //プレイヤーが索敵範囲内にいるか：全体
    [Header("索敵範囲内にプレイヤーがいるか、居る：true")]
    public bool _playerHere = false;
    //プレイヤーが索敵範囲内にいるか：前方
    [Header("前方索敵範囲内にプレイヤーがいるか、居る：true")]
    public bool _playerFrontHere = false;

    //自身のCollider2D
    [SerializeField]
    private CircleCollider2D _maruCol;

    private void Start()
    {
        _maruCol = this.GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        //美味しそうな匂いのときに実行
        //プレイヤーが索敵範囲内に存在してたら追いかける
        if (_player.tag == "Florus" && (_playerHere == true || _playerFrontHere == true)
            || _player.tag == "Player" && _playerFrontHere == true)
        {
            Debug.Log("Case1:Chase");
            //コライダーのIsTriggerをFalseにする
            _maruCol.isTrigger = false;
            //プレイヤーに向かって移動
            transform.position
                = Vector2.MoveTowards(transform.position
                , _player.transform.position
                , _angrySpeed * Time.deltaTime);
        }

        //不味そうな匂い状フラグがTrueのときに毎フレーム実行
        else if (_player.tag == "Odor")
        {
            Debug.Log("Case2:Odor");
            //コライダーのIsTriggerをTrueにする
            _maruCol.isTrigger = false;
            //基本歩行
            var pos = transform.position;

            //敵の左右移動
            transform.Translate(pos * _maruspeed * Time.deltaTime);
            if (pos.x > 1.5)
            {
                this.transform.position = new Vector3(-1f, this.transform.position.y, 0);
            }
            if (pos.x < -1.5)
            {
                this.transform.position = new Vector3(1f, this.transform.position.y, 0);
            }
        }
        else
        {
            Debug.Log("Case3:Normal");
            //コライダーのIsTriggerをFalseにする
            _maruCol.isTrigger = false;
            //基本歩行
            var pos = transform.position;

            //敵の左右移動
            transform.Translate(pos * _maruspeed * Time.deltaTime);
            if (pos.x > 1.5)
            {
                this.transform.position = new Vector3(-1f, 0, 0);
            }
            if (pos.x < -1.5)
            {
                this.transform.position = new Vector3(1f, 0, 0);
            }
        }
    }
}
