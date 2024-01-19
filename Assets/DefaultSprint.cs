using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSprint : MonoBehaviour
{
    //position,rotation,scaleを配列で保存して後で保存したものを呼び出す
    private List<Vector3> _defaultBonePosition = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        //positionを配列で保存
        Transform[] _arrT = GetComponentsInChildren<Transform>();
        foreach (Transform t in _arrT)
        {
            _defaultBonePosition.Add(t.localPosition);
        }
    }

    public void SetDafaultTransform()
    {
        //呼び出す関数
    }
}
/*
 RigidBody:Dinamic->Kinematic
 Collider:無効化
 全てのSpringjoint:無効化
 上から順に処理する
 */