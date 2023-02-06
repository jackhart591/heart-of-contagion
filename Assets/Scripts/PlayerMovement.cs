using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
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
    public Vector3 checkpoint;
    public bool inRoot;
    public bool RootPower = false;
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool shooting = false;

    private void Start() {
        if (Persistent.PlayerCheckpoint != Vector3.zero)
            transform.position = Persistent.PlayerCheckpoint;
    }
    
    private void Update() {
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
            if(RootPower == true)
            {

                rb.velocity = new Vector2(rb.velocity.x, jumpingPower * 2);
                RootPower = false;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;


            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            }
            
        }

        if (ctx.canceled && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public bool IsGrounded() {
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
        StartCoroutine(ShootInternal(ctx));
    }

    private IEnumerator ShootInternal(InputAction.CallbackContext ctx) {
        if (shooting) {
            yield break;
        }

        List<GameObject> objectsTemp = new List<GameObject>();

        foreach (GameObject obj in collidingObjs) {
            objectsTemp.Add(obj);
        }

        shooting = true;
        if (cooldown <= 0) {
            GetComponent<AudioSource>().Play();
            foreach(GameObject obj in objectsTemp) {
                if (Physics2D.Raycast(transform.position, (transform.position - obj.transform.position), maxGunRange))
                    if (obj.TryGetComponent(out EnemyMain script) && cooldown <= 0) {
                        script.Damage(gunDamage);
                    }

            }
            yield return new WaitForSeconds(0.8f);
        }
        shooting = false;
    }
}
