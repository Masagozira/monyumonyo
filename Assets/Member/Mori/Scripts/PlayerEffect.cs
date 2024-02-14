using System.Collections;
using UnityEngine;

/// <summary>
/// �����ύX�G�t�F�N�g�̊Ǘ��X�N���v�g
/// �e�֐��Ăяo���œ��삷��
/// ���������̃G�t�F�N�g�FFloEffect();
/// �܂��������̃G�t�F�N�g�FOdrEffect();
/// �����̃G�t�F�N�g�ύX���s�FFailEffect();
/// </summary>
public class PlayerEffect : MonoBehaviour
{
    //���������̃G�t�F�N�g
    [SerializeField, Header("���������̃G�t�F�N�g")]
    private ParticleSystem _florusEffect;
    private ParticleSystem.MainModule _florusEffectTime;
    //�܂��������̃G�t�F�N�g
    [SerializeField, Header("�܂��������̃G�t�F�N�g")]
    private ParticleSystem _odorEffect;
    private ParticleSystem.MainModule _odorEffectTime;
    //�����̃G�t�F�N�g�ύX���s
    [SerializeField, Header("�����̕ύX���s�G�t�F�N�g")]
    private ParticleSystem _failEffect;
    private ParticleSystem.MainModule _failEffectTime;

    [SerializeField, Header("�����������ʎ���")]
    private float _floEffectTime = 4f;
    [SerializeField, Header("�܂����������ʎ���")]
    private float _OdrEffectTime = 3f;

    // �G�t�F�N�g���ԊǗ�
    private bool istag1 = false;  //�v���C���[�^�O��"Florus"�̂Ƃ�true
    private bool istag2 = false;  //�v���C���[�^�O��"Odor"�̂Ƃ�true
    public float ChangeTime1 = 4f;  // �����������ʎ���
    public float ChangeTime2 = 3f;  // �܂����������ʎ���
    public float ChangeTime = 0.0f;  // ���Ԍv���p
    private float CooldownTime = 2f;  // �G�t�F�N�g�؂�ւ��̃N�[���^�C��
    public float NonChangeTime = 0.0f;  // ���Ԍv���p
    private float NonCooldownTime = 0f;  // �G�t�F�N�g�؂�ւ��̃N�[���^�C��
    private bool isInCooldown = false;  // ���N�[���^�C�������ǂ���

    //�����ݒ�
    private void Start()
    {
        //�����n�̃R���|�[�l���g�̑��
        _florusEffectTime = _florusEffect.main;
        _odorEffectTime = _odorEffect.main;
        _failEffectTime = _failEffect.main;

        //�����n�̌��ʎ��ԕϐ��̑��
        _florusEffectTime.duration = _floEffectTime;
        _odorEffectTime.duration = _OdrEffectTime;
        _failEffectTime.duration = 1;

        Cursor.visible = false;
        // �v���C���[����
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in objs)
        {
            //Debug.Log(obj.gameObject.name);
        }
        Transform parentTransform = transform; // �v���C���[��e�ɐݒ�
        GetLayersRecursiveEff(parentTransform);
    }

    //�G�t�F�N�g��������
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
            // �N�[���^�C�����͕Ԃ�
            return;
        }
        // ���炩�̓���������Ƃ�
        if (istag1 || istag2)
        {
            ChangeTime += Time.deltaTime;
            // �w�肵�������̕b���o�߂�����
            if (ChangeTime >= (istag1 ? ChangeTime1 : ChangeTime2))
            {
                EndChangeTagEff();  //�^�Obool��false�ɂ���
                StartCooldownEff();  // �N�[���^�C���J�n
            }
            return;
        }
        // "Florus"�ɐ؂�ւ���
        if (!istag1 && Input.GetKeyDown(KeyCode.W))
        {
            FloEffect();
            istag1 = true;
            ChangeTime = 0.0f;
            Debug.Log("Florus");
        }
        // "Odor"�ɐ؂�ւ���
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
    /// ��������
    /// </summary>
    public void FloEffect()
    {
        Vector3 playerPosition;
        // �v���C���[�ʒu�擾
        playerPosition = transform.position;

        // ���������C���X�^���X
        ParticleSystem floEffectInstance = Instantiate(_florusEffect, playerPosition, Quaternion.identity, transform);
        Debug.Log("Effect:Flo");
        //Instantiate(_florusEffect, transform);
        _florusEffect.Play();
        _florusEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);

    }
    /// <summary>
    /// �܂�������
    /// </summary>
    public void OdrEffect()
    {
        Vector3 playerPosition;
        // �v���C���[�ʒu�擾
        playerPosition = transform.position;
        // �܂��������C���X�^���X
        ParticleSystem floEffectInstance = Instantiate(_odorEffect, playerPosition, Quaternion.identity, transform);

        Debug.Log("Effect:Odr");
        //Instantiate(_odorEffect, transform);
        _odorEffect.Play();
        _odorEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    /// <summary>
    /// �����؂�ւ����s
    /// </summary>
    public void FailEffect()
    {
        Vector3 playerPosition;
        // �v���C���[�ʒu�擾
        playerPosition = transform.position;

        // ���s�G�t�F�N�g�C���X�^���X
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
            // �q�I�u�W�F�N�g�̃��C���[�擾
            int layer = child.gameObject.layer;
            Debug.Log(child.name + " �̃��C���[�F " + LayerMask.LayerToName(layer));

            GetLayersRecursiveEff(child);
        }
    }
    // �^�O���Z�b�g
    void EndChangeTagEff()
    {
        istag1 = false;
        istag2 = false;
    }
    // �G�t�F�N�g�N�[���_�E���J�n
    void StartCooldownEff()
    {
        StartCoroutine(CooldownCoroutineEff());
    }
    // �G�t�F�N�g�N�[���_�E��
    IEnumerator CooldownCoroutineEff()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(CooldownTime);
        isInCooldown = false;
    }
}