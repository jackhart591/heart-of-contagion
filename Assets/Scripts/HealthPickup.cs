using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public float HealthGainAmount = 2.5f;
    
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            col.GetComponent<PlrHealth>().GainHealth(HealthGainAmount);
            Destroy(gameObject);
        }
    }
}
