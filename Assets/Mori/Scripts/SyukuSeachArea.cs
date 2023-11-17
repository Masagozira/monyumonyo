using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyukuSeachArea : MonoBehaviour
{
    [SerializeField]
    private SyukuHands _syukuHands;

    private void Start()
    {
        _syukuHands._isHandsMove = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_syukuHands._isHandsMove==false)
        {
            _syukuHands._isHandsMove = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_syukuHands._isHandsMove == true)
        {
            _syukuHands._isHandsMove = false;
        }
    }
}
