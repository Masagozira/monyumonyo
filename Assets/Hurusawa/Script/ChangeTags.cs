using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ChangeTags : MonoBehaviour
{
    //タグ切り替え中の時間
    private bool istag1 = false;
    private bool istag2 = false;
    public float ChangeTime1 = 5.0f;
    public float ChangeTime2 = 4.0f;
    public float ChangeTime = 0.0f;

    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in objs)
        {
            Debug.Log(obj.gameObject.name);
        }
    }

    void FixedUpdate()
    {

        if (istag1 || istag2)
        {
            ChangeTime += Time.deltaTime;

            if (ChangeTime >= (istag1 ? ChangeTime1 : ChangeTime2))
            {
                EndChangeTag();
            }

            return;
        }

        //タグ切り替え
        if (!istag1 && Input.GetKeyDown(KeyCode.W))
        {
            SwitchTag1();
            istag1 = true;
            ChangeTime = 0.0f;
            Debug.Log("Florusに変更しました");
        }

        if (!istag2 && Input.GetKeyDown(KeyCode.S))
        {
            SwitchTag2();
            istag2 = true;
            ChangeTime = 0.0f;
            Debug.Log("Odorに変更しました");
        }

        Debug.Log("istag1: " + istag1 + ", istag2: " + istag2);
        Debug.Log("ChangeTime: " + ChangeTime);
    }

    //良い匂いというタグに切り替える
    private void SwitchTag1()
    {
        this.tag = "Florus";
        ChangeChildTags("Florus");
    }

    //嫌な匂いというタグに切り替える
    private void SwitchTag2()
    {
        this.tag = "Odor";
        ChangeChildTags("Odor");
    }

    //子オブジェクトのタグを切り替える
    private void ChangeChildTags(string newTag)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.tag = newTag;
            Debug.Log(child.gameObject.name);
        }
    }

    
    void EndChangeTag()
    {
        istag1 = false;
        istag2 = false;
        this.tag = "Player";
        ChangeChildTags("Player");
    }
}
