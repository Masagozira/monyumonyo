using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーのタグ変更エフェクト管理スクリプト
/// 以下の関数を呼び出すことでエフェクトが再生される:
/// ・Florus：FloEffect()
/// ・Odor：OdrEffect()
/// ・失敗エフェクト：FailEffect()
/// </summary>
public class PlayerEffect : MonoBehaviour
{
    // -------------------------
    // エフェクト管理
    // -------------------------

    [SerializeField, Header("Florusエフェクト")]
    private ParticleSystem _florusEffect;
    private ParticleSystem.MainModule _florusEffectTime;

    [SerializeField, Header("Odorエフェクト")]
    private ParticleSystem _odorEffect;
    private ParticleSystem.MainModule _odorEffectTime;

    [SerializeField, Header("タグ失敗時エフェクト")]
    private ParticleSystem _failEffect;
    private ParticleSystem.MainModule _failEffectTime;

    [SerializeField, Header("Florus効果時間")]
    private float _floEffectTime = 4f;

    [SerializeField, Header("Odor効果時間")]
    private float _OdrEffectTime = 3f;

    [SerializeField]
    private PlayerInput _playerinput;

    // -------------------------
    //  タグ変更・クールダウン管理
    // -------------------------

    private bool istag1 = false; // タグが"Florus"なら true
    private bool istag2 = false; // タグが"Odor"なら true

    public float ChangeTime1 = 4f;  // Florus継続時間
    public float ChangeTime2 = 3f;  // Odor継続時間
    public float ChangeTime = 0.0f; // 経過時間計測

    private float CooldownTime = 2f; // エフェクト切替後のクールダウン
    public float NonChangeTime = 0.0f;
    private float NonCooldownTime = 0f;

    private bool isInCooldown = false; // クールダウン中かどうか

    private void Start()
    {
        _playerinput = GetComponent<PlayerInput>();

        // パーティクル設定
        _florusEffectTime = _florusEffect.main;
        _odorEffectTime = _odorEffect.main;
        _failEffectTime = _failEffect.main;

        // エフェクト時間を設定
        _florusEffectTime.duration = _floEffectTime;
        _odorEffectTime.duration = _OdrEffectTime;
        _failEffectTime.duration = 1;

        Cursor.visible = false;

        // 子オブジェクトのレイヤー調査（デバッグ）
        Transform parentTransform = transform;
        GetLayersRecursiveEff(parentTransform);
    }

    private void Update()
    {
        if (Menu.IsMenuActive) return;

        // -----------------
        // クールダウン中
        // -----------------
        if (isInCooldown)
        {
            NonChangeTime += Time.deltaTime;

            if ((_playerinput.actions["Florus"].triggered ||
                 _playerinput.actions["Odor"].triggered) 
                && NonChangeTime >= NonCooldownTime)
            {
                NonChangeTime = 0.0f;
                FailEffect();
            }
            return;
        }

        // -----------------
        // 効果時間中
        // -----------------
        if (istag1 || istag2)
        {
            ChangeTime += Time.deltaTime;

            // 効果時間終了
            if (ChangeTime >= (istag1 ? ChangeTime1 : ChangeTime2))
            {
                EndChangeTagEff();
                StartCooldownEff();
            }
            return;
        }

        // -----------------
        // Florus への変更
        // -----------------
        if (!istag1 && _playerinput.actions["Florus"].triggered)
        {
            FloEffect();
            istag1 = true;
            ChangeTime = 0.0f;
            Debug.Log("Florus");
        }

        // -----------------
        // Odor への変更
        // -----------------
        if (!istag2 && _playerinput.actions["Odor"].triggered)
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

    /// <summary>Florusエフェクト</summary>
    public void FloEffect()
    {
        Vector3 pos = transform.position;

        Instantiate(_florusEffect, pos, Quaternion.identity, transform);

        Debug.Log("Effect:Flo");

        _florusEffect.Play();
        _florusEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    /// <summary>Odorエフェクト</summary>
    public void OdrEffect()
    {
        Vector3 pos = transform.position;

        Instantiate(_odorEffect, pos, Quaternion.identity, transform);

        Debug.Log("Effect:Odr");

        _odorEffect.Play();
        _odorEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    /// <summary>失敗エフェクト</summary>
    public void FailEffect()
    {
        Vector3 pos = transform.position;

        Instantiate(_failEffect, pos, Quaternion.identity, transform);

        Debug.Log("Effect:Non");

        _failEffect.Play();
        _failEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    /// <summary>子オブジェクトのレイヤー出力（デバッグ）</summary>
    void GetLayersRecursiveEff(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Debug.Log(child.name + " のレイヤー: " + LayerMask.LayerToName(child.gameObject.layer));
            GetLayersRecursiveEff(child);
        }
    }

    /// <summary>タグ状態をリセット</summary>
    void EndChangeTagEff()
    {
        istag1 = false;
        istag2 = false;
    }

    /// <summary>クールダウン開始</summary>
    void StartCooldownEff()
    {
        StartCoroutine(CooldownCoroutineEff());
    }

    /// <summary>クールダウン処理</summary>
    IEnumerator CooldownCoroutineEff()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(CooldownTime);
        isInCooldown = false;
    }
}
