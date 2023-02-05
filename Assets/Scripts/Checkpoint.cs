using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    
    private void Start() {
        try {
            if (Persistent.usedCheckpoints.Contains(gameObject)) {
                Destroy(gameObject);
            }
        } catch {}
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            Persistent.PlayerCheckpoint = transform.position;

            if (Persistent.usedCheckpoints == null) 
                Persistent.usedCheckpoints = new List<GameObject>();
            
            Persistent.usedCheckpoints.Add(gameObject);
            Destroy(gameObject);
        }
    }
}
