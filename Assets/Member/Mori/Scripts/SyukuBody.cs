using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SyukuBody : MonoBehaviour
{
    private GameObject _marimo;
    private CircleCollider2D _marimoCol;
    private bool _playerDeath;
    [SerializeField, Header("�v���C���[�����ʂƂ���SE")]
    public AudioSource Audio;
    public AudioClip _deathSe1;
    public AudioClip _deathSe2;
    AudioSource audioSource;

    [SerializeField, Header("フェードアウト用のパネル")]
    private UnityEngine.UI.Image fadePanel;

    private Animator _maruWalkAnim;

    [SerializeField, Header("死亡後の遷移先シーン")]
    public string gameOverScene = "Gameover1";

    private void Start()
    {
        _marimo = GameObject.Find("Cha_MonyuSmile1");
        _playerDeath = false;
        audioSource = GetComponent<AudioSource>();
        _marimoCol = _marimo.GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// �v���C���[������������
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_playerDeath == false && collision.collider == _marimoCol)
        {
            _playerDeath = true;
            //Audio.PlayOneShot(_deathSe);
           // Invoke("ReDeath", 10f);
            StartCoroutine(FadeOutAndPlaySE());
        }

    }

    private void ReDeath()
    {
        _playerDeath = false;
    }

    private IEnumerator FadeOutAndPlaySE()
    {
        // フェードアウトの開始
        yield return StartCoroutine(FadeOut());

        // SEを再生
        Audio.PlayOneShot(_deathSe1);
        Audio.PlayOneShot(_deathSe2);

        // SEの再生が終わるまで待機
        yield return new WaitForSeconds(_deathSe1.length);

        // シーン遷移
        SceneManager.LoadScene(gameOverScene);
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float fadeTime = 1.2f;

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
