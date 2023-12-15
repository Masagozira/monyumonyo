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
    public LayerMask groundLayer;
    public LayerMask mushroomLayer;
    public float rayDistance = 1.3f;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);
        RaycastHit2D hitMushroom = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, mushroomLayer);

        if (hitGround.collider != null)
        {
            Debug.Log("Hit ground: " + hitGround.collider.gameObject.name);
        }

        if (hitMushroom.collider != null)
        {
            Debug.Log("Hit mushroom: " + hitMushroom.collider.gameObject.name);
        }

        isGrounded = hitGround.collider != null || hitMushroom.collider != null; 

        float horizontalInput = Input.GetAxis("Horizontal");

        // 左右の移動
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // ジャンプ
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            float currentJumpForce = hitMushroom.collider != null ? jumpForce * JumpUpPoint : jumpForce; 
            rb.AddForce(Vector2.up * currentJumpForce, ForceMode2D.Impulse);
        }
    }
}
