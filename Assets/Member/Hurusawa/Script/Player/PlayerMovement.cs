using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 移動速度
    public float moveSpeed = 5.0f;

    // ジャンプ速度
    public float jumpForce = 10.0f;
    public float JumpUpPoint = 5.0f;

    private Rigidbody2D rb;

    // ジャンプ制限
    // 地面とキノコのレイヤーを取得
    public LayerMask groundLayer;
    public LayerMask mushroomLayer;
    public LayerMask DestroyobjLayer;
    public float rayDistance = 1.3f;
    private bool isGrounded = true;


    //スプライトを切り替えるオブジェクトの取得
    public GameObject ChangeObject;
    //元のスプライト
    public Sprite initialSprite;
    //切り替えるスプライト
    public Sprite newSprite;
    private bool wasGrounded = true;

    //SEを取得
    public AudioSource Audio;
    public AudioClip JumpSE1;
    public AudioClip JumpSE2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // レイキャストを使用して地面とキノコの判定を取得
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);
        RaycastHit2D hitMushroom = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, mushroomLayer);
        RaycastHit2D hitDestroyobj = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, DestroyobjLayer);

        // デバッグログ+スプライトの変更
        if (hitGround.collider != null  ||hitMushroom.collider != null || hitDestroyobj.collider != null)
        {
            if (!wasGrounded)
            {
                ChangeSprite2();
            }
            //Debug.Log("Hit ground: " + hitGround.collider.gameObject.name);
        }

        // 地面またはキノコに接地しているか判定
        isGrounded = hitGround.collider != null || hitMushroom.collider != null || hitDestroyobj.collider != null;


        // 左右の移動
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // ジャンプ時の処理
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            ChangeSprite();
            float currentJumpForce = jumpForce;

            if (hitMushroom.collider != null)
            {
                if (gameObject.layer == LayerMask.NameToLayer("Odor"))
                {
                    currentJumpForce *= JumpUpPoint;
                }
            }
            rb.AddForce(Vector2.up * currentJumpForce, ForceMode2D.Impulse);
            Audio.PlayOneShot(JumpSE1);
            //Audio.PlayOneShot(JumpSE2);
        }

        wasGrounded = isGrounded;
    }

    // スプライトを切り替える
    void ChangeSprite()
    {
        SpriteRenderer spriteRenderer = ChangeObject.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
    }

    //切り替えたスプライトを元のものに戻す
    void ChangeSprite2()
    {
        SpriteRenderer spriteRenderer = ChangeObject.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && initialSprite != null)
        {
            spriteRenderer.sprite = initialSprite;
        }
    }
}
