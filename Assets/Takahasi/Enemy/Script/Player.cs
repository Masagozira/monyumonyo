using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float Speed = 3f;   //�v���C���[�̃X�s�[�h

    void Update()
    {
        PlayerOnClickKey();
    }

    //�v���C���[����L�[����
    public void PlayerOnClickKey()
    {
        //�E�ړ�
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Speed * transform.right * Time.deltaTime;
        }
        //���ړ�
        if (Input.GetKey(KeyCode.LeftArrow))
        {      
            transform.position -= Speed * transform.right * Time.deltaTime;
        }
        //��
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Debug.Log("��������");
        }
        //��
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Debug.Log("�܂���");        
        }
        else
        {
           // Debug.Log("�ʏ�");
        }

    }

}
