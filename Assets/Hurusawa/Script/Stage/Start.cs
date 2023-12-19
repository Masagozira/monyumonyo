using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public AudioClip sound;
    public AudioSource Audio;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Audio.PlayOneShot(sound);
            SceneManager.LoadScene("Stage1");
        }
    }
}
