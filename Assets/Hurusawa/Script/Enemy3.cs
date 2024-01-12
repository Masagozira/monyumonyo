using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.SceneManagement;

public class Enemy3 : MonoBehaviour
{
    // 敵の移動関連
    private bool isEnemyMove = false;
    public Vector3 targetPosition; // 移動先の座標を指定するためのVector3
    public float moveSpeed = 5f; // 移動速度

    // プレイヤーと敵3のSEとフェードアウト
    // SE
    private bool isFadingOutAndPlayingSE = false;
    [SerializeField, Header("プレイヤーが死ぬときのSE")]
    public AudioSource audioSource;
    public AudioClip _deathSe; // プレイヤーが死ぬときのSE

    // フェードアウト
    [SerializeField, Header("フェードアウト用のパネル")]
    private UnityEngine.UI.Image fadePanel;
    private SpriteRenderer spriteRenderer;

    [SerializeField, Header("敵3の起動時に切り替わるSprite")]
    // スプライトを切り替えるオブジェクトの取得
    public GameObject ChangeObject;
    // 元のスプライト
    public Sprite initialSprite;
    // 切り替えるスプライト
    public Sprite newSprite;
    private bool wasGrounded = true;

    [SerializeField, Header("ぶつかったときに消すobject")]
    // 障害物
    public GameObject Destroyobj;
    // 敵1
    public GameObject Obstacleobj;

    
    [SerializeField, Header("障害物にぶつかったときのアニメーションとエフェクト、SE")]
    public Animator animator;
    public ParticleSystem Clash_2;
    public AudioSource audioSource2;
    public AudioClip DestroyobjSE;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isEnemyMove)
        {
            EnemyMove();
            // 目標座標までの距離が一定以下になったらオブジェクトを削除
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                StartCoroutine(FadeOutAndDestroy());
            }
        }
    }

    //プレイヤーと障害物に触れたときに削除する処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // フェードアウトとSE再生中でない場合に実行
        if (!isFadingOutAndPlayingSE)
        {
            // 衝突したオブジェクトが特定のタグを持っている場合にフェードアウトとSE再生を実行
            if (collision.gameObject.tag == "Odor" || collision.gameObject.tag == "Florus" || collision.gameObject.tag == "Player")
            {
                StartCoroutine(FadeOutAndPlaySE());
            }
        }

        // 衝突したオブジェクトが特定のタグを持っている場合にDestroyobjを3秒後に削除
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(Destroyobj, 3f);
        }

        // 衝突したオブジェクトが特定のタグを持っている場合にObstacleobjとエフェクトを3秒後に削除
        if (collision.gameObject.tag == "woll")
        {
            HandleObstacleCollision();
        }
    }

    //トリガー内のプレイヤーを取得
    private void OnTriggerStay2D(Collider2D collider)
    {
        // 特定のタグのオブジェクトに当たった場合に敵の移動とスプライト切り替えを実行
        if (collider.CompareTag("Florus"))
        {
            isEnemyMove = true;
            ChangeSprite();
        }
    }

    //敵3の移動処理
    private void EnemyMove()
    {
        // 目標座標への方向ベクトルを計算
        Vector3 direction = targetPosition - transform.position;

        // 方向ベクトルの長さがゼロでない場合に移動
        if (direction.magnitude > 0.01f)
        {
            // 方向ベクトルを正規化して移動方向を取得
            Vector3 moveDirection = direction.normalized;

            // 移動
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    //プレイヤーとぶつかったときのフェードアウトとSEの再生メソッド
    private IEnumerator FadeOutAndPlaySE()
    {
        // 既にフェードアウトとSE再生中の場合は終了
        if (isFadingOutAndPlayingSE)
        {
            yield break;
        }

        isFadingOutAndPlayingSE = true;
        // フェードアウトの開始
        StartCoroutine(FadeOut());

        // SEを再生
        audioSource.PlayOneShot(_deathSe);

        // SEの再生が終わるまで待機
        yield return new WaitForSeconds(_deathSe.length);

        // シーン遷移
        SceneManager.LoadScene("Gameover");
        isFadingOutAndPlayingSE = false;
    }

    private IEnumerator FadeOutAndDestroy()
    {
        yield return StartCoroutine(FadeOut2());

        // オブジェクトを削除
        Destroy(gameObject);
    }

    //プレイヤーとぶつかったときのフェードアウト
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

    //敵３が指定位置まで移動したときに動作するフェードアウト
    private IEnumerator FadeOut2()
    {
        float elapsedTime2 = 0f;
        float fadeTime2 = 1.5f;

        Color originalColor = spriteRenderer.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        while (elapsedTime2 < fadeTime2)
        {
            spriteRenderer.color = Color.Lerp(originalColor, targetColor, elapsedTime2 / fadeTime2);
            elapsedTime2 += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = targetColor;
    }

    // スプライトを切り替える
    void ChangeSprite()
    {
        SpriteRenderer renderer = ChangeObject.GetComponent<SpriteRenderer>();

        if (renderer != null && newSprite != null)
        {
            renderer.sprite = newSprite;
        }
    }

    private void HandleObstacleCollision()
    {
        // アニメーションを開始
        animator.SetBool("ObjBool", true);

        //SEを再生
        audioSource.PlayOneShot(DestroyobjSE);

        // パーティクルシステムのインスタンスを生成する。
        ParticleSystem newParticle = Instantiate(Clash_2);

        // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
        newParticle.transform.position = transform.position;

        // パーティクルを発生させる。
        newParticle.Play();

        // インスタンス化したパーティクルシステムのGameObjectを3秒後に削除する
        Destroy(newParticle.gameObject, 3f);

        // 障害物オブジェクトを3秒後に削除する
        Destroy(Obstacleobj, 3f);
    }
}