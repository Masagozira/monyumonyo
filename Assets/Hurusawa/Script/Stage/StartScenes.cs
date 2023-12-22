using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScenes : MonoBehaviour
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
            SceneManager.LoadScene("Stage1");
        }
    }
}
