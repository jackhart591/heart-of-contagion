using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {
    
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            // Put health code here
            Destroy(gameObject);
        }
    }
}
