using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    // This determines health and collisions for enemy characters
    // very general so can be applied to ANY enemy

    [SerializeField] float HealthVal = 3;
   public bool invincible = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.layer == 8)
        {
            Debug.Log("Hit By Weapon");
            if (invincible == false)
            {
                Hurt();

            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision) // when the enemy is hit by a player bullet
    {
        if(collision.gameObject.tag == "PlayerWeapon")
        {
            Debug.Log("Hit By Weapon");
            if(invincible == false)
            {
                Hurt();

            }
            
        }
    }

    public void Damage(float damage) {
        HealthVal -= damage;
        Hurt();
    }

    void Hurt()
    {
        if (HealthVal >= 1)
        {
            HealthVal -= 1;
        }
        else
        {
            Destroy(gameObject); // very abstract- effects can be added of course
        }



    }
}
