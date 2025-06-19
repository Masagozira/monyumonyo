using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    [SerializeField] public Sprite normalSprite;
    [SerializeField] public Sprite hoverSprite;

    [SerializeField] public ButtonSelection buttonSelection;

    private Button thisButton;
    private bool isMouseHover = false;

    void Start()
    {
        Cursor.visible = true;

        image = GetComponent<Image>();
        image.sprite = normalSprite;

        thisButton = GetComponent<Button>();
    }

    void Update()
    {
        if (isMouseHover) return;// マウスが乗っているときは処理しない

            if (buttonSelection != null && thisButton != null)
            {
                if (buttonSelection.GetCurrentButton() == thisButton)
                {
                    image.sprite = hoverSprite;
                }
                else
                {
                    image.sprite = normalSprite;
                }
            }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseHover = true;
        image.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseHover = false;
        // ボタン選択中かどうかでスプライトを変える
        if (buttonSelection != null && thisButton != null &&
            buttonSelection.GetCurrentButton() == thisButton)
        {
            image.sprite = hoverSprite;
        }
        else
        {
            image.sprite = normalSprite;
        }
    }
}
