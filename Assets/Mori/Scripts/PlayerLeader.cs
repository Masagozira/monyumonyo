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
    /// bone11の子から
    /// Cha_MonyuSmile1の子へ変更し、全てのboneの親になる
    /// </summary>
    public void OutParent()
    {
        //bone11の親子を解除、Cha_MonyuSmile1を検索
        this.transform.parent = null;
        //全てのboneの親になる
        ChaChildCount = PaCha.transform.childCount;
        for (int i = 0; i < ChaChildCount; i++)
        {
            Transform childTransform = PaCha.transform.GetChild(0);
            GameObject childObject = childTransform.gameObject;
            childObject.gameObject.transform.parent = this.transform;
            Debug.Log(i);
        }
        //Cha_MonyuSmile1の子へ変更
        this.transform.parent = PaCha.transform;
    }
    /// <summary>
    /// bone11の子に戻る
    /// </summary>
    public void Set11Parent()
    {
        //自分の子をChaの子にする
        thisChildCount = this.transform.childCount;
        Debug.Log(thisChildCount);
        for (int i = 0; i < thisChildCount; i++)
        {
            Transform childTransform = this.transform.GetChild(0);
            GameObject childObject = childTransform.gameObject;
            childObject.gameObject.transform.parent = PaCha.transform;
        }
        //11の子OBJになる
        this.transform.parent = Pa11.transform;
    }
}
