using UnityEngine;
/// <summary>
/// ���G�͈͂�Bool�Ǘ�
/// </summary>
public class MaruFrontSeach : MonoBehaviour
{
    //�v���C���[�̃R���C�_�[�擾�A���ʗp
    [SerializeField]
    private CircleCollider2D _marimo;
    //�v���C���[�̃R���C�_�[�擾�A���ʗp
    [SerializeField]
    private Maru _maru;

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
