using UnityEngine;
using UnityEditor;

public class MoveAnimation : MonoBehaviour
{
    private Animator animator;  // �A�j���[�^�[�R���|�[�l���g�擾�p
    //[SerializeField]
    //private HumanContlloler _humanContlloler;

    // Start is called before the first frame update
    void Start()
    {
        // �A�j���[�^�[�R���|�[�l���g�擾
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (_humanContlloler.DragFlg ==true && Input.GetKey(KeyCode.Mouse0))
        //{
        //    animator.SetBool("Triangle", true); // �A�j���[�V�����؂�ւ�
        //}
        //else
        //{
        //    animator.SetBool("Triangle", false);
        //}
    }
}