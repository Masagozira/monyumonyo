using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float Speed = 3f;   //プレイヤーのスピード

    void Update()
    {
        PlayerOnClickKey();
    }

    //プレイヤー操作キー入力
    public void PlayerOnClickKey()
    {
        //右移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Speed * transform.right * Time.deltaTime;
        }
        //左移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {      
            transform.position -= Speed * transform.right * Time.deltaTime;
        }
        //上
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Debug.Log("美味しい");
        }
        //下
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Debug.Log("まずい");        
        }
        else
        {
           // Debug.Log("通常");
        }

    }

}
