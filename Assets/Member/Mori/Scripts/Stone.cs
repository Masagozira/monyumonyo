using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enter");
            Rigidbody2D _rig = this.gameObject.GetComponent<Rigidbody2D>();
            _rig.constraints
                = RigidbodyConstraints2D.None
                | RigidbodyConstraints2D.FreezeRotation;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Exit");
            Rigidbody2D _rig = this.gameObject.GetComponent<Rigidbody2D>();
            _rig.constraints
                = RigidbodyConstraints2D.FreezePositionX
                | RigidbodyConstraints2D.FreezeRotation;
            Debug.Log("Exit:1");
        }
    }
}
