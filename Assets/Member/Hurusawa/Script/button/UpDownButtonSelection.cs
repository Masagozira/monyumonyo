using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class UpDownButtonSelection : MonoBehaviour
{
    public Button[] buttons; // UIボタンの配列
    private int currentButtonIndex = 0;
    [SerializeField] InputActions _inputActions;

    public Color selectedTextColor = Color.black;
    public Color defaultTextColor = Color.gray;
    public float selectedAlpha = 1f;
    public float defauletAlpha = 0.5f;


    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Enable(); // アクションコントロールを有効化

        // コントローラーの上下ボタンに関連付けられたアクションをSubscribe
        _inputActions.UI.UpSelect.performed += ctx => SelectButton(currentButtonIndex - 1); // 上ボタン
        _inputActions.UI.DownSelect.performed += ctx => SelectButton(currentButtonIndex + 1); // 下ボタン

        // Xボタンに関連付けられたアクションをSubscribe
        _inputActions.UI.Decision.performed += ctx => buttons[currentButtonIndex].onClick.Invoke();

        InitializeButtons();

        // 最初のボタンを選択
        UpdateButtonSelection();
    }

    private void OnEnable()
    {
        _inputActions.UI.Enable(); // UIアクションを有効化
    }

    private void OnDisable()
    {
        _inputActions.UI.Disable(); // UIアクションを無効化
    }

    // ボタンの選択を更新するメソッド
    private void SelectButton(int newIndex)
    {
        if (newIndex < 0 || newIndex >= buttons.Length)
        {
            return; // インデックスが範囲外の場合は何もしない
        }

        // 現在選択中のボタンの色をデフォルトに戻す
        SetButonState(buttons[currentButtonIndex], defaultTextColor, defauletAlpha);

        // 新しいボタンを選択
        currentButtonIndex = newIndex;

        // ボタンの選択を更新
        UpdateButtonSelection();
    }

    // ボタンの選択を更新して色を変える
    private void UpdateButtonSelection()
    {
        // 選択中のボタンの色を変更
        SetButonState(buttons[currentButtonIndex], selectedTextColor, selectedAlpha);
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            SetButonState(buttons[i], defaultTextColor, defauletAlpha);
        }
    }

    private void SetButonState(Button button, Color textColor, float alpha)
    {
        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.color = textColor;
        }

        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            Color buttonColor = buttonImage.color;
            buttonColor.a = alpha;
            buttonImage.color = buttonColor;
        }
    }

    public Button GetCurrentButton()
    {
        return buttons[currentButtonIndex];
    }

}
