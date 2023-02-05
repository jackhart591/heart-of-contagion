using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour {
    void Start() {
        DontDestroyOnLoad(gameObject);
    }
}
