using System.Collections;
using UnityEngine;

/// <summary>
/// 匂い変更エフェクトの管理スクリプト
/// 各関数呼び出しで動作する
/// いい匂いのエフェクト：FloEffect();
/// まずい匂いのエフェクト：OdrEffect();
/// 匂いのエフェクト変更失敗：FailEffect();
/// </summary>
public class PlayerEffect : MonoBehaviour
{
    //いい匂いのエフェクト
    [SerializeField, Header("いい匂いのエフェクト")]
    private ParticleSystem _florusEffect;
    private ParticleSystem.MainModule _florusEffectTime;
    //まずい匂いのエフェクト
    [SerializeField, Header("まずい匂いのエフェクト")]
    private ParticleSystem _odorEffect;
    private ParticleSystem.MainModule _odorEffectTime;
    //匂いのエフェクト変更失敗
    [SerializeField, Header("匂いの変更失敗エフェクト")]
    private ParticleSystem _failEffect;
    private ParticleSystem.MainModule _failEffectTime;

    [SerializeField, Header("いい匂い効果時間")]
    private float _floEffectTime = 4f;
    [SerializeField, Header("まずい匂い効果時間")]
    private float _OdrEffectTime = 3f;

    // エフェクト時間管理
    private bool istag1 = false;  //プレイヤータグが"Florus"のときtrue
    private bool istag2 = false;  //プレイヤータグが"Odor"のときtrue
    public float ChangeTime1 = 4f;  // いい匂い効果時間
    public float ChangeTime2 = 3f;  // まずい匂い効果時間
    public float ChangeTime = 0.0f;  // 時間計測用
    private float CooldownTime = 2f;  // エフェクト切り替えのクールタイム
    public float NonChangeTime = 0.0f;  // 時間計測用
    private float NonCooldownTime = 0f;  // エフェクト切り替えのクールタイム
    private bool isInCooldown = false;  // 今クールタイム中かどうか

    //初期設定
    private void Start()
    {
        //匂い系のコンポーネントの代入
        _florusEffectTime = _florusEffect.main;
        _odorEffectTime = _odorEffect.main;
        _failEffectTime = _failEffect.main;

        //匂い系の効果時間変数の代入
        _florusEffectTime.duration = _floEffectTime;
        _odorEffectTime.duration = _OdrEffectTime;
        _failEffectTime.duration = 1;

        Cursor.visible = false;
        // プレイヤー検索
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in objs)
        {
            //Debug.Log(obj.gameObject.name);
        }
        Transform parentTransform = transform; // プレイヤーを親に設定
        GetLayersRecursiveEff(parentTransform);
    }

    //エフェクト発生処理
    private void Update()
    {
        if (isInCooldown)
        {
            NonChangeTime += Time.deltaTime;
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && NonChangeTime >= NonCooldownTime)
            {
                NonChangeTime = 0.0f;
                FailEffect();
            }
            // クールタイム中は返す
            return;
        }
        // 何らかの匂いがあるとき
        if (istag1 || istag2)
        {
            ChangeTime += Time.deltaTime;
            // 指定した匂いの秒数経過した時
            if (ChangeTime >= (istag1 ? ChangeTime1 : ChangeTime2))
            {
                EndChangeTagEff();  //タグboolをfalseにする
                StartCooldownEff();  // クールタイム開始
            }
            return;
        }
        // "Florus"に切り替える
        if (!istag1 && Input.GetKeyDown(KeyCode.W))
        {
            FloEffect();
            istag1 = true;
            ChangeTime = 0.0f;
            Debug.Log("Florus");
        }
        // "Odor"に切り替える
        if (!istag2 && Input.GetKeyDown(KeyCode.S))
        {
            OdrEffect();
            istag2 = true;
            ChangeTime = 0.0f;
            Debug.Log("Odor");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            FailEffect();
        }
    }

    /// <summary>
    /// いい匂い
    /// </summary>
    public void FloEffect()
    {
        Vector3 playerPosition;
        // プレイヤー位置取得
        playerPosition = transform.position;

        // いい匂いインスタンス
        ParticleSystem floEffectInstance = Instantiate(_florusEffect, playerPosition, Quaternion.identity, transform);
        Debug.Log("Effect:Flo");
        //Instantiate(_florusEffect, transform);
        _florusEffect.Play();
        _florusEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);

    }
    /// <summary>
    /// まずい匂い
    /// </summary>
    public void OdrEffect()
    {
        Vector3 playerPosition;
        // プレイヤー位置取得
        playerPosition = transform.position;
        // まずい匂いインスタンス
        ParticleSystem floEffectInstance = Instantiate(_odorEffect, playerPosition, Quaternion.identity, transform);

        Debug.Log("Effect:Odr");
        //Instantiate(_odorEffect, transform);
        _odorEffect.Play();
        _odorEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    /// <summary>
    /// 匂い切り替え失敗
    /// </summary>
    public void FailEffect()
    {
        Vector3 playerPosition;
        // プレイヤー位置取得
        playerPosition = transform.position;

        // 失敗エフェクトインスタンス
        ParticleSystem floEffectInstance = Instantiate(_failEffect, playerPosition, Quaternion.identity, transform);

        Debug.Log("Effect:Non");
        //Instantiate(_failEffect, transform);
        _failEffect.Play();
        _failEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
    void GetLayersRecursiveEff(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // 子オブジェクトのレイヤー取得
            int layer = child.gameObject.layer;
            Debug.Log(child.name + " のレイヤー： " + LayerMask.LayerToName(layer));

            GetLayersRecursiveEff(child);
        }
    }
    // タグリセット
    void EndChangeTagEff()
    {
        istag1 = false;
        istag2 = false;
    }
    // エフェクトクールダウン開始
    void StartCooldownEff()
    {
        StartCoroutine(CooldownCoroutineEff());
    }
    // エフェクトクールダウン
    IEnumerator CooldownCoroutineEff()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(CooldownTime);
        isInCooldown = false;
    }
}