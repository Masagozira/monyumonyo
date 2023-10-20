using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MARIMO : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    void Update()
    {
        Vector2 position = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            position.x -= _speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            position.x += _speed;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            position.y += _speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            position.y -= _speed;
        }
        transform.position = position;
    }
}
