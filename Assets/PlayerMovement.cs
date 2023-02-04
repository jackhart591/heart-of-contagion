using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    
    void Update() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!isFacingRight && horizontal > 0f) {
            Flip();
        } else if (isFacingRight && horizontal < 0f) {
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext ctx) {
        if (ctx.performed && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (ctx.canceled && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;

        localScale.x *= -1f;

        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext ctx) {
        horizontal = ctx.ReadValue<Vector2>().x;
    }
}
