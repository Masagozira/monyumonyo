using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScenes : MonoBehaviour
{
    [SerializeField, Header("遷移先シーン")]
    public string GameScene = "StageSample 1";

    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(ChangeScene);
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(GameScene);
    }
}
