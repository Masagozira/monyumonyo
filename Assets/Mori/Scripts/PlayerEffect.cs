using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����؂�ւ����̃G�t�F�N�g�̕ύX
/// �Ăт����֐�
/// �E���������FFloEffect();
/// �E�܂��������FOdrEffect();
/// �E�����ύX���s�FFailEffect();
/// </summary>
public class PlayerEffect : MonoBehaviour
{
    //���������̃G�t�F�N�g
    [SerializeField,Header("���������̃G�t�F�N�g")]
    private ParticleSystem _florusEffect;
    private ParticleSystem.MainModule _florusEffectTime;
    //�܂��������̃G�t�F�N�g
    [SerializeField, Header("�܂��������̃G�t�F�N�g")]
    private ParticleSystem _odorEffect;
    private ParticleSystem.MainModule _odorEffectTime;
    //�����؂�ւ����s�̃G�t�F�N�g
    [SerializeField, Header("�����؂�ւ����s�̃G�t�F�N�g")]
    private ParticleSystem _failEffect;
    private ParticleSystem.MainModule _failEffectTime;

    [SerializeField, Header("�����p������")]
    private float _effectTime = 3f;

    //�����ݒ�
    private void Start()
    {
        //�G�t�F�N�g�p�����Ԃ̕ϐ��ɂ��ꂼ��̃G�t�F�N�g���w��
        _florusEffectTime = _florusEffect.main;
        _odorEffectTime = _odorEffect.main;
        _failEffectTime = _failEffect.main;

        //�����p�����Ԃ���
        _florusEffectTime.duration = _effectTime;
        _odorEffectTime.duration = _effectTime;
        _failEffectTime.duration = 1;
    }

    //���s�p�A�����Ă���
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

    //�����ύX�������̃v���C���[�̃G�t�F�N�g
    //�v���C���[�̕��ŌĂ�ł��炤

    /// <summary>
    /// ���������ɕύX
    /// </summary>
    private void FloEffect()
    {
        //���������ɕύX
        Debug.Log("Effect:Flo");
        Instantiate(_florusEffect, transform);
        _florusEffect.Play();
        _florusEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);

    }
    /// <summary>
    /// �܂��������ɕύX
    /// </summary>
    private void OdrEffect()
    {
        Debug.Log("Effect:Odr");
        Instantiate(_odorEffect, transform);
        _odorEffect.Play();
        _odorEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    /// <summary>
    /// �����ύX���s
    /// </summary>
    private void FailEffect()
    {
        Debug.Log("Effect:Non");
        Instantiate(_failEffect, transform);
        _failEffect.Play();
        _failEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

}