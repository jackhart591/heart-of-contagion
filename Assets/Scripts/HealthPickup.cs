using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public float HealthGainAmount = 2.5f;
    [SerializeField] int AITYPE = 0;
    private void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.tag == "Player") {
            switch (AITYPE)
            {


                case (0):
                    col.GetComponent<PlrHealth>().GainHealth(HealthGainAmount);
                    Destroy(gameObject);
                    break;


                case (1):
                    col.GetComponent<PlayerMovement>().RootPower = true;
                    col.GetComponent<SpriteRenderer>().color = Color.green;
                    Destroy(gameObject);
                    break;

                default:

                    break;
            }
            
        }
    }
}
