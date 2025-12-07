using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField, Header("遷移先シーン")]
    public string GameScene = "StageSample 1";

    [SerializeField] public Image InstructionsImage;

    [SerializeField] public Canvas targetCanvas;
    public static bool IsMenuActive { get; private set; } = false;

    void Start()
    {
        InstructionsImage.gameObject.SetActive(false);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void Instructions()
    {
        InstructionsImage.gameObject.SetActive(true);
    }

    public void InactiveCanvas()
    {
        targetCanvas.gameObject.SetActive(false);
        InstructionsImage.gameObject.SetActive(false);
        IsMenuActive = false;
    }
}
