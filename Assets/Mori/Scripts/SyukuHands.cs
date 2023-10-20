using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyukuHands : MonoBehaviour
{
    //自身のTransform
    [SerializeField]
    private Transform _armsTra;

    //ターゲットのTransform
    [SerializeField]
    private Transform _target;

    //プレイヤータグ取得用
    [SerializeField]
    private GameObject _marimo;

    //手を向かせたい方向
    private Vector3 dir;

    // Update is called once per frame
    void Update()
    {
        HandsMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AMAI" || collision.gameObject.tag == "NORMAL")
        {
            //プレイヤーを子オブジェクトにする、ツタにくっつける
            collision.gameObject.transform.parent = this.gameObject.transform;
            Debug.Log("キャッチ");
        }
        else if (collision.gameObject.tag == "MAZUI")
        {
            //プレイヤー子オブジェクト解除、ツタから離す
            collision.gameObject.transform.parent = null;
            Debug.Log("触れない");
        }
    }

    private void HandsMove()
    {
        //向きたい方向を計算
        dir = (_target.position - _armsTra.position);
        //向きたい方向に回転
        _armsTra.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }

}

/*
void Update()
{
    //向きたい方向を計算
    dir = (_target.position - _armsTra.position);
    //向きたい方向に回転
    _armsTra.rotation = Quaternion.FromToRotation(Vector3.up, dir);

}
void LateUpdate()
{
    transform.position
        = Vector3.Lerp(transform.position
        , _target.transform.position + dir
        , 6.0f * Time.deltaTime);
}
*/