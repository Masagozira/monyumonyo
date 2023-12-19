using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Gameover");
        }
        if (collision2D.gameObject.CompareTag("Odor"))
        {
            SceneManager.LoadScene("Gameover");
        }
        if (collision2D.gameObject.CompareTag("mushroom"))
        {
            SceneManager.LoadScene("Gameover");
        }
        if (collision2D.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision2D.gameObject);
        }
    }

}
