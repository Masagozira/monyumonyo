using UnityEngine;

public class Move : MonoBehaviour
{
    //�����������I�u�W�F�N�g
    [SerializeField,Header("�����������I�u�W�F�N�g")]
    private GameObject _human;
    //�I�u�W�F�N�g�������œ����X�s�[�h
    [SerializeField, Header("�I�u�W�F�N�g����������")]
    private int _speed = 5;
    //�I�u�W�F�N�g�̖ړI�n��ۑ�
    private Vector3 _movePosition;

    //�����_���ɓ����I�u�W�F�N�g�̉��͈�
    [SerializeField, Header("�����_������̉E���")]
    private float X_Max = 7f;
    [SerializeField, Header("�����_������̍����")]
    private float X_Min = -7f;
    [SerializeField, Header("�����_������̏���")]
    private float Y_Max = 4f;
    [SerializeField, Header("�����_������̉����")]
    private float Y_Min = -4f;

    void Start()
    {
        //�I�u�W�F�N�g�̖ړI�n��ݒ�
        _movePosition = moveRandomPosition();
    }

    void Update()
    {
        //�I�u�W�F�N�g���ړI�n�ɓ��B�����Ƃ�
        if (_movePosition == _human.transform.position)
        {
            //�ړI�n���Đݒ�
            _movePosition = moveRandomPosition();
        }
        //�I�u�W�F�N�g���ړI�n�Ɉړ�
        this._human.transform.position 
            = Vector3.MoveTowards(_human.transform.position
                                , _movePosition
                                , _speed * Time.deltaTime);
    }
    /// <summary>
    /// �ړI�n�𐶐��Ax��y�̃|�W�V�����������_���ɒl���擾
    /// </summary>
    /// <returns></returns>
    private Vector3 moveRandomPosition()
    {
        Vector3 randomPosi = new Vector3(Random.Range(X_Min, X_Max), Random.Range(Y_Min, Y_Max), Random.Range(0, 0));
        return randomPosi;
    }
}