using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MARIMO : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    void Update()
    {
        Vector2 position = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            position.x -= speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            position.x += speed;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            position.y += speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            position.y -= speed;
        }

        transform.position = position;
    }
}
