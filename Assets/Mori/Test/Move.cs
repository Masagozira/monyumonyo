using UnityEngine;

public class Move : MonoBehaviour
{
    //動かしたいオブジェクト
    [SerializeField,Header("動かしたいオブジェクト")]
    private GameObject _human;
    //オブジェクトが自動で動くスピード
    [SerializeField, Header("オブジェクトが動く速さ")]
    private int _speed = 5;
    //オブジェクトの目的地を保存
    private Vector3 _movePosition;

    //ランダムに動くオブジェクトの可動範囲
    [SerializeField, Header("ランダム可動域の右上限")]
    private float X_Max = 7f;
    [SerializeField, Header("ランダム可動域の左上限")]
    private float X_Min = -7f;
    [SerializeField, Header("ランダム可動域の上上限")]
    private float Y_Max = 4f;
    [SerializeField, Header("ランダム可動域の下上限")]
    private float Y_Min = -4f;

    void Start()
    {
        //オブジェクトの目的地を設定
        _movePosition = moveRandomPosition();
    }

    void Update()
    {
        //オブジェクトが目的地に到達したとき
        if (_movePosition == _human.transform.position)
        {
            //目的地を再設定
            _movePosition = moveRandomPosition();
        }
        //オブジェクトが目的地に移動
        this._human.transform.position 
            = Vector3.MoveTowards(_human.transform.position
                                , _movePosition
                                , _speed * Time.deltaTime);
    }
    /// <summary>
    /// 目的地を生成、xとyのポジションをランダムに値を取得
    /// </summary>
    /// <returns></returns>
    private Vector3 moveRandomPosition()
    {
        Vector3 randomPosi = new Vector3(Random.Range(X_Min, X_Max), Random.Range(Y_Min, Y_Max), Random.Range(0, 0));
        return randomPosi;
    }
}