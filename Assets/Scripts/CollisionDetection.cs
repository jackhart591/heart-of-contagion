using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {
    public PlayerMovement player;

    void OnTriggerEnter2D(Collider2D col) {
        player.collidingObjs.Add(col.gameObject);
    }

    void OnTriggerExit2D(Collider2D col) {
        try {
            player.collidingObjs.Remove(col.gameObject);
        } catch {}
    }
}
