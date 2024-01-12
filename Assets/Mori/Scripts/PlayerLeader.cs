using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerLerader.OutParent();
/// PlayerLerader.Set11Parent();
/// </summary>
public class PlayerLeader : MonoBehaviour
{
    private GameObject PaCha;
    private GameObject Pa11;
    private int thisChildCount;
    private int ChaChildCount;
    private void Start()
    {
        PaCha = GameObject.Find("Cha_MonyuSmile1");
        Pa11 = GameObject.Find("bone_11");
        Set11Parent();
    }
    /// <summary>
    /// bone11�̎q����
    /// Cha_MonyuSmile1�̎q�֕ύX���A�S�Ă�bone�̐e�ɂȂ�
    /// </summary>
    public void OutParent()
    {
        //bone11�̐e�q�������ACha_MonyuSmile1������
        this.transform.parent = null;
        //�S�Ă�bone�̐e�ɂȂ�
        ChaChildCount = PaCha.transform.childCount;
        for (int i = 0; i < ChaChildCount; i++)
        {
            Transform childTransform = PaCha.transform.GetChild(0);
            GameObject childObject = childTransform.gameObject;
            childObject.gameObject.transform.parent = this.transform;
            Debug.Log(i);
        }
        //Cha_MonyuSmile1�̎q�֕ύX
        this.transform.parent = PaCha.transform;
    }
    /// <summary>
    /// bone11�̎q�ɖ߂�
    /// </summary>
    public void Set11Parent()
    {
        //�����̎q��Cha�̎q�ɂ���
        thisChildCount = this.transform.childCount;
        Debug.Log(thisChildCount);
        for (int i = 0; i < thisChildCount; i++)
        {
            Transform childTransform = this.transform.GetChild(0);
            GameObject childObject = childTransform.gameObject;
            childObject.gameObject.transform.parent = PaCha.transform;
        }
        //11�̎qOBJ�ɂȂ�
        this.transform.parent = Pa11.transform;
    }
}
