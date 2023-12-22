using UnityEngine;
/// <summary>
/// �V���N�V���N�̂��ł̓����̃X�N���v�g
/// </summary>
public class SyukuHands : MonoBehaviour
{
    //���g��Transform
    [SerializeField]
    private Transform _armsTra;
    //���g��Collider2D
    [SerializeField]
    private BoxCollider2D _armsCol;
    //�^�[�Q�b�g��Transform
    [SerializeField, Header("�v���C���[��Transform�擾")]
    private Transform _targetTra;
    //�^�[�Q�b�g��Rigidbody2D
    [SerializeField, Header("�v���C���[��RigidBody�擾")]
    private Rigidbody2D _targetRig;
    //�v���C���[�^�O�擾�p&��������p
    [SerializeField, Header("�v���C���[")]
    private GameObject _marimo;
    //�v���C���[����̃X�N���v�g
    [SerializeField, Header("�v���C���[�̑���X�N���v�g�擾")]
    private PlayerMovement _marimoScr;
    //�r�����������ɂ��邩
    [Header("���G�͈͓��Ƀv���C���[�����邩�A����Ftrue")]
    public bool _isHandsMove = false;
    //�r��L�΂�(�X�v���C�g��L�΂�)���߂�SpriteRenderer�擾
    private SpriteRenderer _syuSpr;
    //�r���ǂꂭ�炢�̂΂���
    [SerializeField, Header("�r�̒������E�l�A�ǂ̂��炢�L�т邩")]
    private float _syuHandHeightRim = 10f;
    //�r�̂̂т鑬�x
    [SerializeField,Header("�r���v���C���[�Ɍ������ĐL�т鑬��")]
    private float _handTime = 30;
    //�r�̂̂т鑬�x�F����p
    private float _handLong = 0.5f;
    //�r�̂̂т鑬�x�FCollider����p
    private float _handColSize = 0.4f;
    //�����������������
    private Vector3 dir;
    //�v���C���[�����͂�ł��邩
    private bool _chaching = false;
    //�����񂹂���v���C���[�̑���
    [SerializeField,Header("�����񂹂鑬��")]
    private float _attractionSpeed = 1f;

    /// <summary>
    /// �����ݒ�
    /// �E���ł�SpriteRenderer��BoxCollider2D�̎擾
    /// </summary>
    private void Start()
    {
        _syuSpr = this.GetComponent<SpriteRenderer>();
        _armsCol = this.GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// �����Ŕ��肵�Ă��ł�����
    /// </summary>
    void Update()
    {
        //���������ŁA���G�͈͓��ŁA�͂܂�Ă��Ȃ���
        if (_marimo.gameObject.tag == "Florus" && _isHandsMove == true && _chaching == false)
        {
            HandsMove();
            HandStretch();
        }
        //�͂܂ꂽ��
        else if (_chaching==true)
        {
            HandShrink();
        }

        //bool�m�F
        Debug.Log("Player�����G�͈͓��ɂ���F" + _isHandsMove + "," + "�͂�ł���F" + _chaching);

        //�܂��������ɂȂ�����
        if (_marimo.gameObject.tag == "Odor")
        {
            HandShrink();
            _targetRig.bodyType = RigidbodyType2D.Dynamic;
            _chaching = false;
            _marimoScr.enabled = true;
        }
    }

    /// <summary>
    /// �v���C���[�Ƃ��ł������������A�v���C���[�𓮂��Ȃ�����i�����؂�ւ������j
    /// �E�v���C���[�̑���X�N���v�g�𖳌��ɂ���
    /// �E�v���C���[�̏d�͂𖳂���
    /// �E�͂�ł��邩���肷��bool��true�ɂ���
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Florus" || collision.gameObject.tag == "Player")
        {
            Debug.Log("�L���b�`");
            _chaching = true;
            _targetRig.bodyType = RigidbodyType2D.Kinematic;
            _marimoScr.enabled = false;
        }
    }

    /// <summary>
    /// �v���C���[�Ɍ������Ă��ł�L�΂�
    /// </summary>
    private void HandsMove()
    {
        //���������������v�Z
        dir = (_targetTra.position - _armsTra.position);
        //�������������ɉ�]
        _armsTra.rotation = Quaternion.FromToRotation(Vector3.up, -dir);

        //FromToRotation
        //Vector3 vector3 = _targetTra.position - this.transform.position;
        //Quaternion quaternion = Quaternion.LookRotation(vector3);
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, _targetTra.transform.rotation, _speed);
    }

    /// <summary>
    /// ���ŐL�т�
    /// </summary>
    private void HandStretch()
    {
        if (_handLong>-_syuHandHeightRim)
        {
            _handLong += _handTime / 100;
            _handColSize += _handTime / 100;
            //�r��L�΂�
            _syuSpr.size = new Vector2(1f, -_handLong);
            _armsCol.offset = new Vector2(0, -_handColSize / 2);
            _armsCol.size = new Vector2(0.5f, _handColSize);
        }
    }

    /// <summary>
    /// ���ŏk��
    /// </summary>
    private void HandShrink()
    {
        if(_handLong >= 0.01f||(_handLong >= 0.01f&&_marimo.gameObject.tag == "Odor"))
        {
            _handLong -= _handTime / 100;
            _handColSize -= _handTime / 100;
            //�r���k�߂�
            _syuSpr.size = new Vector2(1f, -_handLong);
            _armsCol.offset = new Vector2(0, -_handColSize / 2);
            _armsCol.size = new Vector2(0.5f, _handColSize);
        }
        _marimo.transform.position 
            = Vector3.MoveTowards(_marimo.transform.position, this.transform.position, _attractionSpeed/100);
    }

}

/*
�ύX�_
 private MARIMO _marimoScr;�@����@private PlayerMovement _marimoScr;�ɕύX
*/