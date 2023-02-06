using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour {

    float opacity = 0f;
    
    public IEnumerator TriggerDeath() {
        Debug.Log("starting");
        if (opacity < 255f) {
            opacity += 1;
            transform.GetChild(0).GetComponent<Image>().material.color = new Color(0f, 0f, 0f, opacity);
            yield return null;
        } else {
            Application.Quit();
        }
    }
}
