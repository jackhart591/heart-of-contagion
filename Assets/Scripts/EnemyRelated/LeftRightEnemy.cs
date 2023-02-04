using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightEnemy : MonoBehaviour
{
    // Enemy script that moves left and right
    [SerializeField] float speed = 3f;
    [SerializeField] GameObject plr;
    public Rigidbody2D rb;
    public LayerMask groundLayers;
    public Transform checkerObject;
    bool canFlip = false;
    bool facingRight = true; // just a really strange way of flipping the sprite

    RaycastHit2D hit; // the raycast shot from the checkerObject

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.Raycast(checkerObject.position, -transform.up, 1f, groundLayers);

    }
    private void FixedUpdate()
    {
        if (hit.collider != false)
        {

            if (facingRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);

                canFlip = true;

            }
            else
            {

                rb.velocity = new Vector2(-speed, rb.velocity.y);
                canFlip = true;
            }
        }
        else
        {
            if (canFlip == true)
            {
                facingRight = !facingRight;

                Flip();

            }

        }
        void Flip()
        {


            canFlip = false;
            Vector3 CurrentScale = gameObject.transform.localScale;
            CurrentScale.x *= -1;
            gameObject.transform.localScale = CurrentScale;
        }
    }

}
