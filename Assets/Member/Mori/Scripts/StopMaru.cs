using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StopMaru : MonoBehaviour
{
    // PlayerGameobject
    public GameObject _player;

    // MaruWalkSpeed
    [SerializeField, Header("WalkSpeed")]
    private float _maruspeed = 2;

    // MaruWalkArea
    [SerializeField, Header("WalkArea")]
    private float _maruArea = 5;

    // Add with Time until MaruWalkArea
    private float _maruAreaAdd = 0;

    // MaruRunSpeed
    [SerializeField, Header("RunSpeed")]
    private float _angrySpeed = 5f;

    // SearchToPlayerInNear
    [Header("Player In Search Area : true")]
    public bool _playerHere = false;

    // SearchToPlayerInFront
    [Header("Player In Front Serarch Area : true")]
    public bool _playerFrontHere = false;

    // MaruDirection
    private int _direction = 1;
    private Vector3 _scare;

    private bool _playerDeath;
    private CircleCollider2D _playerCol;

    //プレイヤー操作のスクリプト
    private PlayerMovement _marimoScr;

    [SerializeField, Header("PlayerDieSE")]
    public AudioSource audioSource;
    public AudioClip _deathSe1;
    public AudioClip _deathSe2;

    [SerializeField, Header("FadeInPanel")]
    private UnityEngine.UI.Image fadePanel;

    private Animator _maruWalkAnim;

    [SerializeField, Header("死亡後の遷移先シーン")]
    public string gameOverScene = "Gameover2";

    private void Start()
    {
        //Setting
        _player = GameObject.Find("bone_11");
        _scare = this.transform.localScale;
        _playerCol = _player.GetComponent<CircleCollider2D>();
        _playerDeath = false;
        _maruWalkAnim = GetComponent<Animator>();
        _marimoScr = _player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // プレイヤーが索敵範囲内に存在してたら追いかける
        // ・美味しそうな匂いかつ索敵範囲内
        // ・通常時かつ前方索敵範囲内
        if ((_player.tag == "Florus" && (_playerHere == true || _playerFrontHere == true))
            || (_player.tag == "Player" && _playerFrontHere == true))
        {
            ChaseCase();
            Debug.Log("Chase");
        }
        // :
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
        // プレイヤーに向かって移動
        transform.position = Vector2.MoveTowards(
            this.transform.position,
            _player.transform.position,
            _angrySpeed * Time.deltaTime);

        // もしプレイヤー.xが自分より+だったら
        if (this.transform.position.x < _player.transform.position.x)
        {
            // 右向き
            _scare.x = -1;
            this.transform.localScale = _scare;
        }
        // もしプレイヤー.xが自分より-だったら
        else
        {
            // 左向き
            _scare.x = 1;
            this.transform.localScale = _scare;
        }
    }

    private void NonCase()
    {
        Debug.Log("Case2:NotFind");

        // 基本歩行
        _maruAreaAdd += Time.deltaTime * 1.4f;

        // 変数が一定値に達したらリセット、向き反転
        if (_maruAreaAdd >= _maruArea)
        {
            _maruAreaAdd = 0;
            _direction *= -1;

            // 歩行向きが+なら右向き
            if (_direction == 1)
            {
                _scare.x = -1;
                this.transform.localScale = _scare;
            }
            // 歩行向きが-なら左向き
            else
            {
                _scare.x = 1;
                this.transform.localScale = _scare;
            }
        }

        // 歩き続ける
        transform.position = new Vector3(
            transform.position.x + _maruspeed * Time.fixedDeltaTime * _direction,
            this.transform.position.y,
            0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーが死亡した時
        //変更　collider == _playerCol から collision.gameObject.CompareTag("Player")とcollision.gameObject.CompareTag("Florus")
        if (_playerDeath == false && collision.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning("HitBody");
            _playerDeath = true;
            StartCoroutine(FadeOutAndPlaySE());
            _marimoScr.enabled = false;
        }

        if (_playerDeath == false && collision.gameObject.CompareTag("Florus"))
        {
            Debug.LogWarning("HitBody");
            _playerDeath = true;
            StartCoroutine(FadeOutAndPlaySE());
            _marimoScr.enabled = false;
        }
    }

    private IEnumerator FadeOutAndPlaySE()
    {
        // フェードアウトの開始
        StartCoroutine(FadeOut());

        // SEを再生
        audioSource.PlayOneShot(_deathSe1);

        audioSource.PlayOneShot(_deathSe2);

        // SEの再生が終わるまで待機
        yield return new WaitForSeconds(_deathSe1.length);

        // シーン遷移
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
