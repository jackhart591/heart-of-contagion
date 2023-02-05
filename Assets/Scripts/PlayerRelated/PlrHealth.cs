using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlrHealth : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] bool db = false;
    [SerializeField] float MaxHealth = 5f;
    private bool dead = false;
    [SerializeField] float Health;
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

    public float GetHealth()
    {
        return Health;

    }

    public void GainHealth(float gain) {
        Health = ((Health+gain) > MaxHealth) ? MaxHealth : Health+gain;
    }

    public void Damage(float dam) {
        Health -= dam;
        Hurt();
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
        {
           

            if (db == false)
            {
                Debug.Log("OK");
                Hurt();

            }


        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
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

        gameObject.layer = default;
        db = false;
    }

}
