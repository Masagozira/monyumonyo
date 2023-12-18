using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�܂��������̎��v���C���[���X���[���鏈��
//���C���[�Ŕ���AOdor�͂��蔲����
//gameObject.layer = LayerMask.NameToLayer("Odor");

/// <summary>
/// �}�������̕��s�A�ǐՂ̃X�N���v�g
/// </summary>
public class Maru : MonoBehaviour
{
    //�ǂ���������gameobject
    [SerializeField,Header("�v���C���[")]
    private GameObject _player;
    //�ʏ���s�X�s�[�h
    [SerializeField,Header("�ʏ펞�̕��s�X�s�[�h")]
    private float _maruspeed = 2;
    //�ʏ���s�͈�
    [SerializeField, Header("�ʏ펞�̕��s�͈�")]
    private float _maruArea = 5;
    //���s�p�A����������_maruArea�̒l�ɂȂ���������Ԃ�
    private float _maruAreaAdd = 0;
    //�ǂ�������X�s�[�h
    [SerializeField,Header("�v���C���[�������̕��s�X�s�[�h")]
    private float _angrySpeed = 5f;
    //�v���C���[�����G�͈͓��ɂ��邩�F�S��
    [Header("���G�͈͓��Ƀv���C���[�����邩�A����Ftrue")]
    public bool _playerHere = false;
    //�v���C���[�����G�͈͓��ɂ��邩�F�O��
    [Header("�O�����G�͈͓��Ƀv���C���[�����邩�A����Ftrue")]
    public bool _playerFrontHere = false;

    //�����̕ύX
    private int _direction = 1;
    private Vector3 _scare;

    private bool _playerDeath;
    private CircleCollider2D _playerCol;

    [SerializeField, Header("�v���C���[�����ʂƂ���SE")]
    private AudioClip _deathSe;
    AudioSource audioSource;

    private void Start()
    {
        //�����ݒ�F����
        _scare = this.transform.localScale;
        _playerCol = _player.GetComponent<CircleCollider2D>();
        _playerDeath = false;
        audioSource = GetComponent<AudioSource>();
    }
    
    /// <summary>
    /// ��������G�͈͂Ŕ��肵�ǐՂ܂��͈ړ�����
    /// </summary>
    private void Update()
    {
        //�v���C���[�����G�͈͓��ɑ��݂��Ă���ǂ�������
        //�E�����������ȓ��������G�͈͓�
        //�E�ʏ펞���O�����G�͈͓�
        if (_player.tag == "Florus" && (_playerHere == true || _playerFrontHere == true)
            || _player.tag == "Player" && _playerFrontHere == true)
        {
            ChaseCase();
        }
        //�ʏ펞�A�܂������ȓ����A����ȊO
        else
        {
            NonCase();
        }

        Debug.Log("�v���C���[���߂��ɂ��� : " + _playerHere +" " + "�v���C���[���O�ɂ��� : " + _playerFrontHere);
        Debug.Log(_playerDeath);
    }

    /// <summary>
    /// �ǔ����s�A�v���C���[��ǂ�������
    /// </summary>
    private void ChaseCase()
    {
        Debug.Log("Case1:Chase");

        //�v���C���[�Ɍ������Ĉړ�
        transform.position
            = Vector2.MoveTowards(this.transform.position
            , _player.transform.position
            , _angrySpeed * Time.deltaTime);
        //�����v���C���[.x���������+��������
        if(this.transform.position.x < _player.transform.position.x)
        {
            //�E����
            _scare.x = -1;
            this.transform.localScale = _scare;
        }
        //�����v���C���[.x���������-��������
        else
        {
            //������
            _scare.x = 1;
            this.transform.localScale = _scare;
        }
    }

    /// <summary>
    /// �ʏ���s
    /// </summary>
    private void NonCase()
    {
        Debug.Log("Case2:NotFind");
        //���C���[�Ŕ���AOdor�͂��蔲����
        //gameObject.layer = LayerMask.NameToLayer("Odor");

        //��{���s
        //�ϐ��ɒl����ꑱ����
        _maruAreaAdd += Time.deltaTime * 1.4f;
        //�ϐ������l�ɒB�����烊�Z�b�g�A�������]
        if(_maruAreaAdd >= _maruArea)
        {
            _maruAreaAdd = 0;
            _direction *= -1;
            //���s������+�Ȃ�E����
            if(_direction == 1)
            {
                _scare.x = -1;
                this.transform.localScale = _scare;
            }
            //���s������-�Ȃ獶����
            else
            {
                _scare.x = 1;
                this.transform.localScale = _scare;
            }
        }
        //����������
        transform.position 
            = new Vector3(transform.position.x + _maruspeed * Time.fixedDeltaTime * _direction
            , this.transform.position.y
            , 0);
    }

    ///// <summary>
    ///// �f�o�b�O�p
    ///// </summary>
    ///// <param name="collision"></param>
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "Player") Debug.LogWarning("Debug : Hit,OK");
    //    if (collision.collider.tag == "Odor") Debug.LogError("Debug : Hit,ERROR");
    //}

    /// <summary>
    /// �v���C���[������������
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_playerDeath == false && collision.collider == _playerCol)
        {
            Debug.LogWarning("HitBody");
            _playerDeath = true;
            audioSource.PlayOneShot(_deathSe);
            Invoke("ReDeath", 10f);
        }
    }

    private void ReDeath()
    {
        _playerDeath = false;
        Debug.Log("ReDeath");
    }
}
