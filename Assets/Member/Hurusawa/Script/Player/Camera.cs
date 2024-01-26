using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target; // 追従するオブジェクト
    public Vector3 offset; // 追加の位置オフセット

    void Update()
    {
        // 追加の位置オフセットを加えてカメラの位置を調整
        Vector3 desiredPosition = target.position + offset;
        transform.position = new Vector3(desiredPosition.x, desiredPosition.y, transform.position.z);

        // カメラの回転を固定（Z軸回りの回転を固定）
        transform.rotation = Quaternion.Euler(0, 0, 0); // カメラがZ軸を基準に回転しないようにする
    }
}
