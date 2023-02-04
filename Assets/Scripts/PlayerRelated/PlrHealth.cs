using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlrHealth : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] bool db = false;
    [SerializeField] int MaxHealth = 5;
    private bool dead = false;
    [SerializeField] int Health;
    [SerializeField] SpriteRenderer spritey;
    //EventSystem Esys;


    private void Start()
    {
        Health = MaxHealth;
    }
    private void Hurt()
    {
        if(Health > 1)
        {

            Health -= 1;
            StartCoroutine(invinciblity());

        }
        else if(dead == false)
        {
            dead = true;
            StartCoroutine(gameOver());
        }


    }
    public int GetHealth()
    {
        return Health;

    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
           

            if (db == false)
            {
                Debug.Log("OK");
                Hurt();

            }


        }
    }

    IEnumerator gameOver()
    {

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator invinciblity()
    {
        gameObject.layer = 9;
        db = true;
        if (spritey != null)
        {
            for(int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(.2f - (.05f * i));

                spritey.color = Color.red;
                yield return new WaitForSeconds(.2f - (.05f * i)); 
                spritey.color = Color.white;

            }
           

        }
        yield return new WaitForSeconds(.4f);

        gameObject.layer = 6;
        db = false;
    }

}
