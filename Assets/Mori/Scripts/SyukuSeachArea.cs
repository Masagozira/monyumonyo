using UnityEngine;
/// <summary>
/// ���G�͈͂�Bool�Ǘ�
/// </summary>
public class SyukuSeachArea : MonoBehaviour
{
    //���ŃX�N���v�g�擾
    [SerializeField]
    private SyukuHands _syukuHands;

    /// <summary>
    /// ���G�͈͓��ɓ�������
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _syukuHands._isHandsMove = true;
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
