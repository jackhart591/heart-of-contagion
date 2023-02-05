using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Persistent {
    public static Vector3 PlayerCheckpoint { get; set; }
    public static List<GameObject> usedCheckpoints { get; set; }
}
