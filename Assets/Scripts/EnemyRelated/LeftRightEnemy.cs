using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightEnemy : MonoBehaviour
{
    // Enemy script that moves left and right.. with a player detection system as well.
    [SerializeField] float speed = 3f;
    [SerializeField] float AgroRange = 3f;
    private float OGSpeed;
    [SerializeField] GameObject plr;
    public Rigidbody2D rb;
    public LayerMask groundLayers;
    public Transform checkerObject;
    bool canFlip = false;
    bool facingRight = true; // just a really strange way of flipping the sprite
    [SerializeField] int AIType = 0; // simple AI types for player actions
    RaycastHit2D hit; // the raycast shot from the checkerObject

    void Start()
    {
        OGSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.Raycast(checkerObject.position, -transform.up, 1f, groundLayers);
        if (plr != null)
        {
            IfPlayerSpotted(AgroRange);
        }
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
    void IfPlayerSpotted(float AgroRange) // for special abilities, such as shooting or special attacks
    {
        float dist = Vector2.Distance(plr.transform.position, gameObject.transform.position);
        if(dist <= AgroRange)
        {
            switch (AIType)
            {


                case 0:
                    Debug.Log("Spotted");
                    break;
                case 1:
                    speed = OGSpeed * 2f;

                    break;
            }

        }
        else
        {
            switch (AIType)
            {


                case 0:
                    Debug.Log("Not spotted");
                    break;
                case 1:
                    speed = OGSpeed;

                    break;
            }

        }


    }
}
