using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyukuBody : MonoBehaviour
{
    [SerializeField,Header("プレイヤーアタッチ")]
    private GameObject _marimo;
    private CircleCollider2D _marimoCol;
    private bool _playerDeath;
    [SerializeField, Header("プレイヤーが死ぬときのSE")]
    private AudioClip _deathSe;
    AudioSource audioSource;
    private void Start()
    {
        _playerDeath = false;
        audioSource = GetComponent<AudioSource>();
        _marimoCol = _marimo.GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// プレイヤーが当たった時
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_playerDeath == false && collision.collider == _marimoCol)
        {
            _playerDeath = true;
            audioSource.PlayOneShot(_deathSe);
            Invoke("ReDeath", 10f);
        }
    }

    private void ReDeath()
    {
        _playerDeath = false;
    }
}
