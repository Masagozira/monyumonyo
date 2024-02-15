using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    [SerializeField, Header("フェードアウト用のパネル")]
    private UnityEngine.UI.Image fadePanel;

    public string gameClearScene = "Gameover1";

    [SerializeField, Header("SE")]
    public AudioSource Audio;
    public AudioClip audioClip;

    void Start()
    {
        Cursor.visible = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Odor") || other.CompareTag("Florus"))
        {
            StartCoroutine(FadeOut());
        }

        if (other.CompareTag("Player2"))
        {
            Audio.PlayOneShot(audioClip);
        }
        
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
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
         SceneManager.LoadScene(gameClearScene);
    }

}
