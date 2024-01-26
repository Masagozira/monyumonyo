using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Maru : MonoBehaviour
{
    // 追いかけたいgameobject
    private GameObject _player;

    // 通常歩行スピード
    [SerializeField, Header("通常時の歩行スピード")]
    private float _maruspeed = 2;

    // 通常歩行範囲
    [SerializeField, Header("通常時の歩行範囲")]
    private float _maruArea = 5;

    // 歩行用、増加させて_maruAreaの値になったら引き返す
    private float _maruAreaAdd = 0;

    // 追いかけるスピード
    [SerializeField, Header("プレイヤー発見時の歩行スピード")]
    private float _angrySpeed = 5f;

    // プレイヤーが索敵範囲内にいるか：全体
    [Header("索敵範囲内にプレイヤーがいるか、居る：true")]
    public bool _playerHere = false;

    // プレイヤーが索敵範囲内にいるか：前方
    [Header("前方索敵範囲内にプレイヤーがいるか、居る：true")]
    public bool _playerFrontHere = false;

    // 向きの変更
    private int _direction = 1;
    private Vector3 _scare;

    private bool _playerDeath;
    private CircleCollider2D _playerCol;

    [SerializeField, Header("プレイヤーが死ぬときのSE")]
    public AudioSource audioSource;
    public AudioClip _deathSe;

    [SerializeField, Header("フェードアウト用のパネル")]
    private UnityEngine.UI.Image fadePanel;

    private Animator _maruWalkAnim;

    [SerializeField, Header("死亡後の遷移先シーン")]
    public string gameOverScene = "Gameover1";

    private void Start()
    {
        _player = GameObject.Find("bone_11");
        // 初期設定：向き
        _scare = this.transform.localScale;
        _playerCol = _player.GetComponent<CircleCollider2D>();
        _playerDeath = false;
        _maruWalkAnim = GetComponent<Animator>();
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
            Debug.Log("プレイヤー追跡中");
        }
        // 通常時、まずそうな匂い、それ以外
        else
        {
            NonCase();
        }

        Debug.Log("プレイヤーが近くにいる : " + _playerHere + " " + "プレイヤーが前にいる : " + _playerFrontHere);
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
        _maruWalkAnim.SetBool("Run", false);
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
        // フェードアウトの開始
        StartCoroutine(FadeOut());

        // SEを再生
        audioSource.PlayOneShot(_deathSe);

        // SEの再生が終わるまで待機
        yield return new WaitForSeconds(_deathSe.length);

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
