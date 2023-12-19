using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonImage :MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    [SerializeField] public Sprite normalSprite; // ボタンの通常時の画像
    [SerializeField]public Sprite hoverSprite;  // マウスが乗ったときの画像

    // 初期化時に呼ばれるメソッド
    void Start()
    {
        // Imageコンポーネントを取得
        image = GetComponent<Image>();

        // ボタンの通常時の画像を設定
        image.sprite = normalSprite;
    }

    // マウスポインターがボタンに乗ったときの処理
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ボタンの画像を変更する
        image.sprite = hoverSprite;
    }

    // マウスポインターがボタンから離れたときの処理
    public void OnPointerExit(PointerEventData eventData)
    {
        // ボタンの画像を元に戻す
        image.sprite = normalSprite;
    }
}