//using UnityEngine;
//using UnityEngine.UI;

//public class Fade : MonoBehaviour
//{
//    //�t�F�[�h������I�u�W�F�N�g
//    [SerializeField, Header("�t�F�[�h������I�u�W�F�N�g")]
//    private Image _fadeObj;
//    //�t�F�[�h�̑��x
//    [SerializeField, Header("�t�F�[�h�̑��x�A+�C���@-�A�E�g")]
//    private float _fadeSpeed;
//    //�t�F�[�h�C�����I����Ă��邩���f
//    [SerializeField, Header("�`�F�b�N�O���ƃt�F�[�h�A�E�g�\")]
//    private bool _fadeFin = false;

//    void Update()
//    {
//        //�t�F�[�h�C�����I����Ă��Ȃ����FadeInOut�J�n
//        if(_fadeFin==false)
//        {
//            FadeInOut();
//        }
//        //0���߂��Ă��܂������A0�Ƀ��Z�b�g
//        if(_fadeObj.GetComponent<Image>().color.a < 0)
//        {
//            Color FADE = _fadeObj.GetComponent<Image>().color;
//            FADE.a = 0;
//            _fadeObj.GetComponent<Image>().color = FADE;
//            Debug.Log(_fadeObj.GetComponent<Image>().color);
//            _fadeFin = true;
//            Debug.Log(_fadeFin);
//        }
//        //1���߂��Ă��܂������A1�Ƀ��Z�b�g
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
//    /// �t�F�[�h�C���A�A�E�g����
//    /// </summary>
//    private void FadeInOut()
//    {
//        //����
//        if (_fadeObj.GetComponent<Image>().color.a >= 0 && _fadeObj.GetComponent<Image>().color.a <= 1)
//        {
//            //color�̕ϐ���_fade��Image.coler����
//            Color FADE = _fadeObj.GetComponent<Image>().color;
//            //FADE�̃A���t�@�l���A���ԁ��w��̃X�s�[�h�ŕω�
//            FADE.a = FADE.a + (Time.deltaTime * _fadeSpeed);
//            //FADE�̒l��_fade�ɔ��f������
//            _fadeObj.GetComponent<Image>().color = FADE;
//            Debug.Log(_fadeObj.GetComponent<Image>().color);
//        }
//    }
//}
