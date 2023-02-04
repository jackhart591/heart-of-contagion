using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform playerLight;
    [SerializeField] private Transform shootingRange;
    public List<GameObject> collidingObjs;
    public float maxGunRange = 8f;
    public float gunDamage = 1f;
    public float cooldown = 0.8f;

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

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        Vector3 rotation = mousePos - playerLight.position;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg - 90;

        playerLight.rotation = Quaternion.Euler(0, 0, rotz);
        cooldown -= Time.deltaTime;
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

    public void Shoot(InputAction.CallbackContext ctx) {
        try {
            foreach(GameObject obj in collidingObjs) {
                if (Physics2D.Raycast(transform.position, (transform.position - obj.transform.position), maxGunRange))
                    if (obj.TryGetComponent(out EnemyMain script) && cooldown <= 0) {
                        script.Damage(gunDamage);
                        cooldown = 0.8f;
                    }
            }
        } catch {}
    }
}
