using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 匂い切り替え時のエフェクトの変更
/// 呼びだす関数
/// ・いい匂い：FloEffect();
/// ・まずい匂い：OdrEffect();
/// ・匂い変更失敗：FailEffect();
/// </summary>
public class PlayerEffect : MonoBehaviour
{
    //いい匂いのエフェクト
    [SerializeField,Header("いい匂いのエフェクト")]
    private ParticleSystem _florusEffect;
    private ParticleSystem.MainModule _florusEffectTime;
    //まずい匂いのエフェクト
    [SerializeField, Header("まずい匂いのエフェクト")]
    private ParticleSystem _odorEffect;
    private ParticleSystem.MainModule _odorEffectTime;
    //匂い切り替え失敗のエフェクト
    [SerializeField, Header("匂い切り替え失敗のエフェクト")]
    private ParticleSystem _failEffect;
    private ParticleSystem.MainModule _failEffectTime;

    [SerializeField, Header("匂い継続時間")]
    private float _effectTime = 3f;

    //初期設定
    private void Start()
    {
        //エフェクト継続時間の変数にそれぞれのエフェクトを指定
        _florusEffectTime = _florusEffect.main;
        _odorEffectTime = _odorEffect.main;
        _failEffectTime = _failEffect.main;

        //匂い継続時間を代入
        _florusEffectTime.duration = _effectTime;
        _odorEffectTime.duration = _effectTime;
        _failEffectTime.duration = 1;
    }

    //試行用、消していい
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            FloEffect();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            OdrEffect();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            FailEffect();
        }
    }

    //匂い変更した時のプレイヤーのエフェクト
    //プレイヤーの方で呼んでもらう

    /// <summary>
    /// いい匂いに変更
    /// </summary>
    private void FloEffect()
    {
        //いい匂いに変更
        Debug.Log("Effect:Flo");
        Instantiate(_florusEffect, transform);
        _florusEffect.Play();
        _florusEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);

    }
    /// <summary>
    /// まずい匂いに変更
    /// </summary>
    private void OdrEffect()
    {
        Debug.Log("Effect:Odr");
        Instantiate(_odorEffect, transform);
        _odorEffect.Play();
        _odorEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    /// <summary>
    /// 匂い変更失敗
    /// </summary>
    private void FailEffect()
    {
        Debug.Log("Effect:Non");
        Instantiate(_failEffect, transform);
        _failEffect.Play();
        _failEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

}