using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScenes1 : MonoBehaviour
{
    public AudioClip sound;
    public AudioSource Audio;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Audio.PlayOneShot(sound);

            // 1秒後に指定されたメソッド（LoadStartScene）を呼び出す
            Invoke("Scene", 0.5f);
        }
    }

    void Scene()
    {
        // シーンを遷移
        SceneManager.LoadScene("Start");
    }
}
