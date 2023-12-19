using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Odor"))
        {
            SceneManager.LoadScene("Stage1");
        }
        if (collision2D.gameObject.CompareTag("mushroom"))
        {
            SceneManager.LoadScene("Stage1");
        }
         if (collision2D.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Stage1");
        }
    }
}
