using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LightingBuddy : MonoBehaviour
{
    public Material NewMat;
    public TilemapRenderer Tily;
    // Start is called before the first frame update
    void Start()
    {
        if(Tily && NewMat)
        {

            Tily.material = NewMat;
        }
    }

}
