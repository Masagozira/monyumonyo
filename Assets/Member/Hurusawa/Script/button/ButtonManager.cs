using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioClip sound;
    public AudioSource Audio;

    void Start()
    {
        Cursor.visible = true;
    }

    // サウンドエフェクトが終了した後にシーン遷移を行う
    private IEnumerator LoadSceneAfterSound(string sceneName)
    {
        Audio.PlayOneShot(sound);

        // サウンドの再生が終わるまで待つ
        yield return new WaitForSeconds(sound.length);

        // SceneManagerを使用して指定されたシーンに遷移
        SceneManager.LoadScene(sceneName);
    }

    // ボタンが押されたときの共通処理
    private void OnButtonPressed(string sceneName)
    {
        StartCoroutine(LoadSceneAfterSound(sceneName));
    }

    public void PushButton1()
    {
        OnButtonPressed("StageSample 1");
    }

    public void PushButton2()
    {
        OnButtonPressed("StageSample 2");
    }

    public void PushButton3()
    {
        OnButtonPressed("Start");
    }

    public void PushButton4()
    {
        OnButtonPressed("Select");
    }
}
