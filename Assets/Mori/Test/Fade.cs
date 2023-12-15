//using UnityEngine;
//using UnityEngine.UI;

//public class Fade : MonoBehaviour
//{
//    //フェードさせるオブジェクト
//    [SerializeField, Header("フェードさせるオブジェクト")]
//    private Image _fadeObj;
//    //フェードの速度
//    [SerializeField, Header("フェードの速度、+イン　-アウト")]
//    private float _fadeSpeed;
//    //フェードインが終わっているか判断
//    [SerializeField, Header("チェック外すとフェードアウト可能")]
//    private bool _fadeFin = false;

//    void Update()
//    {
//        //フェードインが終わっていなければFadeInOut開始
//        if(_fadeFin==false)
//        {
//            FadeInOut();
//        }
//        //0を過ぎてしまった時、0にリセット
//        if(_fadeObj.GetComponent<Image>().color.a < 0)
//        {
//            Color FADE = _fadeObj.GetComponent<Image>().color;
//            FADE.a = 0;
//            _fadeObj.GetComponent<Image>().color = FADE;
//            Debug.Log(_fadeObj.GetComponent<Image>().color);
//            _fadeFin = true;
//            Debug.Log(_fadeFin);
//        }
//        //1を過ぎてしまった時、1にリセット
//        if (_fadeObj.GetComponent<Image>().color.a > 1)
//        {
//            Color FADE = _fadeObj.GetComponent<Image>().color;
//            FADE.a = 1;
//            _fadeObj.GetComponent<Image>().color = FADE;
//            Debug.Log(_fadeObj.GetComponent<Image>().color);
//            _fadeFin = true;
//            Debug.Log(_fadeFin);
//        }
//    }
//    /// <summary>
//    /// フェードイン、アウト処理
//    /// </summary>
//    private void FadeInOut()
//    {
//        //条件
//        if (_fadeObj.GetComponent<Image>().color.a >= 0 && _fadeObj.GetComponent<Image>().color.a <= 1)
//        {
//            //colorの変数に_fadeのImage.colerを代入
//            Color FADE = _fadeObj.GetComponent<Image>().color;
//            //FADEのアルファ値を、時間＊指定のスピードで変化
//            FADE.a = FADE.a + (Time.deltaTime * _fadeSpeed);
//            //FADEの値を_fadeに反映させる
//            _fadeObj.GetComponent<Image>().color = FADE;
//            Debug.Log(_fadeObj.GetComponent<Image>().color);
//        }
//    }
//}
