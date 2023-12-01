using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    public GameObject Player;                   // 追いかけたいgameobject
    [SerializeField]
    public GameObject Enemy;                    // 追いかけるgameobject
    [SerializeField]
    public int _enemypos = 1;                   // エネミーの座標
    [SerializeField]
    public float speed = 2;                     // 通常歩行スピード
    [SerializeField]
    public float ChaseSpeed = 5f;               // 追いかけるスピード
    [SerializeField]
    public float Smelltime = 5f;                // 匂い時間（秒）

    public float RotationY = 180f;              //画像を左右反転させる数

    private float Smelltimer = 0.0f;                                        // 経過時間を格納するタイマー変数(初期値0秒)
    private bool Smellsdelicious = false;                                   // 美味しそうな匂い状態かどうかのフラグ
    private bool Smellsdisgusting = false;                                  // 不味そうな匂い状態かどうかのフラグ
    private bool Usual = false;                                             // 通常状態かどうかのフラグ
    private Vector2 pos;                                                    // 座標
    private string playertag = "Player";                                    // Playerの通常状態タグ
    private string playerSmellsdelicioustag = "playerSmellsdelicious";      // 2タグ用：Playerの美味しそうな匂いタグ(1を使うなら要らない)
    private string playerSmellsdisgustingtag = "playerSmellsdisgusting";    // 2タグ用：Playerの不味そうな匂い状態タグ(1を使うなら要らない)

    PlatformEffector2D _enemy;                                              // 貫通,当たり判定の名前


    private void Start()
    {

    }

    private void Update()
    {
        EnemyOnClickKey(); //キー入力呼び出し

        //美味しそうな匂い状フラグがTrueのときに毎フレーム実行
        if (Smellsdelicious)
        {
            Debug.Log("美味しそうな香り");

            //プレイヤーが存在してたら追いかける
            if (Player)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, ChaseSpeed * Time.deltaTime);

                //すり抜けさせない,PlatformEffector2Dをtrue（off）にする
                _enemy = Enemy.GetComponent<PlatformEffector2D>();
                _enemy.enabled = false;
            }

            //毎フレームタイマー変数にTime.deltaTimeを足す
            Smelltimer += Time.deltaTime;

            //タイマーが無敵時間(5秒)を超えたとき
            if (Smelltimer >= Smelltime)
            {
                Debug.Log("元に戻る");
                //無敵状態フラグをFalseにする
                Smellsdelicious = false;
                //タイマーを0.0秒にリセットする
                Smelltimer = 0.0f;
            }
        }

        //不味そうな匂い状フラグがTrueのときに毎フレーム実行
        if (Smellsdisgusting)
        {
            Debug.Log("不味そうな香り");
            //不味そうな香りを出して貫通アクション
            //すり抜けさせる,PlatformEffector2Dをtrue（on）にする
            _enemy = Enemy.GetComponent<PlatformEffector2D>();
            _enemy.enabled = true;

            //毎フレームタイマー変数にTime.deltaTimeを足す
            Smelltimer += Time.deltaTime;
            //タイマーが無敵時間(5秒)を超えたとき
            if (Smelltimer >= Smelltime)
            {
                Debug.Log("元に戻る");
                //無敵状態フラグをFalseにする
                Smellsdisgusting = false;
                //タイマーを0.0秒にリセットする
                Smelltimer = 0.0f;
            }

        }

        //通常状態
        if (Usual)
        {
            //基本歩行
            pos = transform.position;
            // transformを取得
            Transform myTransform = this.transform;

            //敵の左右移動
            transform.Translate(transform.right * Time.deltaTime * speed * _enemypos);
            if (pos.x > 1.5)
            {
                _enemypos = -1;

                //画像回転
                myTransform.Rotate(0f, RotationY, 0f); 
            }
            if (pos.x < -1.5)
            {
                _enemypos = 1;

                //画像回転
                myTransform.Rotate(0f, -RotationY, 0f);
            }
        }

    }

    //バグ防止の画像の固定化
    public void Rotation()
    {
        if (RotationY <= 180)
        {
            RotationY = 180;
        }

        if (RotationY <= 0)
        {
            RotationY = 0;
        }
    }


    //プレイヤー操作キー入力
    public void EnemyOnClickKey()
    {
        //上　UpKeyが押されたらinvokeを呼び出す
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Debug.Log("美味しいそうな匂い");
            //ボタンを押されたらフラグをTrueにする
            Smellsdelicious = true;
            //ボタンを押されたらフラグをfalseにする
            //不味そうな匂いと美味しそうな匂いを重ねて使えない様にするため
            Smellsdisgusting = false;

        }
        //下　DownKeyが押されたらinvokeを呼び出さなくする
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Debug.Log("まずそうな匂い");
            //ボタンを押されたらフラグをTrueにする
            Smellsdisgusting = true;
            //ボタンを押されたらフラグをfalseにする
            //不味そうな匂いと美味しそうな匂いを重ねて使えない様にするため
            Smellsdelicious = false;

        }

        else //上下以外のボタンを押してないとき
        {
            //Debug.Log("お腹すいた");
            //すり抜けさせない,PlatformEffector2Dをtrue（off）にする
            Usual = true;
            _enemy = Enemy.GetComponent<PlatformEffector2D>();
            _enemy.enabled = false;
        }

    }

    //プレイヤーに当たったら生きるか死ぬか
    void OnCollisionEnter2D(Collision2D collision)
    {
        //１
        if (collision.collider.tag == playertag)
        {
            if (_enemy.enabled == true)
            {
                Debug.Log("seef");      //何も起こらない
            }
            if (_enemy.enabled == false)
            {
                Debug.Log("hit");
                Destroy(Player, 0.2f);  //プレイヤー死ぬ
            }
        }

        ////２タグバージョンを使うなら１をコメントアウトか消去
        ////美味しそうな匂いタグ
        //if (collision.collider.tag == playerSmellsdelicioustag)
        //{
        //    Debug.Log("hit");
        //    Destroy(Player, 0.2f);  //プレイヤー死ぬ
        //}
        ////不味そうな匂いタグ
        //if (collision.collider.tag == playerSmellsdisgustingtag)
        //{
        //    Debug.Log("seef");      //何も起こらない
        //}
        ////通常状態の匂いタグ
        //if (collision.collider.tag == playertag)
        //{
        //    Debug.Log("hit");
        //    Destroy(Player, 0.2f);  //プレイヤー死ぬ
        //}
    }

    //探索範囲,範囲内に入ったら追いかける
    void OnTriggerStay2D(Collider2D collision)
    {
        //1
        if (collision.gameObject.tag == playertag)
        {
            
            //不味そうな匂いの時は追いかけない
            if (Smellsdisgusting == true)
            {

            }
            //それ以外は追いかける
            else
            {
                //Debug.Log("hitヒット");
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, ChaseSpeed * Time.deltaTime);
            }
        }

        ////２タグバージョンを使うなら１をコメントアウトか消去
        ////美味しそうな匂いタグ
        //if (collision.gameObject.tag == playerSmellsdelicioustag)
        //{
        //    //Debug.Log("hitヒット");
        //    //transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, ChaseSpeed * Time.deltaTime);
        //}
        ////不味そうな匂いタグ
        //if (collision.gameObject.tag == playerSmellsdisgustingtag)
        //{
        //    Debug.Log("seef");      //何も起こらない
        //}
        ////通常状態の匂いタグ
        //if (collision.gameObject.tag == playertag)
        //{
        //    //Debug.Log("hitヒット");
        //    //transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, ChaseSpeed * Time.deltaTime);
        //}
    }

}
 