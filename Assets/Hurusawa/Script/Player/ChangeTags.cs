using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ChangeTags : MonoBehaviour
{
    // タグ切り替え中の時間
    private bool istag1 = false;  // タグ1（"Florus"）に切り替え中かどうかを示すフラグ
    private bool istag2 = false;  // タグ2（"Odor"）に切り替え中かどうかを示すフラグ
    public float ChangeTime1 = 5.0f;  // タグ1に切り替えるまでの時間
    public float ChangeTime2 = 4.0f;  // タグ2に切り替えるまでの時間
    public float ChangeTime = 0.0f;  // 現在の切り替え中の経過時間

    //切り替え時のSE
    public AudioSource Audio;
    public AudioClip FloruSE; //いい匂いの時のSE
    public AudioClip OdorSE; //嫌な臭いの時のSE

    void Start()
    {
        // ゲーム開始時に"Player"タグを持つオブジェクトを検索し、ログに表示
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in objs)
        {
            //Debug.Log(obj.gameObject.name);
        }

        Transform parentTransform = transform; // あなたの親オブジェクトのTransformに置き換えてください
        GetLayersRecursive(parentTransform);
    }

    void FixedUpdate()
    {
        // タグが切り替え中の場合
        if (istag1 || istag2)
        {
            ChangeTime += Time.deltaTime;

            // 切り替え中の時間が指定時間を超えた場合
            if (ChangeTime >= (istag1 ? ChangeTime1 : ChangeTime2))
            {
                EndChangeTag();  // タグの切り替えを終了する
            }

            return;
        }

        // "Florus"タグに切り替える条件
        if (!istag1 && Input.GetKeyDown(KeyCode.W))
        {
            Audio.PlayOneShot(FloruSE);
            SwitchTag1();
            istag1 = true;
            ChangeTime = 0.0f;
            Debug.Log("Florusに変更しました");
        }

        // "Odor"タグに切り替える条件
        if (!istag2 && Input.GetKeyDown(KeyCode.S))
        {
            Audio.PlayOneShot(OdorSE);
            SwitchTag2();
            istag2 = true;
            ChangeTime = 0.0f;
            Debug.Log("Odorに変更しました");
        }
    }

    // "Florus"タグに切り替えるメソッド
    private void SwitchTag1()
    {
        this.tag = "Florus";
        ChangeChildTags("Florus");
        ChangeChildLayers("Default");
    }

    // "Odor"タグに切り替えるメソッド
    private void SwitchTag2()
    {
        this.tag = "Odor";
        ChangeChildTags("Odor");
        ChangeChildLayers("Odor");
    }

    // 子オブジェクトのタグを切り替えるメソッド
    private void ChangeChildTags(string newTag)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.tag = newTag;
            // Debug.Log(child.gameObject.name);
        }
    }

    private void ChangeChildLayers(string newLayerName)
    {
        int newLayer = LayerMask.NameToLayer(newLayerName);
        foreach (Transform child in transform)
        {
            child.gameObject.layer = newLayer;
        }
    }

    // タグの切り替えが終了したときに元のタグに戻すメソッド
    void EndChangeTag()
    {
        istag1 = false;
        istag2 = false;
        this.tag = "Player";
        ChangeChildTags("Player");
        ChangeChildLayers("Default");
    }

    void GetLayersRecursive(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // 子オブジェクトのレイヤーを取得
            int layer = child.gameObject.layer;
            Debug.Log(child.name + " のレイヤー: " + LayerMask.LayerToName(layer));

            // 再帰的に子オブジェクトのレイヤーを取得
            GetLayersRecursive(child);
        }
    }
}
