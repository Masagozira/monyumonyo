using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSprint : MonoBehaviour
{
    //position,rotation,scale��z��ŕۑ����Č�ŕۑ��������̂��Ăяo��
    private List<Vector3> _defaultBonePosition = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        //position��z��ŕۑ�
        Transform[] _arrT = GetComponentsInChildren<Transform>();
        foreach (Transform t in _arrT)
        {
            _defaultBonePosition.Add(t.localPosition);
        }
    }

    public void SetDafaultTransform()
    {
        //�Ăяo���֐�
    }
}
/*
 RigidBody:Dinamic->Kinematic
 Collider:������
 �S�Ă�Springjoint:������
 �ォ�珇�ɏ�������
 */