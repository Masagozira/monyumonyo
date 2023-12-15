using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maru : MonoBehaviour
{
    // �ǂ���������gameobject
    [SerializeField,Header("�v���C���[")]
    private GameObject _player;
    // �ʏ���s�X�s�[�h
    [SerializeField,Header("�ʏ펞�̕��s�X�s�[�h")]
    private float _maruspeed = 2;
    // �ǂ�������X�s�[�h
    [SerializeField,Header("�v���C���[�������̕��s�X�s�[�h")]
    private float _angrySpeed = 5f;
    //�v���C���[�����G�͈͓��ɂ��邩�F�S��
    [Header("���G�͈͓��Ƀv���C���[�����邩�A����Ftrue")]
    public bool _playerHere = false;
    //�v���C���[�����G�͈͓��ɂ��邩�F�O��
    [Header("�O�����G�͈͓��Ƀv���C���[�����邩�A����Ftrue")]
    public bool _playerFrontHere = false;

    //���g��Collider2D
    [SerializeField]
    private CircleCollider2D _maruCol;

    private void Start()
    {
        _maruCol = this.GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        //�����������ȓ����̂Ƃ��Ɏ��s
        //�v���C���[�����G�͈͓��ɑ��݂��Ă���ǂ�������
        if (_player.tag == "Florus" && (_playerHere == true || _playerFrontHere == true)
            || _player.tag == "Player" && _playerFrontHere == true)
        {
            Debug.Log("Case1:Chase");
            //�R���C�_�[��IsTrigger��False�ɂ���
            _maruCol.isTrigger = false;
            //�v���C���[�Ɍ������Ĉړ�
            transform.position
                = Vector2.MoveTowards(transform.position
                , _player.transform.position
                , _angrySpeed * Time.deltaTime);
        }

        //�s�������ȓ�����t���O��True�̂Ƃ��ɖ��t���[�����s
        else if (_player.tag == "Odor")
        {
            Debug.Log("Case2:Odor");
            //�R���C�_�[��IsTrigger��True�ɂ���
            _maruCol.isTrigger = false;
            //��{���s
            var pos = transform.position;

            //�G�̍��E�ړ�
            transform.Translate(pos * _maruspeed * Time.deltaTime);
            if (pos.x > 1.5)
            {
                this.transform.position = new Vector3(-1f, this.transform.position.y, 0);
            }
            if (pos.x < -1.5)
            {
                this.transform.position = new Vector3(1f, this.transform.position.y, 0);
            }
        }
        else
        {
            Debug.Log("Case3:Normal");
            //�R���C�_�[��IsTrigger��False�ɂ���
            _maruCol.isTrigger = false;
            //��{���s
            var pos = transform.position;

            //�G�̍��E�ړ�
            transform.Translate(pos * _maruspeed * Time.deltaTime);
            if (pos.x > 1.5)
            {
                this.transform.position = new Vector3(-1f, 0, 0);
            }
            if (pos.x < -1.5)
            {
                this.transform.position = new Vector3(1f, 0, 0);
            }
        }
    }
}
