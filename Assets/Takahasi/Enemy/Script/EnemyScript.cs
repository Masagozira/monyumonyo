using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    public GameObject Player;                   // �ǂ���������gameobject
    [SerializeField]
    public GameObject Enemy;                    // �ǂ�������gameobject
    [SerializeField]
    public int _enemypos = 1;                   // �G�l�~�[�̍��W
    [SerializeField]
    public float speed = 2;                     // �ʏ���s�X�s�[�h
    [SerializeField]
    public float ChaseSpeed = 5f;               // �ǂ�������X�s�[�h
    [SerializeField]
    public float Smelltime = 5f;                // �������ԁi�b�j

    public float RotationY = 180f;              //�摜�����E���]�����鐔

    private float Smelltimer = 0.0f;                                        // �o�ߎ��Ԃ��i�[����^�C�}�[�ϐ�(�����l0�b)
    private bool Smellsdelicious = false;                                   // �����������ȓ�����Ԃ��ǂ����̃t���O
    private bool Smellsdisgusting = false;                                  // �s�������ȓ�����Ԃ��ǂ����̃t���O
    private bool Usual = false;                                             // �ʏ��Ԃ��ǂ����̃t���O
    private Vector2 pos;                                                    // ���W
    private string playertag = "Player";                                    // Player�̒ʏ��ԃ^�O
    private string playerSmellsdelicioustag = "playerSmellsdelicious";      // 2�^�O�p�FPlayer�̔����������ȓ����^�O(1���g���Ȃ�v��Ȃ�)
    private string playerSmellsdisgustingtag = "playerSmellsdisgusting";    // 2�^�O�p�FPlayer�̕s�������ȓ�����ԃ^�O(1���g���Ȃ�v��Ȃ�)

    PlatformEffector2D _enemy;                                              // �ђ�,�����蔻��̖��O


    private void Start()
    {

    }

    private void Update()
    {
        EnemyOnClickKey(); //�L�[���͌Ăяo��

        //�����������ȓ�����t���O��True�̂Ƃ��ɖ��t���[�����s
        if (Smellsdelicious)
        {
            Debug.Log("�����������ȍ���");

            //�v���C���[�����݂��Ă���ǂ�������
            if (Player)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, ChaseSpeed * Time.deltaTime);

                //���蔲�������Ȃ�,PlatformEffector2D��true�ioff�j�ɂ���
                _enemy = Enemy.GetComponent<PlatformEffector2D>();
                _enemy.enabled = false;
            }

            //���t���[���^�C�}�[�ϐ���Time.deltaTime�𑫂�
            Smelltimer += Time.deltaTime;

            //�^�C�}�[�����G����(5�b)�𒴂����Ƃ�
            if (Smelltimer >= Smelltime)
            {
                Debug.Log("���ɖ߂�");
                //���G��ԃt���O��False�ɂ���
                Smellsdelicious = false;
                //�^�C�}�[��0.0�b�Ƀ��Z�b�g����
                Smelltimer = 0.0f;
            }
        }

        //�s�������ȓ�����t���O��True�̂Ƃ��ɖ��t���[�����s
        if (Smellsdisgusting)
        {
            Debug.Log("�s�������ȍ���");
            //�s�������ȍ�����o���ĊђʃA�N�V����
            //���蔲��������,PlatformEffector2D��true�ion�j�ɂ���
            _enemy = Enemy.GetComponent<PlatformEffector2D>();
            _enemy.enabled = true;

            //���t���[���^�C�}�[�ϐ���Time.deltaTime�𑫂�
            Smelltimer += Time.deltaTime;
            //�^�C�}�[�����G����(5�b)�𒴂����Ƃ�
            if (Smelltimer >= Smelltime)
            {
                Debug.Log("���ɖ߂�");
                //���G��ԃt���O��False�ɂ���
                Smellsdisgusting = false;
                //�^�C�}�[��0.0�b�Ƀ��Z�b�g����
                Smelltimer = 0.0f;
            }

        }

        //�ʏ���
        if (Usual)
        {
            //��{���s
            pos = transform.position;
            // transform���擾
            Transform myTransform = this.transform;

            //�G�̍��E�ړ�
            transform.Translate(transform.right * Time.deltaTime * speed * _enemypos);
            if (pos.x > 1.5)
            {
                _enemypos = -1;

                //�摜��]
                myTransform.Rotate(0f, RotationY, 0f); 
            }
            if (pos.x < -1.5)
            {
                _enemypos = 1;

                //�摜��]
                myTransform.Rotate(0f, -RotationY, 0f);
            }
        }

    }

    //�o�O�h�~�̉摜�̌Œ艻
    public void Rotation()
    {
        if (RotationY <= 180)
        {
            RotationY = 180;
        }

        if (RotationY <= 0)
        {
            RotationY = 0;
        }
    }


    //�v���C���[����L�[����
    public void EnemyOnClickKey()
    {
        //��@UpKey�������ꂽ��invoke���Ăяo��
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Debug.Log("�������������ȓ���");
            //�{�^���������ꂽ��t���O��True�ɂ���
            Smellsdelicious = true;
            //�{�^���������ꂽ��t���O��false�ɂ���
            //�s�������ȓ����Ɣ����������ȓ������d�˂Ďg���Ȃ��l�ɂ��邽��
            Smellsdisgusting = false;

        }
        //���@DownKey�������ꂽ��invoke���Ăяo���Ȃ�����
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Debug.Log("�܂������ȓ���");
            //�{�^���������ꂽ��t���O��True�ɂ���
            Smellsdisgusting = true;
            //�{�^���������ꂽ��t���O��false�ɂ���
            //�s�������ȓ����Ɣ����������ȓ������d�˂Ďg���Ȃ��l�ɂ��邽��
            Smellsdelicious = false;

        }

        else //�㉺�ȊO�̃{�^���������ĂȂ��Ƃ�
        {
            //Debug.Log("����������");
            //���蔲�������Ȃ�,PlatformEffector2D��true�ioff�j�ɂ���
            Usual = true;
            _enemy = Enemy.GetComponent<PlatformEffector2D>();
            _enemy.enabled = false;
        }

    }

    //�v���C���[�ɓ��������琶���邩���ʂ�
    void OnCollisionEnter2D(Collision2D collision)
    {
        //�P
        if (collision.collider.tag == playertag)
        {
            if (_enemy.enabled == true)
            {
                Debug.Log("seef");      //�����N����Ȃ�
            }
            if (_enemy.enabled == false)
            {
                Debug.Log("hit");
                Destroy(Player, 0.2f);  //�v���C���[����
            }
        }

        ////�Q�^�O�o�[�W�������g���Ȃ�P���R�����g�A�E�g������
        ////�����������ȓ����^�O
        //if (collision.collider.tag == playerSmellsdelicioustag)
        //{
        //    Debug.Log("hit");
        //    Destroy(Player, 0.2f);  //�v���C���[����
        //}
        ////�s�������ȓ����^�O
        //if (collision.collider.tag == playerSmellsdisgustingtag)
        //{
        //    Debug.Log("seef");      //�����N����Ȃ�
        //}
        ////�ʏ��Ԃ̓����^�O
        //if (collision.collider.tag == playertag)
        //{
        //    Debug.Log("hit");
        //    Destroy(Player, 0.2f);  //�v���C���[����
        //}
    }

    //�T���͈�,�͈͓��ɓ�������ǂ�������
    void OnTriggerStay2D(Collider2D collision)
    {
        //1
        if (collision.gameObject.tag == playertag)
        {
            
            //�s�������ȓ����̎��͒ǂ������Ȃ�
            if (Smellsdisgusting == true)
            {

            }
            //����ȊO�͒ǂ�������
            else
            {
                //Debug.Log("hit�q�b�g");
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, ChaseSpeed * Time.deltaTime);
            }
        }

        ////�Q�^�O�o�[�W�������g���Ȃ�P���R�����g�A�E�g������
        ////�����������ȓ����^�O
        //if (collision.gameObject.tag == playerSmellsdelicioustag)
        //{
        //    //Debug.Log("hit�q�b�g");
        //    //transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, ChaseSpeed * Time.deltaTime);
        //}
        ////�s�������ȓ����^�O
        //if (collision.gameObject.tag == playerSmellsdisgustingtag)
        //{
        //    Debug.Log("seef");      //�����N����Ȃ�
        //}
        ////�ʏ��Ԃ̓����^�O
        //if (collision.gameObject.tag == playertag)
        //{
        //    //Debug.Log("hit�q�b�g");
        //    //transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, ChaseSpeed * Time.deltaTime);
        //}
    }

}
 