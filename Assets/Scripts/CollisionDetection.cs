using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {
    public PlayerMovement player;

    void OnTriggerEnter(Collider col) {
        player.collidingObjs.Add(col.gameObject);
    }

    void OnTriggerExit(Collider col) {
        try {
            player.collidingObjs.Remove(col.gameObject);
        } catch {}
    }
}
