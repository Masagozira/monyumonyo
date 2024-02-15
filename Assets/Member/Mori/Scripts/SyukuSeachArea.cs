using UnityEngine;
/// <summary>
/// ���G�͈͂�Bool�Ǘ�
/// </summary>
public class SyukuSeachArea : MonoBehaviour
{
    //���ŃX�N���v�g�擾
    [SerializeField]
    private SyukuHands _syukuHands;
    //�v���C���[�̃R���C�_�[�擾�A���ʗp
    private CircleCollider2D _marimoC;
    private GameObject _marimoG;

    private void Start()
    {
        _marimoG = GameObject.Find("bone_11");
        _marimoC = _marimoG.GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// ���G�͈͓��ɓ�������
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision ==_marimoC)
        {
            _syukuHands._isHandsMove = true;
        }

    }
    /// <summary>
    /// ���G�͈͓�����o����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        _syukuHands._isHandsMove = false;
    }
}
