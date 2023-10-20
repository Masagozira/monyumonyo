using UnityEngine;
using UnityEditor;

public class MoveAnimation : MonoBehaviour
{
    private Animator animator;  // アニメーターコンポーネント取得用
    //[SerializeField]
    //private HumanContlloler _humanContlloler;

    // Start is called before the first frame update
    void Start()
    {
        // アニメーターコンポーネント取得
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (_humanContlloler.DragFlg ==true && Input.GetKey(KeyCode.Mouse0))
        //{
        //    animator.SetBool("Triangle", true); // アニメーション切り替え
        //}
        //else
        //{
        //    animator.SetBool("Triangle", false);
        //}
    }
}