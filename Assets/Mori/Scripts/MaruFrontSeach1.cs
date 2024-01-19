using UnityEngine;
/// <summary>
/// ���G�͈͂�Bool�Ǘ�
/// </summary>
public class MaruFrontSeach1 : MonoBehaviour
{
    //�v���C���[�̃R���C�_�[�擾�A���ʗp
    private CircleCollider2D _marimo;
    private GameObject _marimoG;
    //�v���C���[�̃R���C�_�[�擾�A���ʗp
    [SerializeField]
    private StopMaru _maru;
    private void Start()
    {
        _marimoG = GameObject.Find("bone_11");
        _marimo = _marimoG.GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// ���G�͈͓��ɓ�������
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == _marimo)
        {
            _maru._playerFrontHere = true;
        }

    }
    /// <summary>
    /// ���G�͈͓�����o����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == _marimo)
        {
            _maru._playerFrontHere = false;
        }
    }
}
