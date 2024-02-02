using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StopMaru : MonoBehaviour
{
    // �ǂ���������gameobject
    public GameObject _player;

    // �ʏ���s�X�s�[�h
    [SerializeField, Header("WalkSpeed")]
    private float _maruspeed = 2;

    // �ʏ���s�͈�
    [SerializeField, Header("WalkArea")]
    private float _maruArea = 5;

    // ���s�p�A����������_maruArea�̒l�ɂȂ���������Ԃ�
    private float _maruAreaAdd = 0;

    // �ǂ�������X�s�[�h
    [SerializeField, Header("RunSpeed")]
    private float _angrySpeed = 5f;

    // �v���C���[�����G�͈͓��ɂ��邩�F�S��
    [Header("Serarch Area In Player : true")]
    public bool _playerHere = false;

    // �v���C���[�����G�͈͓��ɂ��邩�F�O��
    [Header("Front Serarch Area In Player : true")]
    public bool _playerFrontHere = false;

    // �����̕ύX
    private int _direction = 1;
    private Vector3 _scare;

    private bool _playerDeath;
    private CircleCollider2D _playerCol;

    [SerializeField, Header("�v���C���[�����ʂƂ���SE")]
    public AudioSource audioSource;
    public AudioClip _deathSe;

    [SerializeField, Header("�t�F�[�h�A�E�g�p�̃p�l��")]
    private UnityEngine.UI.Image fadePanel;

    private Animator _maruWalkAnim;

    [SerializeField, Header("死亡後の遷移先シーン")]
    public string gameOverScene = "Gameover1";

    private void Start()
    {
        _player = GameObject.Find("bone_11");
        // �����ݒ�F����
        _scare = this.transform.localScale;
        _playerCol = _player.GetComponent<CircleCollider2D>();
        _playerDeath = false;
        _maruWalkAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        // �v���C���[�����G�͈͓��ɑ��݂��Ă���ǂ�������
        // �E�����������ȓ��������G�͈͓�
        // �E�ʏ펞���O�����G�͈͓�
        if ((_player.tag == "Florus" && (_playerHere == true || _playerFrontHere == true))
            || (_player.tag == "Player" && _playerFrontHere == true))
        {
            ChaseCase();
            Debug.Log("Chase");
        }
        // �ʏ펞�A�܂������ȓ����A����ȊO
        else
        {
            _maruWalkAnim.SetBool("Run", false);
        }

        Debug.Log(" FindPlayer : " + _playerHere + " " + "FindPlayerInFront : " + _playerFrontHere);
        Debug.Log(_playerDeath);
    }

    private void ChaseCase()
    {
        Debug.Log("Case1:Chase");
        _maruWalkAnim.SetBool("Run", true);
        // �v���C���[�Ɍ������Ĉړ�
        transform.position = Vector2.MoveTowards(
            this.transform.position,
            _player.transform.position,
            _angrySpeed * Time.deltaTime);

        // �����v���C���[.x���������+��������
        if (this.transform.position.x < _player.transform.position.x)
        {
            // �E����
            _scare.x = -1;
            this.transform.localScale = _scare;
        }
        // �����v���C���[.x���������-��������
        else
        {
            // ������
            _scare.x = 1;
            this.transform.localScale = _scare;
        }
    }

    private void NonCase()
    {
        Debug.Log("Case2:NotFind");

        // ��{���s
        _maruAreaAdd += Time.deltaTime * 1.4f;

        // �ϐ������l�ɒB�����烊�Z�b�g�A�������]
        if (_maruAreaAdd >= _maruArea)
        {
            _maruAreaAdd = 0;
            _direction *= -1;

            // ���s������+�Ȃ�E����
            if (_direction == 1)
            {
                _scare.x = -1;
                this.transform.localScale = _scare;
            }
            // ���s������-�Ȃ獶����
            else
            {
                _scare.x = 1;
                this.transform.localScale = _scare;
            }
        }

        // ����������
        transform.position = new Vector3(
            transform.position.x + _maruspeed * Time.fixedDeltaTime * _direction,
            this.transform.position.y,
            0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �v���C���[�����S������
        //�ύX�@collider == _playerCol ���� collision.gameObject.CompareTag("Player")��collision.gameObject.CompareTag("Florus")
        if (_playerDeath == false && collision.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning("HitBody");
            _playerDeath = true;
            StartCoroutine(FadeOutAndPlaySE());
        }

        if (_playerDeath == false && collision.gameObject.CompareTag("Florus"))
        {
            Debug.LogWarning("HitBody");
            _playerDeath = true;
            StartCoroutine(FadeOutAndPlaySE());
        }
    }

    private IEnumerator FadeOutAndPlaySE()
    {
        // �t�F�[�h�A�E�g�̊J�n
        StartCoroutine(FadeOut());

        // SE���Đ�
        audioSource.PlayOneShot(_deathSe);

        // SE�̍Đ����I���܂őҋ@
        yield return new WaitForSeconds(_deathSe.length);

        // �V�[���J��
        SceneManager.LoadScene(gameOverScene);
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float fadeTime = 1.5f;

        Color originalColor = fadePanel.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        while (elapsedTime < fadeTime)
        {
            fadePanel.color = Color.Lerp(originalColor, targetColor, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadePanel.color = targetColor;
    }
}
