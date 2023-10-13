using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyukuHands : MonoBehaviour
{
    //自身のTransform
    [SerializeField] 
    private Transform _armsTra;

    //ターゲットのTransform
    [SerializeField] private Transform _target;

    private Vector3 dir;
    private Vector3 dir2;

    // Update is called once per frame
    void Update()
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