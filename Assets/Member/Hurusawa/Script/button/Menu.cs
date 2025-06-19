using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    [SerializeField] public Canvas targetCanvas;
    public PlayerInput _playerInput;

    public static bool IsMenuActive { get; private set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        targetCanvas.gameObject.SetActive(false);
        IsMenuActive = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (_playerInput.actions["Menu"].triggered)
        {
          if (IsMenuActive)
           {
            InactiveCanvas();
           }
           else
           {
              ActivateCanvas();
           }
        }
    
    }

    public void ActivateCanvas()
    {

        targetCanvas.gameObject.SetActive(true);
        IsMenuActive = true;

    }

    public void InactiveCanvas()
    {
        targetCanvas.gameObject.SetActive(false);
        IsMenuActive = false;

    }
}
