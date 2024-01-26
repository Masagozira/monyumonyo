using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageSelect : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
    }
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Select");
        }
    }
}
