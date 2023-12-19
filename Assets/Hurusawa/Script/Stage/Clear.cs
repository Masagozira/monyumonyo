using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Clear");
        }
         if (collision2D.gameObject.CompareTag("Odor"))
        {
            SceneManager.LoadScene("Clear");
        }
        if (collision2D.gameObject.CompareTag("mushroom"))
        {
            SceneManager.LoadScene("Clear");
        }
    }
}
