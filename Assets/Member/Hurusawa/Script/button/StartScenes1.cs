using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartScenes1 : MonoBehaviour
{
    [SerializeField] public PlayerInput _playerInput;
    public AudioClip sound;
    public AudioSource Audio;


    void Start()
    {
        Cursor.visible = false;

        _playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (_playerInput.actions["Decision"].triggered)
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