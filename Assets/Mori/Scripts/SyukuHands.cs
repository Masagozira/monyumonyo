using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyukuHands : MonoBehaviour
{
    //���g��Transform
    [SerializeField]
    private Transform _armsTra;
    //���g��Collider2D
    [SerializeField]
    private BoxCollider2D _armsCol;
    //�^�[�Q�b�g��Transform
    [SerializeField]
    private Transform _targetTra;
    //�^�[�Q�b�g��Rigidbody2D
    [SerializeField]
    private Rigidbody2D _targetRig;
    //�v���C���[�^�O�擾�p&��������p
    [SerializeField]
    private GameObject _marimo;
    //�����������������
    private Vector3 dir;
    //�r�����������ɂ��邩
    public bool _isHandsMove = false;
    //�r��L�΂����߂�SpriteRenderer�擾
    private SpriteRenderer _syuSpr;
    [SerializeField, Header("�r�̒������E�l")]
    private float _syuHandHeightRim = 10f;
    [SerializeField,Header("�r���v���C���[�Ɍ������ĐL�т鑬�x")]
    private float _handTime = 30;
    private float _handLong;
    private float _handColSize;
    //�v���C���[�����͂�ł��邩
    private bool _chaching = false;
    [SerializeField,Header("�����񂹃X�s�[�h")]
    private float _attractionSpeed = 1f;
    //�v���C���[����̃X�N���v�g
    [SerializeField]
    private MARIMO _marimoScr;
    private bool _chachCoolTime = false;

    private void Start()
    {
        _syuSpr = this.GetComponent<SpriteRenderer>();
        _armsCol = this.GetComponent<BoxCollider2D>();
        _syuSpr.drawMode = SpriteDrawMode.Tiled;
        _handLong += 3.96f;
        _handColSize += 3.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_marimo.gameObject.tag == "Florus" && _isHandsMove == true && _chaching == false)
        {
            HandsMove();
            HandStretch();
        }
        //else if (_chaching == true && _handLong > 4
        //|| _marimo.gameObject.tag == "Odor"
        //|| _marimo.gameObject.tag == "Player")
        //{
        //    HandShrink();
        //}
        else if (_chaching==true)
        {
            HandShrink();
        }

        Debug.Log("Player�����G�͈͓��ɂ���F" + _isHandsMove + "," + "�͂�ł���F" + _chaching);

        if (_marimo.gameObject.tag == "Odor")
        {
            //�v���C���[�q�I�u�W�F�N�g�����A�c�^���痣��
            _marimo.gameObject.transform.parent = null;
            Debug.Log("�G��Ȃ�");
            _targetRig.bodyType = RigidbodyType2D.Dynamic;
            _chaching = false;
            _marimoScr.enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Florus" || collision.gameObject.tag == "Player")
        {
            //�v���C���[���q�I�u�W�F�N�g�ɂ���A�c�^�ɂ�������
            collision.gameObject.transform.parent = this.gameObject.transform;
            Debug.Log("�L���b�`");
            _chaching = true;
            _targetRig.bodyType = RigidbodyType2D.Kinematic;
            _marimoScr.enabled = false;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _targetRig.gravityScale = 1;
        _chaching = false;
    }

    private void HandsMove()
    {
        //���������������v�Z
        dir = (_targetTra.position - _armsTra.position);
        //�������������ɉ�]
        _armsTra.rotation = Quaternion.FromToRotation(Vector3.up, -dir);

        //Vector3 vector3 = _targetTra.position - this.transform.position;
        //Quaternion quaternion = Quaternion.LookRotation(vector3);
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, _targetTra.transform.rotation, _speed);
    }
    private void HandStretch()
    {
        if (_handLong>-_syuHandHeightRim)
        {
            _handLong += _handTime / 100;
            _handColSize += _handTime / 100;
            //�r��L�΂�
            _syuSpr.size = new Vector2(3.8f, -_handLong);
            _armsCol.offset = new Vector2(0, -_handColSize / 2);
            _armsCol.size = new Vector2(3.8f, _handColSize);
        }
    }
    private void HandShrink()
    {
        if(_handLong >= 3.96f)
        {
            _handLong -= _handTime / 100;
            _handColSize -= _handTime / 100;
            //�r���k�߂�
            _syuSpr.size = new Vector2(3.8f, -_handLong);
            _armsCol.offset = new Vector2(0, -_handColSize / 2);
            _armsCol.size = new Vector2(3.8f, _handColSize);
        }
        _marimo.transform.position 
            = Vector3.MoveTowards(_marimo.transform.position, this.transform.position, _attractionSpeed/100);
    }
    private void SwitchChanging()
    {
        _chaching = false;
        Debug.Log("Yobareta:Changing");
    }
    //private void ChachCoolTime()
    //{
    //    _chachCoolTime = false;
    //    Debug.Log("Yobareta:CoolTime");
    //}
}
