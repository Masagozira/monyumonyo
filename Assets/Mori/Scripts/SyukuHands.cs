using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyukuHands : MonoBehaviour
{
    //���g��Transform
    [SerializeField]
    private Transform _armsTra;

    //�^�[�Q�b�g��Transform
    [SerializeField]
    private Transform _target;

    //�v���C���[�^�O�擾�p
    [SerializeField]
    private GameObject _marimo;

    //�����������������
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
            //�v���C���[���q�I�u�W�F�N�g�ɂ���A�c�^�ɂ�������
            collision.gameObject.transform.parent = this.gameObject.transform;
            Debug.Log("�L���b�`");
        }
        else if (collision.gameObject.tag == "MAZUI")
        {
            //�v���C���[�q�I�u�W�F�N�g�����A�c�^���痣��
            collision.gameObject.transform.parent = null;
            Debug.Log("�G��Ȃ�");
        }
    }

    private void HandsMove()
    {
        //���������������v�Z
        dir = (_target.position - _armsTra.position);
        //�������������ɉ�]
        _armsTra.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }

}

/*
void Update()
{
    //���������������v�Z
    dir = (_target.position - _armsTra.position);
    //�������������ɉ�]
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